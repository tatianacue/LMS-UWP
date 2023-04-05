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
        public CourseManagerViewModel(Course course) 
        {
            Course = course;
            Modules = new ObservableCollection<Module>(Course.Modules);
            Announcements = new ObservableCollection<Announcement>(Course.Announcements);
            Roster = new ObservableCollection<Person>(Course.Roster);
        }
        public Course Course { get; set; }
        public ObservableCollection<Module> Modules { get; set; }
        public ObservableCollection<Announcement> Announcements { get; set; }
        public ObservableCollection<Person> Roster { get; set; }
        public Announcement SelectedAnnouncement { get; set; }
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
            foreach (var person in searchResults)
            {
                Announcements.Add(person);
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
    }
}
