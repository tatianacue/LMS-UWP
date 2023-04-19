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
                var payload = new WebRequestHandler().Get("http://localhost:5100/Person").Result;
                var studentlist = JsonConvert.DeserializeObject<List<Student>>(payload);
                var talist = JsonConvert.DeserializeObject<List<TeachingAssistant>>(payload);
                var instructorlist = JsonConvert.DeserializeObject<List<Instructor>>(payload);
                var people = new List<Person>();
                foreach (var person in  studentlist) //make sure type is derived type
                {
                    if (person.Type == 0)
                    {
                        people.Add(person);
                    }
                }
                foreach (var person in instructorlist)
                {
                    if (person.Type == 1)
                    {
                        people.Add(person);
                    }
                }
                foreach (var person in talist)
                {
                    if (person.Type == 2)
                    {
                        people.Add(person);
                    }
                }
                return people.OrderBy(x => x.IdNumber).ToList(); //default list order
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
