using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.LMS.Models;
/* Tatiana Graciela Cue COP4870-0001*/
namespace Library.LMS.Services
{
    public class PersonService
    {
        public List<Person> studentList { get; set; }
        public PersonService()
        {
            studentList = new List<Person>();
        }
        
        public void Add(Person student) //adds student to student list
        {
            studentList.Add(student);
        }
        public List<Person> Search(string srch) //searches for student based on string
        {
            var searchStudent = studentList.Where(t => t.Name.Contains(srch, StringComparison.InvariantCultureIgnoreCase));
            List<Person> results = searchStudent.ToList(); //enumerable to list

            return results;
        }
    }
}
