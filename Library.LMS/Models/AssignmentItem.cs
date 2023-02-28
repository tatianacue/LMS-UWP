using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LMS.Models
{
    public class AssignmentItem : ContentItem //derived
    {
        public AssignmentItem() 
        {
            Name = string.Empty;
            Description = string.Empty;
            Id = $"A{LastId++}";
        }
        public Assignment Assignment { get; set; }
        private static int LastId = 1;
        public override string DisplayAll()
        {
            return $"{Name} - {Description}\n" +
                $"{Assignment.Display()}";
        }
    }
}
