using Library.LMS;
using Library.LMS.Models;
using Library.LMS.Models.Grading;
using Library.LMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
/* Tatiana Graciela Cue COP4870-0001*/
namespace App.LMS.Helpers
{
    public class PersonHelper
    {
        private ListNavigator<Person> listNavigator;
        private PersonService personService;
        private SubmissionHelper sHelper;
        public PersonHelper() {
            personService = new PersonService();
            sHelper = new SubmissionHelper();
            listNavigator = new ListNavigator<Person>(personService.personList);
        }
        public void AddPerson() //adds student to studentList
        {
            Console.WriteLine("Is this person a (S)tudent, (T)eaching Assistant, or (I)nstructor?");
            string choiceChar = Console.ReadLine() ?? string.Empty;
            if (choiceChar.Equals("s", StringComparison.InvariantCultureIgnoreCase))
            {
                var newStudent = new Student();
                Console.WriteLine("Enter an ID:");
                string tempID = Console.ReadLine() ?? string.Empty;
                tempID = tempID.ToUpper();
                newStudent.ID = tempID;
                bool check = personService.CheckID(newStudent.ID);
                while (!check) // checks if ID already exists
                {
                    Console.WriteLine("ID Already Exists. Enter another ID:");
                    tempID = Console.ReadLine() ?? string.Empty;
                    tempID = tempID.ToUpper();
                    newStudent.ID = tempID;
                    check = personService.CheckID(newStudent.ID);
                }
                Console.WriteLine("Enter a name:");
                newStudent.Name = Console.ReadLine() ?? string.Empty;

                Console.WriteLine("f = Freshman, " +
                    "s = Sophomore, " + "j = Junior, " + " e = Senior\n" +
                    "Enter a classification:");
                newStudent.Classification = Console.ReadLine() ?? string.Empty;
                personService.Add(newStudent);
            }
            else if (choiceChar.Equals("t", StringComparison.InvariantCultureIgnoreCase))
            {
                var newTA = new TeachingAssistant();
                Console.WriteLine("Enter an ID:");
                string tempID = Console.ReadLine() ?? string.Empty;
                tempID = tempID.ToUpper();
                newTA.ID = tempID;
                bool check = personService.CheckID(newTA.ID);
                while (!check) // checks if ID already exists
                {
                    Console.WriteLine("ID Already Exists. Enter another ID:");
                    tempID = Console.ReadLine() ?? string.Empty;
                    tempID = tempID.ToUpper();
                    newTA.ID = tempID;
                    check = personService.CheckID(newTA.ID);
                }
                Console.WriteLine("Enter a name:");
                newTA.Name = Console.ReadLine() ?? string.Empty;
                personService.Add(newTA);
            }
            else if (choiceChar.Equals("i", StringComparison.InvariantCultureIgnoreCase))
            {
                var newI = new Instructor();
                Console.WriteLine("Enter an ID:");
                string tempID = Console.ReadLine() ?? string.Empty;
                tempID = tempID.ToUpper();
                newI.ID = tempID;
                bool check = personService.CheckID(newI.ID);
                while (!check) // checks if ID already exists
                {
                    Console.WriteLine("ID Already Exists. Enter another ID:");
                    tempID = Console.ReadLine() ?? string.Empty;
                    tempID = tempID.ToUpper();
                    newI.ID = tempID;
                    check = personService.CheckID(newI.ID);
                }
                Console.WriteLine("Enter a name:");
                newI.Name = Console.ReadLine() ?? string.Empty;
                personService.Add(newI);
            }
        }
        public Student StudentPicker() { //lets user pick student and returns a student
            Console.WriteLine("SELECT A STUDENT:");
            var list = personService.personList.Where(s => s is Student).ToList();
            var tempList = new ListNavigator<Person>(list);
            ListOptions(tempList);
            Console.WriteLine("Which student do you select? (Enter ID)"); //pick student
            var id = Console.ReadLine() ?? string.Empty;
            var selected = personService.personList.FirstOrDefault(s => s.ID.Equals(id, StringComparison.InvariantCultureIgnoreCase));
            return (Student)selected;
        }
        public void SearchForStudent(CourseHelper helper, string? srch = null) //searches for student and allows selection
        {
            if (string.IsNullOrEmpty(srch))
            {
                var list = personService.personList.Where(s => s is Student).ToList(); //lists all students
                var tempList = new ListNavigator<Person>(list);
                ListOptions(tempList);
            }
            else
            {
                var list = personService.Search(srch);
                var tempList = new ListNavigator<Person>(list);
                ListOptions(tempList);
            }
            Console.WriteLine("Which student would you like to select? (Enter ID)");
            var code = Console.ReadLine() ?? string.Empty;
            var selected = personService.personList.FirstOrDefault(s => s.ID.Equals(code, StringComparison.InvariantCultureIgnoreCase) && s is Student);
            if (selected != null)
            {
                DisplayAll((Student)selected);
                helper.ListStudentCourses((Student)selected);
            }
        }
        public void CalculateGPA(CourseHelper helper)
        {
            Dictionary<Course, double> CourseGrades = new Dictionary<Course, double>(); //holds all course grades
            var student = StudentPicker();
            var courses = helper.ListStudentCourses(student); //gets courses student is taking
            foreach (var course in courses)
            {
                double grade = sHelper.CalculateCourseGrade(student, course);
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
            
            Console.WriteLine(student.Name + " GPA: " + Gpa.ToString("F2")); //added precision sort of, check again if works 
        }
        public double ConvertGpaScale(double courseGrade)
        {
            if (courseGrade >= 96)
            {
                return 4.0;
            }
            else if (courseGrade <= 95 && courseGrade >= 91)
            {
                return 4.0;
            }
            else if (courseGrade <= 91 && courseGrade >= 90)
            {
                return 3.7;
            }
            else if (courseGrade <= 89 && courseGrade >= 86)
            {
                return 3.3;
            }
            else if (courseGrade <= 85 && courseGrade >= 81)
            {
                return 3.0;
            }
            else if (courseGrade <= 81 && courseGrade >= 80)
            {
                return 2.7;
            }
            else if (courseGrade <= 79 && courseGrade >= 76)
            {
                return 2.3;
            }
            else if (courseGrade <= 75 && courseGrade >= 71)
            {
                return 2.0;
            }
            else if (courseGrade <= 71 && courseGrade >= 70)
            {
                return 1.7;
            }
            else if (courseGrade <= 69 && courseGrade >= 66)
            {
                return 1.3;
            }
            else if (courseGrade <= 65 && courseGrade >= 61)
            {
                return 1.0;
            }
            else if (courseGrade <= 61 && courseGrade >= 60)
            {
                return 0.7;
            }
            else
            {
                return 0;
            }
        }
        public void ListAssignmentGrades()
        {
            var student = StudentPicker();
            Console.WriteLine("All Assignment Grades:");
            foreach (var pair in student.AssignmentGrades)
            {
                Console.WriteLine(pair.Key.Name + " " + pair.Value + "%"); //assignment name and then percent grade
            }
        }
        public void ListAllCourses(CourseHelper helper)
        {
            var studentCourses = helper.ListStudentCourses(StudentPicker());
            Console.WriteLine("Courses that student is taking:");
            studentCourses.ForEach(Console.WriteLine);
        }
        public void UpdateStudent() //student update menu
        {
            var selected = StudentPicker();
            bool menu = true;
            while (menu)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Update Name");
                Console.WriteLine("2. Update Classification");
                Console.WriteLine("3. Update ID");
                Console.WriteLine("4. Exit");

                string pick = Console.ReadLine() ?? string.Empty;

                if (int.TryParse(pick, out int pickInt))
                {
                    if (pickInt == 1) //updates student name
                    {
                        Console.WriteLine("Enter a new name:");
                        selected.Name = Console.ReadLine() ?? string.Empty;
                        Console.WriteLine("Updated info: " + selected); //displays updated info

                    }
                    else if (pickInt == 2) //updates student classification
                    {
                        Console.WriteLine("f = Freshman, " +
                        "s = Sophomore, " + "j = Junior, " + " e = Senior\n" +
                        "Enter a new classification:");
                        selected.Classification = Console.ReadLine() ?? string.Empty;
                        Console.WriteLine("Updated info: " + selected.Classification);
                    }
                    else if (pickInt == 3)
                    {
                        string oldID = selected.ID; //saves old code to remove from key list

                        Console.WriteLine("Enter new code:");
                        string tempID = Console.ReadLine() ?? string.Empty;
                        tempID = tempID.ToUpper();
                        selected.ID = tempID;
                        bool check = personService.CheckID(selected.ID);
                        while (!check) // checks if code already exists
                        {
                            Console.WriteLine("Course Code Already Exists. Enter another one:");
                            tempID = Console.ReadLine() ?? string.Empty;
                            tempID = tempID.ToUpper();
                            selected.ID = tempID;
                            check = personService.CheckID(selected.ID);
                        }
                        personService.IDDictionary.Remove(oldID); //removes old code and frees it up for possible use later
                        Console.WriteLine("Updated info: " + selected);
                    }
                    else if (pickInt == 4) //exits back to main menu
                    {
                        menu = false;
                    }
                }
            }
        }
        public void DisplayAll(Student student)
        {
            Console.WriteLine("\t\tSTUDENT DETAILS");
            Console.WriteLine("\tID: " + student.ID);
            Console.WriteLine("\tName: " + student.Name);
            Console.WriteLine("\tClassification: " + student.Classification);

        }
        public void ListOptions(ListNavigator<Person>? list = null)
        {
            ListNavigator<Person> navigator;
            if (list != null)
            {
                navigator = list; //if its another list
            }
            else
            {
                navigator = listNavigator; //make it normal list
            }
            var display = navigator.GetCurrentPage();
            foreach (var person in display)
            {
                Console.WriteLine(person.Value);
            }
            bool menu = true;
            while (menu)
            {
                Console.WriteLine("(1)BACK    (2)NEXT     (3)SELECT/EXIT");
                string pick = Console.ReadLine() ?? string.Empty;

                if (int.TryParse(pick, out int pickInt))
                {
                    if (pickInt == 1) //goes back
                    {
                        display = navigator.GoBackward();
                        foreach (var person in display)
                        {
                            Console.WriteLine(person.Value);
                        }
                    }
                    else if (pickInt == 2) //go next
                    {
                        display = navigator.GoForward();
                        foreach (var person in display)
                        {
                            Console.WriteLine(person.Value);
                        }
                    }
                    else if (pickInt == 3) //exits back to main menu
                    {
                        menu = false;
                    }
                }
            }
        }
    }
}
