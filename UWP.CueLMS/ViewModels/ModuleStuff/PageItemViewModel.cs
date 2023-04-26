using Library.LMS.Models;
using System.Collections.Generic;
using System.Net.Http;
using UWP.Library.CueLMS;
/* Tatiana Graciela Cue COP4870-0001*/
namespace UWP.CueLMS.ViewModels.ModuleStuff
{
    public class PageItemViewModel
    {
        public PageItemViewModel(Course course)
        {
            Course = course;
            PageItem = new PageItem();
        }
        public PageItem PageItem { get; set; }
        public Course Course { get; set; }
        public List<ContentItem> Content { get; set; }
        public string Name 
        { 
            set 
            {
                PageItem.Name = value;
            }
        }
        public string Description
        {
            set
            {
                PageItem.Description = value;
            }
        }
        public string HTMLBody
        {
            set
            {
                PageItem.HTMLBody = value;
            }
        }
        public async void AddItem()
        {
            Course.SelectedItem = PageItem;
            var handler = new WebRequestHandler();
            await handler.Post("http://localhost:5100/Module/PostContent", Course, HttpMethod.Post);
        }
    }
}
