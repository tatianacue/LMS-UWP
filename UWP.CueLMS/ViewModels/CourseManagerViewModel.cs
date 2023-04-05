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
    public class CourseManagerViewModel
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
    }
}
