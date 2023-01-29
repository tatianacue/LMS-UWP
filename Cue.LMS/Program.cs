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

                string choice = Console.ReadLine() ?? string.Empty;

                if (int.TryParse(choice, out int choiceInt))
                {
                    if (choiceInt == 1)
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
                    else if (choiceInt == 2)
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
                    else if (choiceInt == 3)
                    {
                        foreach (var course in courseList)
                        {
                            var name = course.Name;
                            var code = course.Code;
                            var description = course.Description;
                            Console.WriteLine(name);
                            Console.WriteLine(code);
                            Console.WriteLine(description);
                        }
                    }
                    else if (choiceInt == 4)
                    {
                        foreach (var student in studentList)
                        {
                            Console.WriteLine(student.Name + "\t" + student.Classification);
                        }
                    }
                    else if (choiceInt == 5)
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
                }
            }
        }
    }
}