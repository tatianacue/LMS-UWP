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
            Grades = new Dictionary<Course, double>(); //all course grades in a list
            AssignmentGrades = new Dictionary<Assignment, double>();
            Name= string.Empty;
            Classification= string.Empty;
        }
        public override string ToString()
        {
            return $"[{ID}] {Name} - {Classification}";
        }
        public override string Display => $"[{ID}] {Name} - {Classification}";
        public Dictionary<Course, double> Grades { get; set; } //dictionary for course grades
        public Dictionary<Assignment, double> AssignmentGrades { get; set; } //grades for specific assignments
        public void AddAssignmentGrade(Assignment assignment, double grade) //takes in assignment id and grade for it
        {
            AssignmentGrades[assignment] = grade;
        }
    }
}
