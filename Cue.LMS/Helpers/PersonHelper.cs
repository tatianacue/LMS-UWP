using Library.LMS.Models;
using Library.LMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
/* Tatiana Graciela Cue COP4870-0001*/
namespace App.LMS.Helpers
{
    public class PersonHelper
    {
        private PersonService studentService;
        public PersonHelper() { 
            studentService = new PersonService();
        }

        public void AddStudent() //adds student to studentList
        {
            var newStudent = new Person();

            Console.WriteLine("Enter a name:");
            newStudent.Name = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("f = Freshman, " +
                "s = Sophomore, " + "j = Junior, " + " e = Senior\n" +
                "Enter a classification:");
            newStudent.Classification = Console.ReadLine() ?? string.Empty;

            studentService.Add(newStudent);
            studentService.studentList.ForEach(Console.WriteLine); //displays all to show student is added to list
        }
        public void ListAllStudents()
        {
            Console.WriteLine("ALL STUDENTS:");
            studentService.studentList.ForEach(Console.WriteLine);
        }
        public Person StudentPicker() { //lets user pick student and returns a student
            Console.WriteLine("Which student do you want to pick?"); //pick student
            int num = 1;
            foreach (var student in studentService.studentList)
            {
                Console.WriteLine(num + ". " + student);
                num++;
            }

            int studentNum;
            while (!int.TryParse(Console.ReadLine(), out studentNum))
            {
                Console.WriteLine("Not a valid selection. Try Again.");
                int dn = 1;
                foreach (var student in studentService.studentList)
                {
                    Console.WriteLine(num + ". " + student);
                    dn++;
                }
                int.TryParse(Console.ReadLine(), out studentNum);
            }
            studentNum--;

            return studentService.studentList[studentNum];
        }
        public void SearchForStudent(CourseHelper helper) //searches for student and allows selection
        {
            Console.WriteLine("Enter a student name you want to find:");
            string search = Console.ReadLine() ?? string.Empty;
            var searchStudent = studentService.studentList.Where(t => t.Name.Contains(search, StringComparison.InvariantCultureIgnoreCase));
            var results = searchStudent.ToList();

            Console.WriteLine("Which student do you want to see more details for?"); //pick student
            int num = 1;
            foreach (var student in results)
            {
                Console.WriteLine(num + ". " + student);
                num++;
            }

            int studentNum;
            while (!int.TryParse(Console.ReadLine(), out studentNum))
            {
                Console.WriteLine("Not a valid selection. Try Again.");
                int dn = 1;
                foreach (var student in results)
                {
                    Console.WriteLine(num + ". " + student);
                    dn++;
                }
                int.TryParse(Console.ReadLine(), out studentNum);
            }
            studentNum--;

            DisplayAll(results[studentNum]); //outputs all student details
            helper.ListStudentCourses(results[studentNum]); //uses course helper to list student schedule

        }
        public void UpdateStudent() //student update menu
        {
            Console.WriteLine("Which student do you want to update?");
            int num = 1;
            foreach (var student in studentService.studentList)
            {
                Console.WriteLine(num + ". " + student.Name);
                num++;
            }

            int studentNum;
            while (!int.TryParse(Console.ReadLine(), out studentNum))
            {
                Console.WriteLine("Not a valid selection. Try Again.");
                int dn = 1;
                foreach (var student in studentService.studentList)
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
                        studentService.studentList[studentNum].Name = Console.ReadLine() ?? string.Empty;
                        Console.WriteLine("Updated info: " + studentService.studentList[studentNum]); //displays updated info

                    }
                    else if (pickInt == 2) //updates student classification
                    {
                        Console.WriteLine("f = Freshman, " +
                        "s = Sophomore, " + "j = Junior, " + " e = Senior\n" +
                        "Enter a new classification:");
                        studentService.studentList[studentNum].Classification = Console.ReadLine() ?? string.Empty;
                        Console.WriteLine("Updated info: " + studentService.studentList[studentNum]);
                    }
                    else if (pickInt == 3) //exits back to main menu
                    {
                        menu = false;
                    }
                }
            }
        }
        public void DisplayAll(Person student)
        {
            Console.WriteLine("\t\tSTUDENT DETAILS");
            Console.WriteLine("\tName: " + student.Name);
            Console.WriteLine("\tClassification: " + student.Classification);

        }
    }
}
