﻿using System;
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
            var personHelper = new PersonHelper();
            var courseHelper = new CourseHelper();
            bool cont = true;

            while (cont) //menu
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Show course menu"); //course
                Console.WriteLine("2. Show person menu"); //student
                Console.WriteLine("3. Show submission menu");
                Console.WriteLine("4. Quit"); //course

                string choice = Console.ReadLine() ?? string.Empty;
                if (int.TryParse(choice, out int choiceInt))
                {
                    if (choiceInt == 1) //show course menu
                    {
                        ShowCourseMenu(courseHelper, personHelper);
                    }
                    else if (choiceInt == 2) //show student menu
                    {
                        ShowPersonMenu(personHelper, courseHelper);
                    }
                    else if (choiceInt == 3)
                    {
                        ShowSubmissionMenu(courseHelper, personHelper);
                    }
                    else if (choiceInt == 4) //quit
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
                Console.WriteLine("6. Add assignment group");
                Console.WriteLine("7. Add assignment to course"); //course
                Console.WriteLine("8. List all courses"); //course
                Console.WriteLine("9. Search for course"); //course
                Console.WriteLine("10. Show announcement menu");
                Console.WriteLine("11. Exit");

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
                        courseHelper.AddAssignmentGroup();
                    }
                    else if (choiceInt == 7)
                    {
                        courseHelper.AddAssignment();
                    }
                    else if (choiceInt == 8) //list all courses
                    {
                        courseHelper.SearchForCourse();
                    }
                    else if (choiceInt == 9) //search for course
                    {
                        Console.WriteLine("Enter course name or description: ");
                        var srch = Console.ReadLine() ?? string.Empty;
                        courseHelper.SearchForCourse(srch);
                    }
                    else if (choiceInt == 10) //announcement menu
                    {
                        courseHelper.ShowAnnouncmentsMenu();
                    }
                    else if (choiceInt == 11) //quit
                    {
                        cont = false;
                    }
                }
            }
        }
        static void ShowPersonMenu(PersonHelper personHelper, CourseHelper courseHelper)
        {
            bool cont = true;
            while (cont) //menu
            {
                Console.WriteLine("PERSON MENU");
                Console.WriteLine("1. Add a person");
                Console.WriteLine("2. List all people");
                Console.WriteLine("3. List all students");
                Console.WriteLine("4. Search for student"); //student
                Console.WriteLine("5. List all courses for a student"); //student
                Console.WriteLine("6. Update student information"); //student
                Console.WriteLine("7. Show Assignment Grades");
                Console.WriteLine("8. Calculate course grade (Student)");
                Console.WriteLine("9. Calculate GPA");
                Console.WriteLine("10. Exit");

                string choice = Console.ReadLine() ?? string.Empty;
                if (int.TryParse(choice, out int choiceInt))
                {
                    if (choiceInt == 1)
                    {
                        personHelper.AddPerson();
                    }
                    else if (choiceInt == 2)
                    {
                        personHelper.ListOptions();
                    }
                    else if (choiceInt == 3) //list students
                    {
                        personHelper.SearchForStudent(courseHelper);
                    }
                    else if (choiceInt == 4)
                    {
                        Console.WriteLine("Enter student name:");
                        var srch = Console.ReadLine() ?? string.Empty;
                        personHelper.SearchForStudent(courseHelper, srch);
                    }
                    else if (choiceInt == 5)
                    {
                        personHelper.ListAllCourses(courseHelper);
                    }
                    else if (choiceInt == 6)
                    {
                        personHelper.UpdateStudent();
                    }
                    else if (choiceInt == 7)
                    {
                        personHelper.ListAssignmentGrades();
                    }
                    else if (choiceInt == 8)
                    {
                        courseHelper.CalculateGrade(personHelper.StudentPicker());
                    }
                    else if (choiceInt == 9)
                    {
                        personHelper.CalculateGPA(courseHelper);
                    }
                    else if (choiceInt == 10) //quit
                    {
                        cont = false;
                    }
                }
            }
        }
        static void ShowSubmissionMenu(CourseHelper courseHelper, PersonHelper personHelper)
        {
            bool cont = true;
            while (cont) //menu
            {
                Console.WriteLine("SUBMISSION MENU");
                Console.WriteLine("1. Submit an assignment");
                Console.WriteLine("2. Grade a submission");
                Console.WriteLine("3. Exit");

                string choice = Console.ReadLine() ?? string.Empty;
                if (int.TryParse(choice, out int choiceInt))
                {
                    if (choiceInt == 1)
                    {
                        courseHelper.AddSubmissionToCourse(personHelper.StudentPicker());
                    }
                    else if (choiceInt == 2)
                    {
                        courseHelper.AddGradeSubmission();
                    }
                    else if (choiceInt == 3)
                    {
                        cont = false;
                    }
                }
            }
        }
    }
}