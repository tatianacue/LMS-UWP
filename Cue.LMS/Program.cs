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
                Console.WriteLine("1. Show course menu"); //course
                Console.WriteLine("2. Show student menu"); //student
                Console.WriteLine("3. Quit"); //course

                string choice = Console.ReadLine() ?? string.Empty;
                if (int.TryParse(choice, out int choiceInt))
                {
                    if (choiceInt == 1) //show course menu
                    {
                        ShowCourseMenu(courseHelper, studentHelper);
                    }
                    else if (choiceInt == 2) //show student menu
                    {
                        ShowStudentMenu(studentHelper, courseHelper);
                    }
                    else if (choiceInt == 3) //quit
                    {
                        cont = false;
                    }
                }
            }
        }
        static void ShowCourseMenu(CourseHelper courseHelper, PersonHelper studentHelper)
        {
            bool cont = true;
            while (cont) //menu
            {
                Console.WriteLine("COURSE MENU");
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Add a course"); //course
                Console.WriteLine("2. Update course information"); //course
                Console.WriteLine("3. Add student to course"); //course
                Console.WriteLine("4. Manage Modules");
                Console.WriteLine("5. Remove student from course");
                Console.WriteLine("6. Add assignment to course"); //course
                Console.WriteLine("7. List all courses"); //course
                Console.WriteLine("8. Search for course"); //course
                Console.WriteLine("9. Exit");

                string choice = Console.ReadLine() ?? string.Empty;
                if (int.TryParse(choice, out int choiceInt))
                {
                    if (choiceInt == 1)
                    {
                        courseHelper.AddCourse();
                    }
                    else if (choiceInt == 2)
                    {
                        courseHelper.UpdateCourse();
                    }
                    else if (choiceInt == 3)
                    {
                        courseHelper.AddStudentToCourse(studentHelper.StudentPicker());
                    }
                    else if (choiceInt == 4)
                    {
                        courseHelper.UpdateModule();
                    }
                    else if (choiceInt == 5)
                    {
                        courseHelper.RemoveStudent();
                    }
                    else if (choiceInt == 6)
                    {
                        courseHelper.AddAssignment();
                    }
                    else if (choiceInt == 7) //list all courses
                    {
                        courseHelper.SearchForCourse();
                    }
                    else if (choiceInt == 8) //search for course
                    {
                        Console.WriteLine("Enter course name or description: ");
                        var srch = Console.ReadLine() ?? string.Empty;
                        courseHelper.SearchForCourse(srch);
                    }
                    else if (choiceInt == 9) //quit
                    {
                        cont = false;
                    }
                }
            }
        }
        static void ShowStudentMenu(PersonHelper studentHelper, CourseHelper courseHelper)
        {
            bool cont = true;
            while (cont) //menu
            {
                Console.WriteLine("STUDENT MENU");
                Console.WriteLine("1. Add a student"); //student
                Console.WriteLine("2. List all students");//student
                Console.WriteLine("3. Search for student"); //student
                Console.WriteLine("4. List all courses for a student"); //student
                Console.WriteLine("5. Update student information"); //student
                Console.WriteLine("6. Exit");

                string choice = Console.ReadLine() ?? string.Empty;
                if (int.TryParse(choice, out int choiceInt))
                {
                    if (choiceInt == 1)
                    {
                        studentHelper.AddStudent();
                    }
                    else if (choiceInt == 2)
                    {
                        studentHelper.SearchForStudent(courseHelper);
                    }
                    else if (choiceInt == 3)
                    {
                        Console.WriteLine("Enter student name:");
                        var srch = Console.ReadLine() ?? string.Empty;
                        studentHelper.SearchForStudent(courseHelper, srch);
                    }
                    else if (choiceInt == 4)
                    {
                        courseHelper.ListStudentCourses(studentHelper.StudentPicker());
                    }
                    else if (choiceInt == 5)
                    {
                        studentHelper.UpdateStudent();
                    }
                    else if (choiceInt == 6) //quit
                    {
                        cont = false;
                    }
                }
            }
        }
    }
}