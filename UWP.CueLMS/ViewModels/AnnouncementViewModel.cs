using Library.LMS.Models;
using System.Collections.Generic;
/* Tatiana Graciela Cue COP4870-0001*/
namespace UWP.CueLMS.ViewModels
{
    public class AnnouncementViewModel
    {
        public AnnouncementViewModel(List<Announcement> announcements) 
        { 
            Announcement = new Announcement();
            Announcements = announcements;
        }
        public Announcement Announcement { get; set; }
        public List<Announcement> Announcements { get; set; }
        public string Title 
        { 
            set { Announcement.Title = value; }
        }
        public string Text 
        { 
            set { Announcement.Text = value;}
        }
        public void AddAnnouncement()
        {
            Announcements.Add(Announcement);
        }
    }
}
