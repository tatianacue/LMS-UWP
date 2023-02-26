using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LMS.Models
{
    public class Student : Person
    {
        public Student() 
        {
            Grades = new Dictionary<Course, double>();
            AssignmentGrades = new Dictionary<int, double>();
        }
        public override string ToString()
        {
            return $"[{ID.ToUpper()}] {Name} - {Classification}";
        }
        public Dictionary<Course, double> Grades { get; set; } //dictionary for course grades
        public Dictionary<int, double> AssignmentGrades { get; set; } //grades for specific assignments
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
        public void AddAssignmentGrade(int Id, double grade) //takes in assignment id and grade for it
        {
            AssignmentGrades.Add(Id, grade);
        }
        public void AddCourseGrade(Course course, double grade) //adds overall course grade
        {
            Grades.Add(course, grade);
        }

    }
}
