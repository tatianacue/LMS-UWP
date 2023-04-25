using Library.LMS.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using UWP.Library.CueLMS;
using UWP.Library.CueLMS.Database;
/* Tatiana Graciela Cue COP4870-0001*/
namespace UWP.CueLMS.ViewModels
{
    public class AnnouncementViewModel
    {
        public AnnouncementViewModel(Course c) 
        { 
            Announcement = new Announcement();
            course = c;
        }
        public Announcement Announcement { get; set; }
        public Course course { get; set; }
        public string Title 
        { 
            set { Announcement.Title = value; }
        }
        public string Text 
        { 
            set { Announcement.Text = value;}
        }
        public async void AddAnnouncement()
        {
            course.SelectedAnnouncement = Announcement;
            var handler = new WebRequestHandler();
            await handler.Post("http://localhost:5100/Announcement", course, HttpMethod.Post);
        }
    }
}
