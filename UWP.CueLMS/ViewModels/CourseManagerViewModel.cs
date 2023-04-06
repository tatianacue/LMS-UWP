using Library.LMS.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWP.CueLMS.Dialogs;

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
        public ObservableCollection<Module> Modules { get; set; }
        public ObservableCollection<Announcement> Announcements { get; set; }
        public ObservableCollection<Person> Roster { get; set; }
        public ObservableCollection<Person> AllStudents { get; set; }
        public Announcement SelectedAnnouncement { get; set; }
        public Module SelectedModule { get; set; }
        public Student SelectedStudent { get; set; }
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
    }
}
