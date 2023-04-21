using Library.LMS.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using UWP.Library.CueLMS;
using UWP.Library.CueLMS.Database;
/* Tatiana Graciela Cue COP4870-0001*/
namespace UWP.CueLMS.ViewModels
{
    public class AnnouncementViewModel
    {
        public AnnouncementViewModel(List<int> ids) 
        { 
            Announcement = new Announcement();
            Ids = ids;
        }
        public Announcement Announcement { get; set; }
        public List<int> Ids { get; set; }
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
            var payload = new WebRequestHandler().Get("http://localhost:5100/Announcement").Result;
            var list = JsonConvert.DeserializeObject<List<Announcement>>(payload).OrderBy(x => x.Id).ToList();
            if (list.Count == 0) //if this is going to be the first item
            {
                Announcement.Id = 1;
            }
            else
            {
                var lastId = list.Select(x => x.Id).Max();
                Announcement.Id = ++lastId; //do last id thing
            }

            Ids.Add(Announcement.Id); //add to list of announcement ids in course

            var handler = new WebRequestHandler(); //send announcement to database
            await handler.Post("http://localhost:5100/Announcement", Announcement);
        }
    }
}
