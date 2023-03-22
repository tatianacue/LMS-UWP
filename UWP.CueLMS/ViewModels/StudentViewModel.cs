using Library.LMS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWP.CueLMS.ViewModels
{
    public class StudentViewModel
    {
        private ObservableCollection<Person> people;
        public StudentViewModel(ObservableCollection<Person> persons) 
        {
            Student = new Student();
            this.people = persons;
        }
        private Student Student { get; set; }
        public string Name { 
            get { return Student.Name; } 
            set { Student.Name = value; } 
        }
        public void AddStudent()
        {
            people.Add(Student);
        }
    }
}
