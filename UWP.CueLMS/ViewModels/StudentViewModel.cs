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
        public async void GetCourseGradeDialog()
        {
            var dialog = new CourseGradeDialog(this);
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
        }
        public async void GetCurrentGPADialog()
        {
            var dialog = new CurrentGPADialog(this);
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
        }
        public double CalculateCurrentGPA()
        {
            Dictionary<Course, double> CourseGrades = new Dictionary<Course, double>(); //holds all course grades
            var courses = StudentCourseList;
            foreach (var course in courses)
            {
                SelectedCourse = course;
                double grade = CalculateCourseGrade();
                CourseGrades.Add(course, grade);
            }
            double numbers = 0;
            int totalCredits = 0;
            foreach (var pair in CourseGrades)
            {
                double gradeValue = ConvertGpaScale(pair.Value);
                int credit = pair.Key.CreditHours;
                totalCredits += credit;
                numbers += (gradeValue * credit); //multiplies by credit hours and adds to total
            }
            double Gpa = numbers / totalCredits;
            return Gpa;
        }
        public string CurrentGPA { get { return CalculateCurrentGPA().ToString("F2"); } }
        private double ConvertGpaScale(double courseGrade)
        {
            if (courseGrade >= 96) //A+
            {
                return 4.0;
            }
            else if (courseGrade <= 95 && courseGrade >= 91) //A
            {
                return 4.0;
            }
            else if (courseGrade <= 91 && courseGrade >= 90) //A-
            {
                return 3.7;
            }
            else if (courseGrade <= 89 && courseGrade >= 86) //B+
            {
                return 3.3;
            }
            else if (courseGrade <= 85 && courseGrade >= 81) //B
            {
                return 3.0;
            }
            else if (courseGrade <= 81 && courseGrade >= 80) //B-
            {
                return 2.7;
            }
            else if (courseGrade <= 79 && courseGrade >= 76) //C+
            {
                return 2.3;
            }
            else if (courseGrade <= 75 && courseGrade >= 71) //C
            {
                return 2.0;
            }
            else if (courseGrade <= 71 && courseGrade >= 70) //C-
            {
                return 1.7;
            }
            else if (courseGrade <= 69 && courseGrade >= 66) //D+
            {
                return 1.3;
            }
            else if (courseGrade <= 65 && courseGrade >= 61) //D
            {
                return 1.0;
            }
            else if (courseGrade <= 61 && courseGrade >= 60) //D-
            {
                return 0.7;
            }
            else //F
            {
                return 0;
            }
        }
    }
}
