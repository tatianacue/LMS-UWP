using Library.LMS.Models;
using Library.LMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

/* Tatiana Graciela Cue COP4870-0001*/
namespace App.LMS.Helpers
{
    public class CourseHelper
    {
        private CourseService courseService;
        public CourseHelper()
        {
            courseService = new CourseService();
        }
        private Course CoursePicker() //course selection menu
        {
            courseService.courseList.ForEach(Console.WriteLine);
            var code = Console.ReadLine() ?? string.Empty;
            var selected = courseService.courseList.FirstOrDefault(c => c.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase));
            return selected;
        }
        public void AddCourse() //adds course to courseList
        {
            var newCourse = new Course();

            Console.WriteLine("Enter a Course Code:");
            newCourse.Code = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Enter a Name:");
            newCourse.Name = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Enter a Description:");
            newCourse.Description = Console.ReadLine() ?? string.Empty;

            courseService.Add(newCourse);
            courseService.courseList.ForEach(Console.WriteLine); //lists all courses after adding a course
        }
        public void AddStudentToCourse(Person student) //takes in student and adds to course
        {
            Console.WriteLine("Which course do you want to add to?"); //pick course
            var selected = CoursePicker();

            selected.AddStudent(student);
            Console.WriteLine(student.Name + " added to " + selected.Code.ToUpper()); //says added to list
        }
        public void RemoveStudent() //removes student from course
        {
            Console.WriteLine("Which course do you want to remove student from?");
            var selected = CoursePicker();
            selected.RemoveStudent();
        }
        public void SearchForCourse(string ? srch = null) //searches for course and allows you to select it
        {
            if (string.IsNullOrEmpty(srch))
            {
                courseService.courseList.ForEach(Console.WriteLine);
            }
            else
            {
                courseService.Search(srch).ForEach(Console.WriteLine);
            }
            Console.WriteLine("Which course would you like to select? (Enter Code)");
            var code = Console.ReadLine() ?? string.Empty;
            var selected = courseService.courseList.FirstOrDefault(c => c.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase));
            if (selected != null)
            {
                Console.WriteLine(selected.DisplayAll());
            }
        }
        public void UpdateCourse() //update course menu and features
        {
            Console.WriteLine("Which course do you want to update?");
            var selected = CoursePicker();

            bool menu = true;
            while (menu)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Update Code");
                Console.WriteLine("2. Update Name");
                Console.WriteLine("3. Update Description");
                Console.WriteLine("4. Add Module");
                Console.WriteLine("5. Exit");

                string pick = Console.ReadLine() ?? string.Empty;

                if (int.TryParse(pick, out int pickInt))
                {
                    if (pickInt == 1) //update course code
                    {
                        Console.WriteLine("Enter new code:");
                        selected.Code = Console.ReadLine() ?? string.Empty;
                        Console.WriteLine("Updated info: " + selected);
                    }
                    else if (pickInt == 2) //update course name
                    {
                        Console.WriteLine("Enter new name:");
                        selected.Name = Console.ReadLine() ?? string.Empty;
                        Console.WriteLine("Updated info: " + selected);
                    }
                    else if (pickInt == 3) //update course description
                    {
                        Console.WriteLine("Enter new description");
                        selected.Description = Console.ReadLine() ?? string.Empty;
                        Console.WriteLine("Updated info: " + selected + " - " + selected.Description);
                    }
                    else if (pickInt == 4) //creates new module
                    {
                        Module tempMod = new Module(); //temporary new module to enter name and desc
                        Console.WriteLine("Enter module name:");
                        tempMod.Name = Console.ReadLine() ?? string.Empty;
                        Console.WriteLine("Enter module description:");
                        tempMod.Description = Console.ReadLine() ?? string.Empty;

                        selected.CreateModule(tempMod);
                    }
                    else if (pickInt == 5) //exits back to main menu
                    {
                        menu = false;
                    }
                }
            }
        }
        public void ListStudentCourses(Person student) //find student in course
        {
            var studentCourses = new List<Course>();
            foreach (var tempCourse in courseService.courseList)
            {
                bool real = tempCourse.FindStudent(student); //if student found in course then it will add to temp list
                if (real)
                {
                    studentCourses.Add(tempCourse);
                }
            }
            //lists courses in student's lists
            Console.WriteLine("\tCourses that " + student.Name + " is taking:");
            foreach (var course in studentCourses)
            {
                Console.WriteLine("\t" + course);
            }
        }
        public void AddAssignment() //add assignment to a course
        {
            Console.WriteLine("Which course do you want to add to?"); //pick course
            var selected = CoursePicker();

            var tempAssignment = new Assignment();
            Console.WriteLine("Enter name for assignment:"); //set name
            tempAssignment.Name = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Enter description for assignment:"); //set description
            tempAssignment.Description = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Enter total available points for assignment:"); //set total available points
            int points;
             while (!int.TryParse(Console.ReadLine(), out points))
            {
                Console.WriteLine("Invalid. Try Again.");
                int.TryParse(Console.ReadLine(), out points);
            }
            tempAssignment.TotalAvailablePoints = points;

            Console.WriteLine("Enter due date:");
            tempAssignment.DueDate = Console.ReadLine() ?? string.Empty;

            selected.AddAssignment(tempAssignment); //adds assignment to that specific course
        }

    }
}
