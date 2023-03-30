using Library.LMS.Models;
using Library.LMS.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UWP.CueLMS.Dialogs;
using UWP.CueLMS.Views;

namespace UWP.CueLMS.ViewModels
{
    public class InstructorViewModel
    {
        public InstructorViewModel()
        {
            courseService = new CourseService();
            personService = new PersonService();
            Courses = courseService.courseList;
            People = personService.personList;
            Collection = new ObservableCollection<Person>(People);
        }
        public Person SelectedPerson { get; set; }
        private CourseService courseService { get; set; }
        private PersonService personService { get; set; }
        public List<Course> Courses { get; set; }
        public List<Person> People { get; set; }
        private ObservableCollection<Person> collection { get; set; }
        public ObservableCollection<Person> Collection { get { return collection; } set { collection = value; } } 
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public async void PersonDialog(InstructorViewModel viewmodel)
        {
            var dialog = new AddPerson(viewmodel);
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
        }
        public async void AddStudent()
        {
            var dialog = new StudentDialog(People);
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            Query = ""; //refresh
            SearchPeople();
        }
        public async void AddTeachingAssistant()
        {
            var dialog = new TeachingAssistantDialog(People);
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            Query = "";
            SearchPeople();
        }
        public async void AddInstructor()
        {
            var dialog = new InstructorDialog(People);
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            Query = "";
            SearchPeople();
        }
        public void SearchPeople()
        {
            var searchResults = People.Where(p => p.Name.Contains(Query)).ToList();
            Collection.Clear();
            foreach (var person in searchResults)
            {
                Collection.Add(person);
            }
        }
        public string Query { get; set; }
        public string NewName { get; set; }
        public string NewId { get; set; }
        private string classification;
        public string NewClassification { get { return classification; }  
            set 
            { 
                if (value.Equals("f"))
                {
                    classification = "Freshman";     
                }
                else if (value.Equals("s"))
                {
                    classification = "Sophomore";
                }
                else if (value.Equals("j"))
                {
                    classification = "Junior";
                }
                else if (value.Equals("e"))
                {
                    classification = "Senior";
                }
            } 
        }   
        public string NewDescription { get; set; }
        public void DeletePerson()
        {
            People.Remove(SelectedPerson);
            Query = "";
            SearchPeople();
        }
        public void UpdateId()
        {
            if (NewId != null)
            {
                SelectedPerson.ID = NewId;
            }
            DisplayPerson = SelectedPerson.Display;
        }
        public void UpdateName()
        {
            if (NewName != null)
            {
                SelectedPerson.Name = NewName;
            }
        }
        public void UpdateClassification()
        {
            if (NewClassification != null)
            {
                Student selected = (Student)SelectedPerson;
                selected.Classification = NewClassification;
            }
        }
        private string displaystuff
        {
            get
            {
                return SelectedPerson.Display;
            }
            set { }
        }
        public string DisplayPerson 
        {
            get
            {
                if (SelectedPerson != null) {
                    return SelectedPerson.Display;
                }
                else
                {
                    return $"No Person Selected";
                }
            }
            set
            { 
                displaystuff = value;
            }
        }

    }
}
