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
        private int CoursePicker() //course selection menu
        {
            int displayNum = 1; //displays courses starting from 1...
            foreach (var course in courseService.courseList)
            {
                Console.WriteLine(displayNum + ". " + course);
                displayNum++;
            }

            int courseNum;
            while (!int.TryParse(Console.ReadLine(), out courseNum))
            {
                Console.WriteLine("Not a valid selection. Try Again.");
                int dn = 1;
                foreach (var course in courseService.courseList)
                {
                    Console.WriteLine(dn + ". " + course);
                    dn++;
                }
                int.TryParse(Console.ReadLine(), out courseNum);
            }

            courseNum--; //to get array index
            return courseNum;
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
        public void ListAllCourses() //lists all courses in course list
        {
            Console.WriteLine("All COURSES:");
            courseService.courseList.ForEach(Console.WriteLine);
        }

        public void AddStudentToCourse(Person student) //takes in student and adds to course
        {
            Console.WriteLine("Which course do you want to add to?"); //pick course
            int courseNum = CoursePicker();

            courseService.courseList[courseNum].AddStudent(student);
            Console.WriteLine(student.Name + " added to " + courseService.courseList[courseNum].Code.ToUpper()); //says added to list
        }
        public void RemoveStudent() //removes student from course
        {
            Console.WriteLine("Which course do you want to remove student from?");
            int displayNum = 1;
            foreach (var course in courseService.courseList)
            {
                Console.WriteLine(displayNum + ". " + course);
                displayNum++;
            }

            int courseNum;
            while (!int.TryParse(Console.ReadLine(), out courseNum))
            {
                Console.WriteLine("Not a valid selection. Try Again.");
                int dn = 1;
                foreach (var course in courseService.courseList)
                {
                    Console.WriteLine(dn + ". " + course);
                    dn++;
                }
                int.TryParse(Console.ReadLine(), out courseNum);
            }

            courseNum--;
            courseService.courseList[courseNum].RemoveStudent();
        }
        public void SearchForCourse() //searches for course and allows you to select it
        {
            Console.WriteLine("Enter a course name or description you want to find:");
            string search = Console.ReadLine() ?? string.Empty;
            var searchCourse = courseService.courseList.Where(t => t.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase) ||
            t.Description.Contains(search, StringComparison.InvariantCultureIgnoreCase));
            List<Course> results = searchCourse.ToList(); //enumerable to list

            //select a course and expand details
            Console.WriteLine("Which course would you like to see detailed view?");
            int displayNum = 1;
            foreach (var course in results)
            {
                Console.WriteLine(displayNum + ". " + course);
                displayNum++;
            }

            int courseNum;
            while (!int.TryParse(Console.ReadLine(), out courseNum))
            {
                Console.WriteLine("Not a valid selection. Try Again.");
                int dn = 1;
                foreach (var course in results)
                {
                    Console.WriteLine(dn + ". " + course);
                    dn++;
                }
                int.TryParse(Console.ReadLine(), out courseNum);
            }

            courseNum--;

            results[courseNum].DisplayAll();
        }
        public void UpdateCourse() //update course menu and features
        {
            Console.WriteLine("Which course do you want to update?");
            int courseNum = CoursePicker();

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
                        courseService.courseList[courseNum].Code = Console.ReadLine() ?? string.Empty;
                        Console.WriteLine("Updated info: " + courseService.courseList[courseNum]);
                    }
                    else if (pickInt == 2) //update course name
                    {
                        Console.WriteLine("Enter new name:");
                        courseService.courseList[courseNum].Name = Console.ReadLine() ?? string.Empty;
                        Console.WriteLine("Updated info: " + courseService.courseList[courseNum]);
                    }
                    else if (pickInt == 3) //update course description
                    {
                        Console.WriteLine("Enter new description");
                        courseService.courseList[courseNum].Description = Console.ReadLine() ?? string.Empty;
                        Console.WriteLine("Updated info: " + courseService.courseList[courseNum] + " - " + courseService.courseList[courseNum].Description);
                    }
                    else if (pickInt == 4) //creates new module
                    {
                        Module tempMod = new Module(); //temporary new module to enter name and desc
                        Console.WriteLine("Enter module name:");
                        tempMod.Name = Console.ReadLine() ?? string.Empty;
                        Console.WriteLine("Enter module description:");
                        tempMod.Description = Console.ReadLine() ?? string.Empty;

                        courseService.courseList[courseNum].CreateModule(tempMod);
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
            int courseNum = CoursePicker();

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

            courseService.courseList[courseNum].AddAssignment(tempAssignment); //adds assignment to that specific course
        }

    }
}
