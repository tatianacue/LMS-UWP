using Library.LMS.Models;
using Library.LMS.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UWP.CueLMS.Dialogs;
/* Tatiana Graciela Cue COP4870-0001*/
namespace UWP.CueLMS.ViewModels
{
    public class MainViewModel
    {
        public MainViewModel() 
        {
            Students = new ObservableCollection<Person>();
            courseService = new CourseService();
            personService = new PersonService();
            Services = new Dictionary<CourseService, PersonService>() { {courseService, personService} };
            FilterStudents();
        }
        public CourseService courseService { get; set; }
        public PersonService personService { get; set; }
        public Dictionary<CourseService, PersonService> Services { get; set; }
        public ObservableCollection<Person> Students { get; set; } //displays students for selection
        public Person Selection { get; set; }
        public void FilterStudents() //filters person list to just students for student selection
        {
            var allstudents = personService.personList.Where(x => x is Student);
            foreach (var student in allstudents)
            {
                Students.Add(student);
            }
        }
        public void SelectStudent() //selected student to be displayed in student view
        {
            personService.SelectedStudent = Selection;
        }
        public async void SelectStudentDialog()
        {
            var dialog = new StudentSelectorDialog(this);
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
        }
        public void AutoRefresh()
        {
            Students.Clear();
            FilterStudents();
        }
    }
}
