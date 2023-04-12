using Library.LMS.Models;
using System.Collections.Generic;
/* Tatiana Graciela Cue COP4870-0001*/
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
