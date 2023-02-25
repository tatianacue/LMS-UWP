using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int Grade { get; set; }
        public Student Student { get; set; }
        public Assignment Assignment { get; set; }
        public override string ToString()
        {
            return $"[{Id}] {Student.Name} - {Assignment.Name}";
        }
    }
}
