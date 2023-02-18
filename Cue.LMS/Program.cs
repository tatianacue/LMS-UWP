using System;
using App.LMS.Helpers;
using Library.LMS;
using Library.LMS.Services;

/* Tatiana Graciela Cue COP4870-0001*/

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var studentHelper = new PersonHelper();
            var courseHelper = new CourseHelper();
            bool cont = true;

            while (cont) //menu
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Add a course");
                Console.WriteLine("2. Add a student");
                Console.WriteLine("3. List all courses");
                Console.WriteLine("4. List all students");
                Console.WriteLine("5. Add student to course");
                Console.WriteLine("6. Remove student from course");
                Console.WriteLine("7. Search for course");
                Console.WriteLine("8. Search for student");
                Console.WriteLine("9. List all courses for a student");
                Console.WriteLine("10. Update course information");
                Console.WriteLine("11. Update student information");
                Console.WriteLine("12. Add assignment to course");
                Console.WriteLine("13. Quit");

                string choice = Console.ReadLine() ?? string.Empty;

                if (int.TryParse(choice, out int choiceInt))
                {
                    if (choiceInt == 1) //create course and add to list
                    {
                        courseHelper.AddCourse();
                    }
                    else if (choiceInt == 2) //create student and add to list
                    {
                        studentHelper.AddStudent();
                    }
                    else if (choiceInt == 3) //lists all courses
                    {
                        courseHelper.SearchForCourse(); //lists if null
                    }
                    else if (choiceInt == 4) //lists all students
                    {
                        studentHelper.SearchForStudent(courseHelper);
                    }
                    else if (choiceInt == 5) //add student to course
                    {
                        courseHelper.AddStudentToCourse(studentHelper.StudentPicker());
                    }
                    else if (choiceInt == 6) //removes student from course
                    {
                        courseHelper.RemoveStudent();
                    }
                    else if (choiceInt == 7) //search for course
                    {
                        Console.WriteLine("Enter course name or description: ");
                        var srch = Console.ReadLine() ?? string.Empty;
                        courseHelper.SearchForCourse(srch);
                    }
                    else if (choiceInt == 8) //search for student
                    {
                        Console.WriteLine("Enter student name:");
                        var srch = Console.ReadLine() ?? string.Empty;
                        studentHelper.SearchForStudent(courseHelper, srch);
                    }
                    else if (choiceInt == 9) //list all courses student is taking
                    {
                        courseHelper.ListStudentCourses(studentHelper.StudentPicker());
                    }
                    else if (choiceInt == 10) //update course info
                    {
                        courseHelper.UpdateCourse();
                    }
                    else if (choiceInt == 11) //update student information
                    {
                        studentHelper.UpdateStudent();
                    }
                    else if (choiceInt == 12) //adds assignment
                    {
                        courseHelper.AddAssignment();
                    }
                    else if (choiceInt == 13) //quit
                    {
                        cont = false;
                    }
                }

            }
        }
    }
}