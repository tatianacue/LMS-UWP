using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/* Tatiana Graciela Cue COP4870-0001*/
namespace Library.LMS.Models.Grading
{
    public class Submission
    {
        public Submission()
        {
            Id = LastId++;
        }
        public static int LastId = 1;
        public int Id { get; private set; }
        public double Grade { get; set; }
        public Student Student { get; set; }
        public Assignment Assignment { get; set; }
        public Course Course { get; set; }

        public override string ToString()
        {
            return $"[{Id}] {Student.Name} - {Assignment.Name}";
        }
    }
}
