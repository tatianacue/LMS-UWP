using Library.LMS.Models;
using Library.LMS.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UWP.CueLMS.ViewModels
{
    public class StudentViewModel
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
        public ObservableCollection<Course> CurrentCollection { get; set; }
        public Course SelectedCourse { get; set; }
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
    }
}
