using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWP.Library.CueLMS;
/* Tatiana Graciela Cue COP4870-0001*/
namespace Library.LMS.Models
{
    public class Module
    {
        public Module() {}
        public int Id { get; set; }
        public override string ToString() //override output course
        {
            return $"[{Id}] {Name}";
        }

        //properties
        public string Name { get; set; }
        public string Description { get; set; }
        //database lists
        public List<FileItem> FileItems = new List<FileItem>();
        public List<PageItem> PageItems = new List<PageItem>();
        public List<AssignmentItem> AssignmentItems = new List<AssignmentItem>();
        public string Display => $"{Name}";
    }
}
