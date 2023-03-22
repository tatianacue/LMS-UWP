using Library.LMS.Models;
using Library.LMS.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Printing.PrintSupport;
using Windows.UI.Xaml.Controls.Primitives;

namespace UWP.CueLMS.ViewModels
{
    public class MainViewModel
    {
        private PersonService personService { get; set; }
        public string Query { get; set; }
        private List<Person> allPeople { get; set; }
        private ObservableCollection<Person> people { get; set; }
        public MainViewModel() 
        { 
            personService = new PersonService();
            allPeople = personService.personList;
            people = new ObservableCollection<Person>(personService.personList);
        }
        public ObservableCollection<Person> People
        {
            get { return people; }
            private set { People = value; }
        }

        public void SearchPeople() 
        {
            var searchResults = allPeople.Where(p => p.Name.Contains(Query)).ToList();
            People.Clear();
            foreach (var person in searchResults)
            {
                People.Add(person);
            }
        }
    }
}
