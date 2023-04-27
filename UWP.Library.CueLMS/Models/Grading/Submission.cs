using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
/* Tatiana Graciela Cue COP4870-0001*/
namespace Library.LMS.Models.Grading
{
    public class Submission
    {
        public Submission() {  }
        public int Id { get; set; }
        public double Grade { get; set; }
        public Student Student { get; set; }
        public Assignment Assignment { get; set; }

        public override string ToString()
        {
            return $"{Student.Name} - {Assignment.Name}";
        }
        public string Display => $"[{Assignment.TotalAvailablePoints} points] {Student.Name} - {Assignment.Name}";
    }
}
