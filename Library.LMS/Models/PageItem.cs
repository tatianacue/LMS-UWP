using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LMS.Models
{
    public class PageItem : ContentItem //derived
    {
        public PageItem() { }

        public string HTMLBody { get; set; }
    }
}
