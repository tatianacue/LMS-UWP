using Library.LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWP.CueLMS.ViewModels.ModuleStuff
{
    public class PageItemViewModel
    {
        public PageItemViewModel(List<ContentItem> content)
        {
            Content = content;
            PageItem = new PageItem();
        }
        public PageItem PageItem { get; set; }
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
        public void AddItem()
        {
            Content.Add(PageItem);
        }
    }
}
