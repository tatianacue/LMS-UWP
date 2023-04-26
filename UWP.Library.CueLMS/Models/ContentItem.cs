using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/* Tatiana Graciela Cue COP4870-0001*/
namespace Library.LMS.Models
{
    public class ContentItem //base class
    {
        public ContentItem() { }

        //properties
        public string Name { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public int Type { get; set; }
        private Assignment assignment;
        public Assignment Assignment 
        { 
            get 
            {
                if (Type == 2) //only if assignment item
                {
                    return assignment;
                }
                else
                {
                    return null;
                }
            }
            set { assignment = value; }
        }
        private string htmlbody { get; set; }
        public string HTMLBody 
        { 
            get
            {
                if (Type == 1) 
                {
                    return htmlbody;
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                htmlbody = value;
            }
        }
        private string filepath { get; set; }
        public string FilePath 
        { 
            get
            {
                if (Type == 0)
                {
                    return filepath;
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                filepath = value;
            }
        }
        public override string ToString()
        {
            return $"[{Id}] {Name}";
        }
        public virtual string DisplayAll()
        {
            return $"{Name} - {Description}";
        }
        public virtual string Display => $"{Name}";

    }
}
