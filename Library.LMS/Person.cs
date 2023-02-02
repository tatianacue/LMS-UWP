using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LMS
{
    public class Person
    {
        public Person() 
        {
            Grades = new Dictionary<string, double>();
        }

        private string name;
        private string classification;
        public Dictionary<string, double> Grades { get; set; }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Classification
        {
            get { return classification; }
            set { 
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
