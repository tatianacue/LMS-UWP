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
                Console.WriteLine("11. Update student information");

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
                        Console.WriteLine("Which course do you want to add to?"); //pick course
                        int displayNum = 1;
                        foreach (var course in courseList)
                        {
                            Console.WriteLine(displayNum + ". " + course.Code + "\t\t" + course.Name);
                            displayNum++;
                        }

                        int courseNum;
                        while (!int.TryParse(Console.ReadLine(), out courseNum))
                        {
                            Console.WriteLine("Not a valid selection. Try Again.");
                            int dn = 1;
                            foreach (var course in courseList)
                            {
                                Console.WriteLine(dn + ". " + course.Code + "\t\t" + course.Name);
                                dn++;
                            }
                            int.TryParse(Console.ReadLine(), out courseNum);
                        }

                        courseNum--;
                        Console.WriteLine("Which student do you want to add to " + courseList[courseNum].Code + "?"); //pick student
                        int num = 1;
                        foreach (var student in studentList)
                        {
                            Console.WriteLine(num + ". " + student.Name);
                            num++;
                        }

                        int studentNum;
                        while (!int.TryParse(Console.ReadLine(), out studentNum))
                        {
                            Console.WriteLine("Not a valid selection. Try Again.");
                            int dn = 1;
                            foreach (var student in studentList)
                            {
                                Console.WriteLine(num + ". " + student.Name);
                                dn++;
                            }
                            int.TryParse(Console.ReadLine(), out studentNum);
                        }
                        studentNum--;

                        courseList[courseNum].AddStudent(studentList[studentNum]);
                    }
                    else if (choiceInt == 6) //removes student from course
                    {
                        Console.WriteLine("Which course do you want to remove student from?");
                        int displayNum = 1;
                        foreach (var course in courseList)
                        {
                            Console.WriteLine(displayNum + ". " + course.Code + "\t\t" + course.Name);
                            displayNum++;
                        }

                        int courseNum;
                        while (!int.TryParse(Console.ReadLine(), out courseNum))
                        {
                            Console.WriteLine("Not a valid selection. Try Again.");
                            int dn = 1;
                            foreach (var course in courseList)
                            {
                                Console.WriteLine(dn + ". " + course.Code + "\t\t" + course.Name);
                                dn++;
                            }
                            int.TryParse(Console.ReadLine(), out courseNum);
                        }

                        courseNum--;
                        courseList[courseNum].RemoveStudent();
                    }
                    else if (choiceInt == 7) //search for course
                    {
                        Console.WriteLine("Enter a course name or description you want to find:");
                        string search = Console.ReadLine() ?? string.Empty;
                        var searchCourse = courseList.Where(t => t.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase) ||
                        t.Description.Contains(search, StringComparison.InvariantCultureIgnoreCase));

                        //select a course and expand details
                        Console.WriteLine("Which course would you like to select?");
                        int displayNum = 1;
                        foreach (var course in searchCourse) //goes through searched courses list
                        {
                            Console.WriteLine(displayNum + ". " + course.Code + "\t\t" + course.Name);
                            displayNum++;
                        }

                        int courseNum;
                        while (!int.TryParse(Console.ReadLine(), out courseNum))
                        {
                            Console.WriteLine("Not a valid selection. Try Again.");
                            int dn = 1;
                            foreach (var course in searchCourse)
                            {
                                Console.WriteLine(dn + ". " + course.Code + "\t\t" + course.Name);
                                dn++;
                            }
                            int.TryParse(Console.ReadLine(), out courseNum);
                        }

                        courseNum--;

                        Console.WriteLine(courseList[courseNum].Code + "\n" + courseList[courseNum].Name + "\n"
                            + courseList[courseNum].Description + "\n Students in course:");
                        courseList[courseNum].ListStudents();
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
                        Console.WriteLine("Which student do you want to list all courses for?");
                        int num = 1;
                        foreach (var student in studentList)
                        {
                            Console.WriteLine(num + ". " + student.Name);
                            num++;
                        }

                        int studentNum;
                        while (!int.TryParse(Console.ReadLine(), out studentNum))
                        {
                            Console.WriteLine("Not a valid selection. Try Again.");
                            int dn = 1;
                            foreach (var student in studentList)
                            {
                                Console.WriteLine(num + ". " + student.Name);
                                dn++;
                            }
                            int.TryParse(Console.ReadLine(), out studentNum);
                        }
                        studentNum--;

                        var studentCourses = new List<Course>();
                        foreach (var tempCourse in courseList)
                        {
                            bool real = tempCourse.FindStudent(studentList[studentNum]); //if student found in course then it will add to temp list
                            if (real)
                            {
                                studentCourses.Add(tempCourse);
                            }
                        }
                        //lists courses in student's lists
                        Console.WriteLine("Courses that " + studentList[studentNum].Name + " is taking:");
                        foreach (var course in studentCourses)
                        {
                            Console.WriteLine(course.Code + "\t\t" + course.Name);
                        }
                    }
                    else if (choiceInt == 10) //update course info
                    {
                        Console.WriteLine("Which course do you want to update?");
                        int displayNum = 1;
                        foreach (var course in courseList)
                        {
                            Console.WriteLine(displayNum + ". " + course.Code + "\t\t" + course.Name);
                            displayNum++;
                        }

                        int courseNum;
                        while (!int.TryParse(Console.ReadLine(), out courseNum))
                        {
                            Console.WriteLine("Not a valid selection. Try Again.");
                            int dn = 1;
                            foreach (var course in courseList)
                            {
                                Console.WriteLine(dn + ". " + course.Code + "\t\t" + course.Name);
                                dn++;
                            }
                            int.TryParse(Console.ReadLine(), out courseNum);
                        }

                        courseNum--;

                        bool menu = true;
                        while(menu)
                        {
                            Console.WriteLine("Choose an option:");
                            Console.WriteLine("1. Update Code");
                            Console.WriteLine("2. Update Name");
                            Console.WriteLine("3. Update Description");
                            Console.WriteLine("4. Exit");

                            string pick = Console.ReadLine() ?? string.Empty;

                            if (int.TryParse(pick, out int pickInt))
                            {
                                if (pickInt == 1) //update course code
                                {
                                    Console.WriteLine("Enter new code:");
                                    courseList[courseNum].Code = Console.ReadLine() ?? string.Empty;
                                }
                                else if (pickInt == 2) //update course name
                                {
                                    Console.WriteLine("Enter new name:");
                                    courseList[courseNum].Name = Console.ReadLine() ?? string.Empty;
                                }
                                else if (pickInt == 3) //update course description
                                {
                                    Console.WriteLine("Enter new description");
                                    courseList[courseNum].Description = Console.ReadLine() ?? string.Empty;
                                }
                                else if (pickInt == 4) //exits back to main menu
                                {
                                    menu = false;
                                }
                            }
                        }
                    }
                    else if (choiceInt == 11) //update student information
                    {
                        Console.WriteLine("Which student do you want to update?");
                        int num = 1;
                        foreach (var student in studentList)
                        {
                            Console.WriteLine(num + ". " + student.Name);
                            num++;
                        }

                        int studentNum;
                        while (!int.TryParse(Console.ReadLine(), out studentNum))
                        {
                            Console.WriteLine("Not a valid selection. Try Again.");
                            int dn = 1;
                            foreach (var student in studentList)
                            {
                                Console.WriteLine(num + ". " + student.Name);
                                dn++;
                            }
                            int.TryParse(Console.ReadLine(), out studentNum);
                        }
                        studentNum--;

                        bool menu = true;
                        while (menu)
                        {
                            Console.WriteLine("Choose an option:");
                            Console.WriteLine("1. Update Name");
                            Console.WriteLine("2. Update Classification");
                            Console.WriteLine("3. Exit");

                            string pick = Console.ReadLine() ?? string.Empty;
                            
                            if (int.TryParse(pick, out int pickInt))
                            {
                                if (pickInt == 1) //updates student name
                                {
                                    Console.WriteLine("Enter a new name:");
                                    studentList[studentNum].Name = Console.ReadLine() ?? string.Empty;

                                }
                                else if (pickInt == 2) //updates student classification
                                {
                                    Console.WriteLine("f = Freshman, " +
                                    "s = Sophomore, " + "j = Junior, " + " e = Senior\n" +
                                    "Enter a new classification:");
                                    studentList[studentNum].Classification = Console.ReadLine() ?? string.Empty;
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
        }
    }
}