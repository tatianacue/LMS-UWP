using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
/* Tatiana Graciela Cue COP4870-0001*/
namespace Library.LMS.Models.Grading
{
    public class AssignmentGroup
    {
        public AssignmentGroup()
        {
            Group = new List<Assignment>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
        public List<Assignment> Group { get; set; }
        public void AddAssignment(Assignment assignment)
        {
            Group.Add(assignment);
        }
        public override string ToString()
        {
            return $"[{Id}] - {Name}";        
        }
        public string Display => $"{Name} [Weight: {Weight}]";
    }
}
