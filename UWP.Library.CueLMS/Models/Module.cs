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
            Id = LastId++;
        }
        private static int LastId = 1;
        public int Id { get; private set; }
        public override string ToString() //override output course
        {
            return $"[{Id}] {Name}";
        }

        //properties
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ContentItem> Content { get; set; }
        public void AddContent(ContentItem item) //adds content item to list
        {
            Content.Add(item);
        }
        public void RemoveContent(ContentItem item)
        {
            Content.Remove(item);
        }
        public string Display => $"[{Id}] - {Name}";
    }
}
