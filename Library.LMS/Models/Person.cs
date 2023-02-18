using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/* Tatiana Graciela Cue COP4870-0001*/
namespace Library.LMS.Models
{
    public class Person
    {
        public Person()
        {
            Grades = new Dictionary<string, double>();
        }

        //properties
        public string Name { get; set; }
        private string classification;
        public Dictionary<string, double> Grades { get; set; } //dictionary for grades
        public string ID { get; set; }

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

        //other
        public override string ToString() //override output person
        {
            return $"[{ID}] - {Name}";
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
