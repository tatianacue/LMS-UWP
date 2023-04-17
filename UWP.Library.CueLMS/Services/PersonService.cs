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
        //public List<Person> personList { get;}
        public List<Person> personList
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5100/Person").Result;
                return JsonConvert.DeserializeObject<List<Person>>(payload);
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
