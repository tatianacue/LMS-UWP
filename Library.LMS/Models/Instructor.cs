using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LMS.Models
{
    public class Instructor : Person
    {
        public Instructor() 
        {
            Name = string.Empty;
        }
        public override string ToString() //override output person
        {
            return $"[{ID}][INSTRUCTOR] - {Name}";
        }
    }
}
