using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
/* Tatiana Graciela Cue COP4870-0001*/
namespace Library.LMS.Models
{
    public class Person
    {
        public Person(){ }
        //properties
        public string Name { get; set; }
        public string ID { get; set; }
        public int IdNumber { get; set; }
        public int Type { get; set; } //type of derived class
        private string classification { get; set; }
        public string Classification //put this in person so that json includes this for student
        { 
            get
            {
                if (Type == 0) //if student
                {
                    return classification;
                }
                else
                {
                    return string.Empty;
                }
            }
            set { classification = value; }
        }
        //other
        public override string ToString() //override output person
        {
            return $"[{ID}] - {Name}";
        }
        public virtual string Display => $"[{ID}] - {Name}";
    }
}
