using Library.LMS.Models;
using Library.LMS.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UWP.CueLMS.Dialogs;
using Windows.Graphics.Printing.PrintSupport;
using Windows.UI.Xaml.Controls.Primitives;

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
        public ObservableCollection<Person> Students { get; set; }
        public Person Selection { get; set; }
        public void FilterStudents()
        {
            var allstudents = personService.personList.Where(x => x is Student);
            foreach (var student in allstudents)
            {
                Students.Add(student);
            }
        }
        public void SelectStudent()
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
    }
}
