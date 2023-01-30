using System;
using Library.LMS;

/* Tatiana Graciela Cue COP4870-0001*/

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var courseList = new List<Course>();
            var studentList = new List<Person>();
            bool cont = true;

            while (cont)
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

                string choice = Console.ReadLine() ?? string.Empty;

                if (int.TryParse(choice, out int choiceInt))
                {
                    if (choiceInt == 1) //create course and add to list
                    {
                        var newCourse = new Course();

                        Console.WriteLine("Enter a Course Code:");
                        newCourse.Code = Console.ReadLine() ?? string.Empty;

                        Console.WriteLine("Enter a Name:");
                        newCourse.Name = Console.ReadLine() ?? string.Empty;

                        Console.WriteLine("Enter a Description:");
                        newCourse.Description = Console.ReadLine() ?? string.Empty;

                        courseList.Add(newCourse);
                    }
                    else if (choiceInt == 2) //create student and add to list
                    {
                        var newStudent = new Person();

                        Console.WriteLine("Enter a name:");
                        newStudent.Name = Console.ReadLine() ?? string.Empty;

                        Console.WriteLine("f = Freshman, " +
                            "s = Sophomore, " + "j = Junior, " + " e = Senior\n" +
                            "Enter a classification:");
                        newStudent.Classification = Console.ReadLine() ?? string.Empty;

                        studentList.Add(newStudent);
                    }
                    else if (choiceInt == 3) //lists all courses
                    {
                        foreach (var course in courseList)
                        {
                            Console.WriteLine(course.Code + "\t\t" + course.Name);
                        }
                    }
                    else if (choiceInt == 4) //lists all students
                    {
                        foreach (var student in studentList)
                        {
                            Console.WriteLine(student.Name + "\t\t" + student.Classification);
                        }
                    }
                    else if (choiceInt == 5) //add student to course
                    {
                        Console.WriteLine("Enter course code of course you want to add to:");
                        string ccode = Console.ReadLine() ?? string.Empty;
                        int index = courseList.FindIndex(t => t.Code.Contains(ccode, StringComparison.InvariantCultureIgnoreCase));

                        Console.WriteLine("Enter a student name:");
                        string nname = Console.ReadLine() ?? string.Empty;
                        int indx = studentList.FindIndex(a => a.Name.Contains(nname, StringComparison.InvariantCultureIgnoreCase));

                        var tempStudent = studentList[indx];
                        courseList[index].AddStudent(tempStudent);
                    }
                    else if (choiceInt == 6) //removes student from course
                    {
                        Console.WriteLine("Enter course code of course you want to remove from:");
                        string ccode = Console.ReadLine() ?? string.Empty;
                        int index = courseList.FindIndex(t => t.Code.Contains(ccode, StringComparison.InvariantCultureIgnoreCase));

                        Console.WriteLine("Enter a student name:");
                        string nname = Console.ReadLine() ?? string.Empty;
                        int indx = studentList.FindIndex(a => a.Name.Contains(nname, StringComparison.InvariantCultureIgnoreCase));

                        var tempStudent = studentList[indx];
                        courseList[index].RemoveStudent(tempStudent);
                    }
                    else if (choiceInt == 7) //search for course
                    {
                        Console.WriteLine("Enter a course name or description you want to find:");
                        string search = Console.ReadLine() ?? string.Empty;
                        var searchCourse = courseList.Where(t => t.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase) ||
                        t.Description.Contains(search, StringComparison.InvariantCultureIgnoreCase));

                        foreach (var course in searchCourse)
                        {
                            Console.WriteLine(course.Code + "\t\t" + course.Name);
                        }

                        //select a course and expand details
                        Console.WriteLine("Which course would you like to select? Enter code:");
                        string ccode = Console.ReadLine() ?? string.Empty;
                        int index = courseList.FindIndex(t => t.Code.Contains(ccode, StringComparison.InvariantCultureIgnoreCase));

                        Console.WriteLine(courseList[index].Code + "\n" + courseList[index].Name + "\n"
                            + courseList[index].Description + "\n Students in course:");
                        courseList[index].ListStudents();
                    }
                    else if (choiceInt == 8) //search for student
                    {
                        Console.WriteLine("Enter a student name you want to find:");
                        string search = Console.ReadLine() ?? string.Empty;
                        var searchStudent = studentList.Where(t => t.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase));

                        foreach (var student in searchStudent)
                        {
                            Console.WriteLine(student.Name + "\t\t" + student.Classification);
                        }
                    }
                    else if (choiceInt == 9) //list all courses student is taking
                    {
                        Console.WriteLine("What is the name of the student you want to see all courses for?");
                        string sstudent = Console.ReadLine() ?? string.Empty;

                        var studentCourses = new List<Course>();
                        foreach (var tempCourse in courseList)
                        {
                            bool real = tempCourse.FindStudent(sstudent); //if student found in course then it will add to temp list
                            if (real)
                            {
                                studentCourses.Add(tempCourse);
                            }
                        }
                        //lists courses in student's lists
                        Console.WriteLine("Courses that " + sstudent + " is taking:");
                        foreach (var course in studentCourses)
                        {
                            Console.WriteLine(course.Code + "\t\t" + course.Name);
                        }
                    }
                    else if (choiceInt == 10) //update course info
                    {

                    }
                }
            }
        }
    }
}