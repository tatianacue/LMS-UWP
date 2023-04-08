using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/* Tatiana Graciela Cue COP4870-0001*/
namespace Library.LMS.Models.Grading
{
    public class AssignmentGroup
    {
        public AssignmentGroup()
        {
            Group = new List<Assignment>();
            Id = LastId++;
        }
        public static int LastId = 1;
        public int Id { get; private set; }
        public string Name { get; set; }
        public int Weight { get; set; }
        public List<Assignment> Group { get; set;}
        public void AddAssignment(Assignment assignment)
        {
            Group.Add(assignment);
        }
        public override string ToString()
        {
            return $"[{Id}] - {Name}";        
        }
        public string Display => $"[{Id}] - {Name} [Weight: {Weight}]";
    }
}
