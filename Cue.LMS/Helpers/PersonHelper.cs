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
            var newStudent = new Student();
            Console.WriteLine("Enter an ID:");
            newStudent.ID = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Enter a name:");
            newStudent.Name = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("f = Freshman, " +
                "s = Sophomore, " + "j = Junior, " + " e = Senior\n" +
                "Enter a classification:");
            newStudent.Classification = Console.ReadLine() ?? string.Empty;

            studentService.Add(newStudent);
            studentService.studentList.ForEach(Console.WriteLine); //displays all to show student is added to list
        }
        public Student StudentPicker() { //lets user pick student and returns a student
            Console.WriteLine("Which student do you want to pick? (Enter ID)"); //pick student
            studentService.studentList.ForEach(Console.WriteLine);
            var id = Console.ReadLine() ?? string.Empty;
            var selected = studentService.studentList.FirstOrDefault(s => s.ID.Equals(id, StringComparison.InvariantCultureIgnoreCase));
            return selected;
        }
        public void SearchForStudent(CourseHelper helper, string? srch = null) //searches for student and allows selection
        {
            if (string.IsNullOrEmpty(srch))
            {
                studentService.studentList.ForEach(Console.WriteLine);
            }
            else
            {
                studentService.Search(srch).ForEach(Console.WriteLine);
            }
            Console.WriteLine("Which student would you like to select? (Enter ID)");
            var code = Console.ReadLine() ?? string.Empty;
            var selected = studentService.studentList.FirstOrDefault(s => s.ID.Equals(code, StringComparison.InvariantCultureIgnoreCase));
            if (selected != null)
            {
                DisplayAll(selected);
                helper.ListStudentCourses(selected);
            }
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
                        Console.WriteLine("Enter new ID:"); //update ID
                        selected.ID = Console.ReadLine() ?? string.Empty;
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
            Console.WriteLine("\tName: " + student.Name);
            Console.WriteLine("\tClassification: " + student.Classification);

        }
    }
}
