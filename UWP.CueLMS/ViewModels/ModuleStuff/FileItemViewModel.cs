using Library.LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWP.CueLMS.ViewModels.ModuleStuff
{
    public class FileItemViewModel
    {
        public FileItemViewModel(List<ContentItem> content)
        {
            Content = content;
            FileItem = new FileItem();
        }
        public FileItem FileItem { get; set; }
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
        public string HTMLBody
        {
            set
            {
                FileItem.FilePath = value;
            }
        }
        public void AddItem()
        {
            Content.Add(FileItem);
        }
    }
}
