using Library.LMS.Models;
using Library.LMS.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UWP.CueLMS.Dialogs;
using Windows.Graphics.Printing.PrintSupport;
using Windows.UI.Xaml.Controls.Primitives;

namespace UWP.CueLMS.ViewModels
{
    public class MainViewModel
    {
        private PersonService personService { get; set; }
        public string Query { get; set; }
        public ObservableCollection<Person> allPeople { get; set; }
        private ObservableCollection<Person> people { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName]  string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public MainViewModel() 
        { 
            personService = new PersonService();
            allPeople = new ObservableCollection<Person>(personService.personList);
            people = new ObservableCollection<Person>(personService.personList);
        }
        public ObservableCollection<Person> People
        {
            get 
            {
                return people; 
            }
            set 
            {
                people = value;
            }
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
        public async void AddStudent()
        {
            var dialog = new StudentDialog(allPeople);
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            Query = "";
            SearchPeople(); //autorefresh
        }
    }
}
