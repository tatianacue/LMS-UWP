using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/* Tatiana Graciela Cue COP4870-0001*/
namespace Library.LMS.Models
{
    public class Student : Person
    {
        public Student() 
        {
            Name = string.Empty;
            Classification= string.Empty;
        }
        public string Classification { get; set; }
        public override string ToString()
        {
            return $"[{ID}] {Name} - {Classification}";
        }
        public override string Display => $"[{ID}] {Name} - {Classification}";
        public Dictionary<Course, double> Grades = new Dictionary<Course, double>(); //dictionary for course grades

        //for setting grade in fake database
        public double TempGrade {  get; set; }
        public Course TempCourse { get; set; }
        
    }
}
