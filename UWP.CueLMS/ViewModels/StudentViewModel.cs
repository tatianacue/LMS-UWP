using Library.LMS.Models;
using Library.LMS.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UWP.CueLMS.Dialogs;
using UWP.CueLMS.Dialogs.StudentViewDialogs;

namespace UWP.CueLMS.ViewModels
{
    public class StudentViewModel : ObservableObject
    {
        public StudentViewModel(CourseService c, PersonService p, Person s) 
        {
            StudentCourseList = new List<Course>();
            Student = s;
            CourseService = c;
            PersonService = p;
            AllCurrentCourses = CourseService.SpringList; //default semester is spring for now
            StudentCourses(); //finds all student courses
            CurrentCollection = new ObservableCollection<Course>(StudentCourseList);
        }
        public Person Student { get; set; }
        public PersonService PersonService { get; set;}
        public CourseService CourseService { get; set;}
        public List<Course> AllCurrentCourses { get; set; }
        public List<Course> StudentCourseList { get; set; }
        public List<Person> AllStudents { get { return PersonService.personList.Where(x => x is Student).ToList(); } }
        public ObservableCollection<Course> CurrentCollection { get; set; }
        public ObservableCollection<Person> StudentsCollection 
        { 
            get { return new ObservableCollection<Person>(AllStudents); }
        }
        public Course SelectedCourse { get; set; }
        public Person SelectedStudent { get; set; }
        public string Name
        {
            get
            {
                if (Student != null) 
                {
                    return Student.Name;
                }
                else { return string.Empty; }
            }
        }
        private void StudentCourses()
        {
            foreach (var course in AllCurrentCourses) 
            {
                foreach(var person in course.Roster)
                {
                    if(person.ID.Equals(Student.ID))
                    {
                        StudentCourseList.Add(course);
                        break;
                    }
                }
            }
        }
        public void ChangeStudent()
        {
            Student = SelectedStudent;
        }
        public async void SwitchStudent()
        {
            var dialog = new StudentViewSelector(this);
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            StudentCourseList.Clear();
            StudentCourses();
            AutoRefresh(); //refreshes display courses
            NotifyPropertyChanged(nameof(Name)); //refreshes display name
        }
        public void AutoRefresh()
        {
            var copy = StudentCourseList;
            CurrentCollection.Clear();
            foreach (var course in copy)
            {
                CurrentCollection.Add(course);
            }
        }
    }
}
