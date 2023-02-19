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
            Grades = new Dictionary<string, double>();
        }
        public override string ToString()
        {
            return $"[{ID.ToUpper()}] {Name} - {Classification}";
        }
        public Dictionary<string, double> Grades { get; set; } //dictionary for grades
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
        public void AddGrade(string assignment) //adds grade to assignment
        {
            Console.WriteLine("What grade would you like to give " + Name + " for this assignment?");
            double grade;
            while (!double.TryParse(Console.ReadLine(), out grade))
            {
                Console.WriteLine("Invalid. Try Again.");
                double.TryParse(Console.ReadLine(), out grade);
            }
            Grades.Add(assignment, grade);
        }

    }
}
