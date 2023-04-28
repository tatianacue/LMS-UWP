using Library.LMS.Models;
using Library.LMS.Models.Grading;
using Library.LMS.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using UWP.CueLMS.Dialogs.StudentViewDialogs;
using UWP.Library.CueLMS;
/* Tatiana Graciela Cue COP4870-0001*/
namespace UWP.CueLMS.ViewModels
{
    public class StudentViewModel : ObservableObject
    {
        public StudentViewModel(CourseService c, PersonService p, Person s)
        {
            Semester = 1; //default spring semester
            StudentCourseList = new List<Course>();
            Student = s;
            CourseService = c;
            PersonService = p;
            AllCurrentCourses = CourseService.SpringList; //default semester is spring for now
            StudentCourses(); //finds all student courses
            CurrentCollection = new ObservableCollection<Course>(StudentCourseList);
            SemesterName = "Spring"; //default display text
            ListName = "Current Courses:";
            ButtonEnable = true;
        }
        //database lists
        public List<Student> rosterList
        {
            get
            {
                var id = tempSelectCourse.Id;
                if (Semester == 1) //spring
                {
                    var payload = new WebRequestHandler().Get($"http://localhost:5100/SpringCourse/GetRoster/{id}").Result;
                    return JsonConvert.DeserializeObject<List<Student>>(payload).OrderBy(x => x.IdNumber).ToList();
                }
                else if (Semester == 2) //summer
                {
                    var payload = new WebRequestHandler().Get($"http://localhost:5100/SummerCourse/GetRoster/{id}").Result;
                    return JsonConvert.DeserializeObject<List<Student>>(payload).OrderBy(x => x.IdNumber).ToList();
                }
                else //fall
                {
                    var payload = new WebRequestHandler().Get($"http://localhost:5100/FallCourse/GetRoster/{id}").Result;
                    return JsonConvert.DeserializeObject<List<Student>>(payload).OrderBy(x => x.IdNumber).ToList();
                }
            }
        }
        //more
        public int Semester { get; set; }
        public Person Student { get; set; }
        public PersonService PersonService { get; set; }
        public CourseService CourseService { get; set; }
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
        public Course tempSelectCourse { get; set; }
        private bool buttonenable { get; set; }
        public bool ButtonEnable
        {
            get { return buttonenable; }
            set 
            { 
                buttonenable = value;
                NotifyPropertyChanged(nameof(ButtonEnable));
            }
        }
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
        public string SemesterName { get; set; } //current semester name display
        public string ListName { get; set; } //current list title display
        private void StudentCourses() //adds to student course list
        {
            foreach (var course in AllCurrentCourses) 
            {
                tempSelectCourse = course;
                foreach(var person in rosterList)
                {
                    if(person.IdNumber == Student.IdNumber)
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
        public async void ViewPastSemesters()
        {
            var dialog = new SemesterSwitchDialog(this);
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            StudentCourseList.Clear();
            StudentCourses(); //updates student courses list
            AutoRefresh(); //updates collection to display new semester
            NotifyPropertyChanged(nameof(SemesterName));
            NotifyPropertyChanged(nameof(ListName));
        }
        public void SwitchSemesters(int choice)
        {
            if (choice == 1) //switch to spring
            {
                Semester = 1;
                AllCurrentCourses = CourseService.SpringList;
                SemesterName = "Spring";
                ListName = "Current Courses:";
                ButtonEnable = true; //enables buttons because spring is current
            }
            else if (choice == 2) //switch to summer
            {
                Semester = 2;
                AllCurrentCourses = CourseService.SummerList;
                SemesterName = "Summer";
                ListName = "Past Courses:";
                ButtonEnable = false; //disables buttons because summer is past
            }
            else if (choice == 3) //switch to fall
            {
                Semester = 3;
                AllCurrentCourses = CourseService.FallList;
                SemesterName = "Fall";
                ListName = "Past Courses:";
                ButtonEnable = false; //disables buttons because fall is past
            }
        }
        //grade and gpa
        public double CalculateCourseGrade()
        {
            if (SelectedCourse != null)
            {
                var student = (Student)Student;

                //gets submissions from database
                var id = SelectedCourse.Id;
                var payload = new WebRequestHandler().Get($"http://localhost:5100/Submission/GetList/{id}").Result;
                var submissions = JsonConvert.DeserializeObject<List<Submission>>(payload).OrderBy(x => x.Id).ToList();

                //get groups from database
                var newpayload = new WebRequestHandler().Get($"http://localhost:5100/AssignmentGroup/GetList/{id}").Result;
                var assignmentgroups = JsonConvert.DeserializeObject<List<AssignmentGroup>>(newpayload).OrderBy(x => x.Id).ToList();

                var tempList = submissions.Where(s => s.Student.IdNumber == student.IdNumber).ToList(); //list of submissions by specific student
                Dictionary<AssignmentGroup, double> GroupGrades = new Dictionary<AssignmentGroup, double>();
                double courseGrade = 0;
                foreach (var group in assignmentgroups) //each group in course
                {
                    double totalGrades = 0;
                    double totalPoints = 0;
                    var groupid = group.Id;
                    var temppayload = new WebRequestHandler().Get($"http://localhost:5100/AssignmentGroup/GetAssignments/{groupid}").Result;
                    var assignments = JsonConvert.DeserializeObject<List<Assignment>>(temppayload).ToList(); //assignments in group

                    foreach (var submission in tempList) //for each submission
                    {
                        foreach (var assignment in assignments) //each assignment in group
                        {
                            if (submission.Assignment.Name.Equals(assignment.Name))
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
                //adds grade to fake database
                student.TempGrade = courseGrade;
                student.TempCourse = SelectedCourse;
                PostGrade(student);

                return courseGrade;
            }
            else { return 0; }
        }
        private async void PostGrade(Student student) //posts grade to fake database
        {
            var handler = new WebRequestHandler();
            await handler.Post("http://localhost:5100/Person/AddStudentGrade", student, HttpMethod.Post);
        }
        public string CourseGrade 
        { get 
            { 
                var grade = CalculateCourseGrade();
                return $"{grade.ToString("F1")}% {ConvertLetterGrade(grade)}"; 
            } 
        }
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
        private string ConvertLetterGrade(double courseGrade)
        {
            if (courseGrade >= 96) //A+
            {
                return "A+";
            }
            else if (courseGrade <= 95 && courseGrade >= 91) //A
            {
                return "A";
            }
            else if (courseGrade <= 91 && courseGrade >= 90) //A-
            {
                return "A-";
            }
            else if (courseGrade <= 89 && courseGrade >= 86) //B+
            {
                return "B+";
            }
            else if (courseGrade <= 85 && courseGrade >= 81) //B
            {
                return "B";
            }
            else if (courseGrade <= 81 && courseGrade >= 80) //B-
            {
                return "B-";
            }
            else if (courseGrade <= 79 && courseGrade >= 76) //C+
            {
                return "C+";
            }
            else if (courseGrade <= 75 && courseGrade >= 71) //C
            {
                return "C";
            }
            else if (courseGrade <= 71 && courseGrade >= 70) //C-
            {
                return "C-";
            }
            else if (courseGrade <= 69 && courseGrade >= 66) //D+
            {
                return "D+";
            }
            else if (courseGrade <= 65 && courseGrade >= 61) //D
            {
                return "D";
            }
            else if (courseGrade <= 61 && courseGrade >= 60) //D-
            {
                return "D-";
            }
            else //F
            {
                return "F";
            }
        }
    }
}
