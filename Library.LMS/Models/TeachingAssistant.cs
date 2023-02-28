using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LMS.Models
{
    public class TeachingAssistant : Person
    {
        public TeachingAssistant() 
        {
            Name = string.Empty;
        }
        public override string ToString() //override output person
        {
            return $"[{ID}][TA] - {Name}";
        }
    }
}
