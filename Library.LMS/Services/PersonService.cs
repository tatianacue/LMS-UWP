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
        public List<Student> studentList { get; set; }
        public Dictionary<string, int> IDDictionary { get; set; } //ID checker
        public PersonService()
        {
            studentList = new List<Student>();
            IDDictionary = new Dictionary<string, int>();
        }
        
        public void Add(Student student) //adds student to student list
        {
            studentList.Add(student);
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
        public List<Student> Search(string srch) //searches for student based on string
        {
            var searchStudent = studentList.Where(t => t.Name.Contains(srch, StringComparison.InvariantCultureIgnoreCase));
            List<Student> results = searchStudent.ToList(); //enumerable to list

            return results;
        }
    }
}
