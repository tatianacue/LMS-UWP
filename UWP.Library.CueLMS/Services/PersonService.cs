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
        public List<Person> personList { get; set; }
        public Dictionary<string, int> IDDictionary { get; set; } //ID checker
        public PersonService()
        {
            personList = new List<Person>();
            IDDictionary = new Dictionary<string, int>();
        }
        
        public void Add(Person person) //adds student, TA, or instructor to list
        {
            personList.Add(person);
        }

        public bool CheckID(string ID)
        {
            var result = new ArgumentException();
            try { IDDictionary.Add(ID, 0); }
            catch (ArgumentException r)
            {
                result = r;
            }
            if (result.Source == null) //if key already exists
            {
                return true;
            }
            else { return false; }
        }
        public List<Person> Search(string srch) //searches for student based on string
        {
            var searchStudent = personList.Where(t => t is Student && t.Name.Contains(srch)); //list of students
            List<Person> results = searchStudent.ToList(); //enumerable to list

            return results;
        }
    }
}
