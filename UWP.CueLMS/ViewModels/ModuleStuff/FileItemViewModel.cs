using Library.LMS.Models;
using System.Collections.Generic;
using System.Net.Http;
using UWP.Library.CueLMS;
/* Tatiana Graciela Cue COP4870-0001*/
namespace UWP.CueLMS.ViewModels.ModuleStuff
{
    public class FileItemViewModel
    {
        public FileItemViewModel(Course course)
        {
            FileItem = new FileItem();
            Course = course;
        }
        public FileItem FileItem { get; set; }
        public Course Course { get; set; }
        public List<ContentItem> Content { get; set; }
        public string Name
        {
            set
            {
                FileItem.Name = value;
            }
        }
        public string Description
        {
            set
            {
                FileItem.Description = value;
            }
        }
        public string FilePath
        {
            set
            {
                FileItem.FilePath = value;
            }
        }
        public async void AddItem()
        {
            Course.SelectedItem = FileItem;
            var handler = new WebRequestHandler();
            await handler.Post("http://localhost:5100/Module/PostContent", Course, HttpMethod.Post);
        }
    }
}
