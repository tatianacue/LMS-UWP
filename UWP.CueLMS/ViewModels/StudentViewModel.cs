using Library.LMS.Models;
using Library.LMS.Models.Grading;
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
        //grade and gpa
        public double CalculateCourseGrade()
        {
            if (SelectedCourse != null)
            {
                var student = (Student)Student;
                var tempList = SelectedCourse.Submissions.Where(s => s.Student == student).ToList(); //list of submissions by specific student
                Dictionary<AssignmentGroup, double> GroupGrades = new Dictionary<AssignmentGroup, double>();
                double courseGrade = 0;
                foreach (var group in SelectedCourse.AssignmentGroups) //each group in course
                {
                    double totalGrades = 0;
                    double totalPoints = 0;
                    foreach (var submission in tempList) //for each submission
                    {
                        foreach (var assignment in group.Group) //each assignment in group
                        {
                            if (submission.Assignment == assignment)
                            {
                                totalGrades += submission.Grade;
                                totalPoints += assignment.TotalAvailablePoints;
                            }
                        }
                    }
                    var totalGroupGrade = totalGrades / totalPoints; //total grade for entire group
                    GroupGrades.Add(group, totalGroupGrade); //adds group and grade to dictionary
                }
                foreach (var pair in GroupGrades) //for each total grade in group, multiply by the weight
                {
                    double weightedTotal = (pair.Key.Weight) * (pair.Value);
                    courseGrade += weightedTotal;
                }
                student.Grades[SelectedCourse] = courseGrade; //update course grade dictionary in student
                return courseGrade;
            }
            else { return 0; }
        }
        public string CourseGrade { get { return $"{CalculateCourseGrade()}%"; } }
        public async void CourseGradeDialog()
        {
            var dialog = new CourseGradeDialog(this);
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
        }
    }
}
