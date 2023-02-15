using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/* Tatiana Graciela Cue COP4870-0001*/
namespace Library.LMS.Models
{
    public class Module
    {
        public Module()
        {
            Content = new List<ContentItem>();
        }

        //properties
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ContentItem> Content { get; set; }
        public void Add(ContentItem item) //adds content item to list
        {
            Content.Add(item);
        }
    }
}
