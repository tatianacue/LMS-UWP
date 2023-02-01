using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
