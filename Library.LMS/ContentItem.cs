using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LMS
{
    public class ContentItem
    {
        public ContentItem() { }

        private string name;
        private string description;
        private string path;

        public string Name 
        { 
            get { return name; } 
            set { name = value; } 
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public string Path 
        {
            get { return path; }
            set { path = value; }
        }
    }
}
