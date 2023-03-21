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
            return $"[{ID.ToUpper()}] {Name} - {Classification}";
        }
        public Dictionary<Course, double> Grades { get; set; } //dictionary for course grades
        public Dictionary<Assignment, double> AssignmentGrades { get; set; } //grades for specific assignments
        private string classification;
        public string Classification //gets and sets classification
        {
            get { return classification; }
            set
            {
                if (value == "f")
                {
                    classification = "Freshman";
                }
                else if (value == "s")
                {
                    classification = "Sophomore";
                }
                else if (value == "j")
                {
                    classification = "Junior";
                }
                else if (value == "e")
                {
                    classification = "Senior";
                }
                else
                {
                    classification = "N/A";
                }
            }
        }
        public void AddAssignmentGrade(Assignment assignment, double grade) //takes in assignment id and grade for it
        {
            AssignmentGrades.Add(assignment, grade);
        }
    }
}
