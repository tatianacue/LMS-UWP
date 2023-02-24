using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LMS.Models.Grading
{
    public class AssignmentGroup
    {
        public AssignmentGroup()
        {
            group = new List<Assignment>();
            Id = LastId++;
        }
        public static int LastId = 1;
        public int Id { get; private set; }
        public string Name { get; set; }
        public int Weight { get; set; }
        public List<Assignment> group { get; set;}
        public void AddAssignment(Assignment assignment)
        {
            group.Add(assignment);
        }
        public override string ToString()
        {
            return $"[{Id}] - {Name}";        
        }
    }
}
