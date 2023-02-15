using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LMS.Models
{
    public class AssignmentItem : ContentItem //derived
    {
        public AssignmentItem() { }
        public Assignment Assignment { get; set; }
    }
}
