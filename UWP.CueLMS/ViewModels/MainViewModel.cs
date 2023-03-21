using Library.LMS.Models;
using Library.LMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWP.CueLMS.ViewModels
{
    public class MainViewModel
    {
        private PersonService personService { get; set; }
        public MainViewModel() 
        { 
            personService = new PersonService();
        }

        public List<Person> People
        {
            get { return personService.personList; } 
        }
    }
}
