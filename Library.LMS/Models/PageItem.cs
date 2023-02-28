using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LMS.Models
{
    public class PageItem : ContentItem //derived
    {
        public PageItem() 
        {
            Name= string.Empty;
            Description= string.Empty;
            Id = $"P{LastId++}";
        }
        private static int LastId = 1;
        public string HTMLBody { get; set; }
        public override string DisplayAll()
        {
            return $"{Name} - {Description}\n" +
                $"{HTMLBody}";
        }
    }
}
