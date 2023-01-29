using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LMS
{
    public class Person
    {
        public Person() { }

        private string name;
        private string classification;
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
    }
}
