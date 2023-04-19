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
        //other
        public override string ToString() //override output person
        {
            return $"[{ID}] - {Name}";
        }
        public virtual string Display => $"[{ID}] - {Name}";
    }
}
