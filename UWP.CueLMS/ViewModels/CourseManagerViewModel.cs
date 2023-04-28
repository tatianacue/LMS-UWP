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
            Roster = new ObservableCollection<Student>(rosterList);
            Assignments = new ObservableCollection<Assignment>(assignmentList);
            AssignmentGroups = new ObservableCollection<AssignmentGroup>(assignmentgroupList);

            peopleList = people;
            var students = people.Where(x => x is Student).ToList();
            AllStudents = new ObservableCollection<Person>(students);
            if (rosterList.Count != 0) //make sure add student doesn't show already added people
            {
                foreach (var student in rosterList)
                {
                    foreach (var person in students)
                    {
                        if (person.IdNumber == student.IdNumber)
                        {
                            AllStudents.Remove(person);
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
        public List<Student> rosterList
        {
            get
            {
                var id = Course.Id;
                if (Semester == 1) //spring
                {
                    var payload = new WebRequestHandler().Get($"http://localhost:5100/SpringCourse/GetRoster/{id}").Result;
                    return JsonConvert.DeserializeObject<List<Student>>(payload).OrderBy(x => x.IdNumber).ToList();
                }
                else if (Semester == 2) //summer
                {
                    var payload = new WebRequestHandler().Get($"http://localhost:5100/SummerCourse/GetRoster/{id}").Result;
                    return JsonConvert.DeserializeObject<List<Student>>(payload).OrderBy(x => x.IdNumber).ToList();
                }
                else //fall
                {
                    var payload = new WebRequestHandler().Get($"http://localhost:5100/FallCourse/GetRoster/{id}").Result;
                    return JsonConvert.DeserializeObject<List<Student>>(payload).OrderBy(x => x.IdNumber).ToList();
                }
            }
        }
        public List<Submission> submissionList
        {
            get
            {
                var id = Course.Id;
                var payload = new WebRequestHandler().Get($"http://localhost:5100/Submission/GetList/{id}").Result;
                return JsonConvert.DeserializeObject<List<Submission>>(payload).OrderBy(x => x.Id).ToList();
            }
        }
        //collections
        public ObservableCollection<Assignment> Assignments { get; set; }
        public ObservableCollection<Module> Modules { get; set; }
        public ObservableCollection<Announcement> Announcements { get; set; }
        public ObservableCollection<Student> Roster { get; set; }
        public ObservableCollection<Person> AllStudents { get; set; }
        public ObservableCollection<AssignmentGroup> AssignmentGroups { get; set; }
        public ObservableCollection<Submission> Submissions { get { return new ObservableCollection<Submission>(submissionList); } }
        public Announcement SelectedAnnouncement { get; set; }
        public Module SelectedModule { get; set; }
        public Student SelectedStudent { get; set; }
        public Assignment SelectedAssignment { get; set; }
        public AssignmentGroup SelectedAssignmentGroup { get; set; }
        public Submission SelectedSubmission { get; set; }
        public int Grade { private get; set; }
        private List<Person> peopleList {  get; set; }
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
        public async void AddToRoster()
        {
            Course.SelectedStudent = SelectedStudent;
            if (Semester == 1) //spring
            {
                var handler = new WebRequestHandler();
                await handler.Post("http://localhost:5100/SpringCourse/AddToRoster", Course, HttpMethod.Post);
            }
            else if (Semester == 2) //summer
            {
                var handler = new WebRequestHandler();
                await handler.Post("http://localhost:5100/SummerCourse/AddToRoster", Course, HttpMethod.Post);
            }
            else if (Semester == 3) //fall
            {
                var handler = new WebRequestHandler();
                await handler.Post("http://localhost:5100/FallCourse/AddToRoster", Course, HttpMethod.Post);
            }
        }
        public async void RemoveFromRoster()
        {
            Course.SelectedStudent = SelectedStudent;
            if (Semester == 1) //spring
            {
                var handler = new WebRequestHandler();
                await handler.Post("http://localhost:5100/SpringCourse/RemoveFromRoster", Course, HttpMethod.Delete);
            }
            else if (Semester == 2) //summer
            {
                var handler = new WebRequestHandler();
                await handler.Post("http://localhost:5100/SummerCourse/RemoveFromRoster", Course, HttpMethod.Delete);
            }
            else if (Semester == 3) //fall
            {
                var handler = new WebRequestHandler();
                await handler.Post("http://localhost:5100/FallCourse/RemoveFromRoster", Course, HttpMethod.Delete);
            }
            RAutoRefresh();

            var students = peopleList.Where(x => x is Student).ToList(); //refresh adder list
            AllStudents = new ObservableCollection<Person>(students);
            if (rosterList.Count != 0)
            {
                foreach (var student in rosterList)
                {
                    foreach (var person in students)
                    {
                        if (person.IdNumber == student.IdNumber)
                        {
                            AllStudents.Remove(person);
                        }
                    }
                }
            }
        }
        public void RAutoRefresh()
        {
            var searchResults = rosterList.Where(p => p.Name.Contains("")).ToList();
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
        public async void GradeSubmission()
        {
            SelectedSubmission.Grade = Grade; //sets in grade for submission
            Course.SelectedSubmission = SelectedSubmission;
            var handler = new WebRequestHandler();
            await handler.Post("http://localhost:5100/Submission", Course, HttpMethod.Post);
        }

    }
}
