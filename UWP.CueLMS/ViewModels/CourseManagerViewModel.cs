using Library.LMS.Models;
using Library.LMS.Models.Grading;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UWP.CueLMS.Dialogs;
using UWP.CueLMS.Dialogs.AssignmentGroupDialogs;

namespace UWP.CueLMS.ViewModels
{
    public class CourseManagerViewModel : ObservableObject
    {
        public CourseManagerViewModel(Course course, List<Person> people) 
        {
            Course = course;
            Modules = new ObservableCollection<Module>(Course.Modules);
            Announcements = new ObservableCollection<Announcement>(Course.Announcements);
            Roster = new ObservableCollection<Person>(Course.Roster);
            Assignments = new ObservableCollection<Assignment>(Course.Assignments);
            AssignmentGroups = new ObservableCollection<AssignmentGroup>(Course.AssignmentGroups);

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
        }
        public Course Course { get; set; }
        public string CourseCode
        {
            get
            {     if (Course != null) {return Course.Code;}
                  else { return String.Empty; }
            }
        }
        public ObservableCollection<Assignment> Assignments { get; set; }
        public ObservableCollection<Module> Modules { get; set; }
        public ObservableCollection<Announcement> Announcements { get; set; }
        public ObservableCollection<Person> Roster { get; set; }
        public ObservableCollection<Person> AllStudents { get; set; }
        public ObservableCollection<AssignmentGroup> AssignmentGroups { get; set; }
        public Announcement SelectedAnnouncement { get; set; }
        public Module SelectedModule { get; set; }
        public Student SelectedStudent { get; set; }
        public Assignment SelectedAssignment { get; set; }
        public AssignmentGroup SelectedAssignmentGroup { get; set; }
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
            var dialog = new AnnouncementDialog(Course.Announcements);
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            AAutoRefresh();
        }
        public void DeleteAnnouncement()
        {
            Course.Announcements.Remove(SelectedAnnouncement);
            AAutoRefresh();
        }
        public void AAutoRefresh()
        {
            var searchResults = Course.Announcements.Where(p => p.Title.Contains("")).ToList();
            Announcements.Clear();
            foreach (var a in searchResults)
            {
                Announcements.Add(a);
            }
        }
        public string NewATitle { get; set; }//new announcment title
        public string NewAText { get; set; }//new announcement body
        public void UpdateATitle()
        {
            if (NewATitle!= null)
            {
                SelectedAnnouncement.Title = NewATitle;
            }
            NotifyPropertyChanged(nameof(DisplayATitle));
        }
        public void UpdateAText()
        {
            if (NewAText != null)
            {
                SelectedAnnouncement.Text = NewAText;
            }
            NotifyPropertyChanged(nameof(DisplayAText));
        }
        //Module Stuff
        public async void ModuleDialog()
        {
            var dialog = new ModuleDialog(Course.Modules);
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            MAutoRefresh();
        }
        public void DeleteModule()
        {
            Course.Modules.Remove(SelectedModule);
            MAutoRefresh();
        }
        public void MAutoRefresh()
        {
            var searchResults = Course.Modules.Where(p => p.Name.Contains("")).ToList();
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
            var dialog = new AssignmentDialog(Course.Assignments);
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            AssignmentAutoRefresh();
        }
        public void AssignmentAutoRefresh()
        {
            var copy = Course.Assignments;
            Assignments.Clear();
            foreach (var a in copy)
            {
                Assignments.Add(a);
            }
        }
        public void DeleteAssignment()
        {
            Course.Assignments.Remove(SelectedAssignment);
            AssignmentAutoRefresh();
        }
        public async void AddToGroupDialog()
        {
            var dialog = new AddToGroupDialog(this);
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
        }
        public async void CreateGroup() //create new assignment group
        {
            var dialog = new AssignmentGroupDialog(Course.AssignmentGroups, SelectedAssignment);
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
        public void AddToAssignmentGroup()
        {
            SelectedAssignmentGroup.AddAssignment(SelectedAssignment);
        }
        public void AGAutoRefresh()
        {
            var copy = Course.AssignmentGroups;
            AssignmentGroups.Clear();
            foreach (var group in copy)
            {
                AssignmentGroups.Add(group);
            }
        }

    }
}
