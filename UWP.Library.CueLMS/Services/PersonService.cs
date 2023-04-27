using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.LMS.Models;
using Newtonsoft.Json;
using UWP.Library.CueLMS;
/* Tatiana Graciela Cue COP4870-0001*/
namespace Library.LMS.Services
{
    public class PersonService
    {
        public List<Person> personList
        {
            get
            {
                var list = new List<Person>();
                foreach (var person in studentList)
                {
                    list.Add(person);
                }
                foreach (var person in taList)
                {
                    list.Add(person);
                }
                foreach (var person in instructorList)
                {
                    list.Add(person);
                }
                return list.OrderBy(x => x.IdNumber).ToList();
            }
        }
        public List<Student> studentList
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5100/Person/GetStudents").Result;
                return JsonConvert.DeserializeObject<List<Student>>(payload);
            }
        }
        public List<TeachingAssistant> taList
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5100/Person/GetTeachingAssistants").Result;
                return JsonConvert.DeserializeObject<List<TeachingAssistant>>(payload);
            }
        }
        public List<Instructor> instructorList
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5100/Person/GetInstructors").Result;
                return JsonConvert.DeserializeObject<List<Instructor>>(payload);
            }
        }
        public Dictionary<string, int> IDDictionary { get; set; } //ID checker
        public PersonService()
        {
            IDDictionary = new Dictionary<string, int>();
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
        public Person SelectedStudent { get; set; }
    }
}
