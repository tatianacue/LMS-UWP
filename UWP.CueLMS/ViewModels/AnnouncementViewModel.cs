using Library.LMS.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
