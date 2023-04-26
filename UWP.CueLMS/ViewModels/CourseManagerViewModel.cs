using Library.LMS.Models;
using Library.LMS.Models.Grading;
using Library.LMS.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using UWP.CueLMS.Dialogs;
using UWP.CueLMS.Dialogs.AssignmentGroupDialogs;
using UWP.Library.CueLMS;
/* Tatiana Graciela Cue COP4870-0001*/
namespace UWP.CueLMS.ViewModels
{
    public class CourseManagerViewModel : ObservableObject
    {
        public CourseManagerViewModel(Course course, List<Person> people, int semester)
        {
            Course = course;
            Semester = semester;
            Modules = new ObservableCollection<Module>(moduleList);
            Announcements = new ObservableCollection<Announcement>(announcementList);
            Roster = new ObservableCollection<Person>(Course.Roster);
            Assignments = new ObservableCollection<Assignment>(assignmentList);
            AssignmentGroups = new ObservableCollection<AssignmentGroup>(assignmentgroupList);

            peopleList = people;
            var students = people.Where(x => x is Student).ToList();
            AllStudents = new ObservableCollection<Person>(students);
            if (Roster != null) //make sure add student doesn't show already added people
            {
                foreach (var student in Roster)
                {
                    foreach (var person in students)
                    {
                        if (person == student)
                        {
                            AllStudents.Remove(student);
                        }
                    }
                }
            }
            Semester = semester;
        }
        public Course Course { get; set; }
        public int Semester { get; set; }
        public string CourseCode
        {
            get
            { if (Course != null) { return Course.Code; }
                else { return String.Empty; }
            }
        }
        //lists from database
        public List<Announcement> announcementList
        {
            get
            {
                var id = Course.Id;
                var payload = new WebRequestHandler().Get($"http://localhost:5100/Announcement/GetList/{id}").Result;
                return JsonConvert.DeserializeObject<List<Announcement>>(payload).OrderBy(x => x.Id).ToList();
            }
        }
        public List<Assignment> assignmentList
        {
            get
            {
                var id = Course.Id;
                var payload = new WebRequestHandler().Get($"http://localhost:5100/Assignment/GetList/{id}").Result;
                return JsonConvert.DeserializeObject<List<Assignment>>(payload).OrderBy(x => x.Id).ToList();
            }
        }
        public List<AssignmentGroup> assignmentgroupList
        {
            get
            {
                var id = Course.Id;
                var payload = new WebRequestHandler().Get($"http://localhost:5100/AssignmentGroup/GetList/{id}").Result;
                return JsonConvert.DeserializeObject<List<AssignmentGroup>>(payload).OrderBy(x => x.Id).ToList();
            }
        }
        public List<Module> moduleList
        {
            get
            {
                var id = Course.Id;
                var payload = new WebRequestHandler().Get($"http://localhost:5100/Module/GetList/{id}").Result;
                return JsonConvert.DeserializeObject<List<Module>>(payload).OrderBy(x => x.Id).ToList();
            }
        }
        //collections
        public ObservableCollection<Assignment> Assignments { get; set; }
        public ObservableCollection<Module> Modules { get; set; }
        public ObservableCollection<Announcement> Announcements { get; set; }
        public ObservableCollection<Person> Roster { get; set; }
        public ObservableCollection<Person> AllStudents { get; set; }
        public ObservableCollection<AssignmentGroup> AssignmentGroups { get; set; }
        public ObservableCollection<Submission> Submissions { get { return new ObservableCollection<Submission>(SubmissionList); } }
        public Announcement SelectedAnnouncement { get; set; }
        public Module SelectedModule { get; set; }
        public Student SelectedStudent { get; set; }
        public Assignment SelectedAssignment { get; set; }
        public AssignmentGroup SelectedAssignmentGroup { get; set; }
        public Submission SelectedSubmission { get; set; }
        public int Grade { private get; set; }
        private List<Person> peopleList {  get; set; }
        private List<Submission> SubmissionList { get { return Course.Submissions; } }
        public string DisplayATitle //announcement display title
        {
            get
            {
                if (SelectedAnnouncement != null)
                {
                    return SelectedAnnouncement.Title;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        public string DisplayAText //announcement display title
        {
            get
            {
                if (SelectedAnnouncement != null)
                {
                    return SelectedAnnouncement.Text;
                }
                else { return string.Empty; }
            }
        }
        //Announcement stuff
        public async void AnnouncementDialog()
        {
            var dialog = new AnnouncementDialog(Course);
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            AAutoRefresh();
        }
        public async void DeleteAnnouncement()
        {
            Course.SelectedAnnouncement = SelectedAnnouncement;
            var handler = new WebRequestHandler();
            await handler.Post("http://localhost:5100/Announcement", Course, HttpMethod.Delete);
            AAutoRefresh();
        }
        public void AAutoRefresh()
        {
            var searchResults = announcementList.Where(p => p.Title.Contains("")).ToList();
            Announcements.Clear();
            foreach (var a in searchResults)
            {
                Announcements.Add(a);
            }
        }
        public string NewATitle { get; set; }//new announcment title
        public string NewAText { get; set; }//new announcement body
        public async void UpdateATitle()
        {
            if (NewATitle!= null)
            {
                SelectedAnnouncement.Title = NewATitle;
            }
            Course.SelectedAnnouncement = SelectedAnnouncement; //packages announcement in course to send through
            var handler = new WebRequestHandler();
            await handler.Post("http://localhost:5100/Announcement", Course, HttpMethod.Post);
            NotifyPropertyChanged(nameof(DisplayATitle));
        }
        public async void UpdateAText()
        {
            if (NewAText != null)
            {
                SelectedAnnouncement.Text = NewAText;
            }
            Course.SelectedAnnouncement = SelectedAnnouncement; //packages announcement in course to send through
            var handler = new WebRequestHandler();
            await handler.Post("http://localhost:5100/Announcement", Course, HttpMethod.Post);
            NotifyPropertyChanged(nameof(DisplayAText));
        }
        //Module Stuff
        public async void ModuleDialog()
        {
            var dialog = new ModuleDialog(Course);
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            MAutoRefresh();
        }
        public async void DeleteModule()
        {
            Course.SelectedModule = SelectedModule;
            var handler = new WebRequestHandler();
            await handler.Post("http://localhost:5100/Module", Course, HttpMethod.Delete);
            MAutoRefresh();
        }
        public void MAutoRefresh()
        {
            var searchResults = moduleList.Where(p => p.Name.Contains("")).ToList();
            Modules.Clear();
            foreach (var module in searchResults)
            {
                Modules.Add(module);
            }
        }
        //Roster Stuff
        public async void StudentToCourseDialog()
        {
            var dialog = new StudentToCourseDialog(this);
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            RAutoRefresh();
            AllStudents.Remove(SelectedStudent); //wont display again
        }
        public void AddToRoster()
        {
            Course.Roster.Add(SelectedStudent);
        }
        public void RemoveFromRoster()
        {
            Course.Roster.Remove(SelectedStudent);
            RAutoRefresh();

            var students = peopleList.Where(x => x is Student).ToList(); //refresh adder list
            AllStudents = new ObservableCollection<Person>(students);
            if (Roster != null)
            {
                foreach (var student in Roster)
                {
                    foreach (var person in students)
                    {
                        if (person == student)
                        {
                            AllStudents.Remove(student);
                        }
                    }
                }
            }
        }
        public void RAutoRefresh()
        {
            var searchResults = Course.Roster.Where(p => p.Name.Contains("")).ToList();
            Roster.Clear();
            foreach (var person in searchResults)
            {
                Roster.Add(person);
            }
        }
        //Assignment Stuff
        public async void AssignmentDialog()
        {
            var dialog = new AssignmentDialog(Course);
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            AssignmentAutoRefresh();
        }
        public void AssignmentAutoRefresh()
        {
            var copy = assignmentList;
            Assignments.Clear();
            foreach (var a in copy)
            {
                Assignments.Add(a);
            }
        }
        public async void DeleteAssignment()
        {
            Course.SelectedAssignment = SelectedAssignment;
            var handler = new WebRequestHandler();
            await handler.Post("http://localhost:5100/Assignment", Course, HttpMethod.Delete);
            AssignmentAutoRefresh();
        }
        public async void CreateGroup() //create new assignment group
        {
            var dialog = new AssignmentGroupDialog(Course);
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            AGAutoRefresh();
        }
        public async void AddToExisting() //add to existing group
        {
            var dialog = new AddToExistingDialog(this);
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
        }
        public async void AddToAssignmentGroup()
        {
            Course.SelectedAssignment = SelectedAssignment;
            Course.SelectedAssignmentGroup = SelectedAssignmentGroup;
            var handler = new WebRequestHandler();
            await handler.Post($"http://localhost:5100/AssignmentGroup", Course, HttpMethod.Post);
        }
        public void AGAutoRefresh()
        {
            var copy = assignmentgroupList;
            AssignmentGroups.Clear();
            foreach (var group in copy)
            {
                AssignmentGroups.Add(group);
            }
        }
        public async void GradeSubmissionDialog()
        {
            var dialog = new GradeSubmissionDialog(this);
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
        }
        public void GradeSubmission()
        {
            SelectedSubmission.Grade = Grade; //sets in grade for submission
            var percent = (Grade/SelectedSubmission.Assignment.TotalAvailablePoints) * 100;
            SelectedSubmission.Student.AddAssignmentGrade(SelectedSubmission.Assignment, percent); //puts in percent grade in student's grade dictionary
        }

    }
}
