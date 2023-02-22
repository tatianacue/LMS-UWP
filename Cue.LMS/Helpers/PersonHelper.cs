using Library.LMS.Models;
using Library.LMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
/* Tatiana Graciela Cue COP4870-0001*/
namespace App.LMS.Helpers
{
    public class PersonHelper
    {
        private PersonService personService;
        public PersonHelper() { 
            personService = new PersonService();
        }
        public void AddPerson() //adds student to studentList
        {
            Console.WriteLine("Is this person a (S)tudent, (T)eaching Assistant, or (I)nstructor?");
            string choiceChar = Console.ReadLine() ?? string.Empty;
            if (choiceChar.Equals("s", StringComparison.InvariantCultureIgnoreCase))
            {
                var newStudent = new Student();
                Console.WriteLine("Enter an ID:");
                string tempID = Console.ReadLine() ?? string.Empty;
                tempID = tempID.ToUpper();
                newStudent.ID = tempID;
                bool check = personService.CheckID(newStudent.ID);
                while (!check) // checks if ID already exists
                {
                    Console.WriteLine("ID Already Exists. Enter another ID:");
                    tempID = Console.ReadLine() ?? string.Empty;
                    tempID = tempID.ToUpper();
                    newStudent.ID = tempID;
                    check = personService.CheckID(newStudent.ID);
                }
                Console.WriteLine("Enter a name:");
                newStudent.Name = Console.ReadLine() ?? string.Empty;

                Console.WriteLine("f = Freshman, " +
                    "s = Sophomore, " + "j = Junior, " + " e = Senior\n" +
                    "Enter a classification:");
                newStudent.Classification = Console.ReadLine() ?? string.Empty;
                personService.Add(newStudent);
                personService.personList.ForEach(Console.WriteLine); //displays all to show student is added to list
            }
            else if (choiceChar.Equals("t", StringComparison.InvariantCultureIgnoreCase))
            {
                var newTA = new TeachingAssistant();
                Console.WriteLine("Enter an ID:");
                string tempID = Console.ReadLine() ?? string.Empty;
                tempID = tempID.ToUpper();
                newTA.ID = tempID;
                bool check = personService.CheckID(newTA.ID);
                while (!check) // checks if ID already exists
                {
                    Console.WriteLine("ID Already Exists. Enter another ID:");
                    tempID = Console.ReadLine() ?? string.Empty;
                    tempID = tempID.ToUpper();
                    newTA.ID = tempID;
                    check = personService.CheckID(newTA.ID);
                }
                Console.WriteLine("Enter a name:");
                newTA.Name = Console.ReadLine() ?? string.Empty;
                personService.Add(newTA);
                personService.personList.ForEach(Console.WriteLine);
            }
            else if (choiceChar.Equals("i", StringComparison.InvariantCultureIgnoreCase))
            {
                var newI = new Instructor();
                Console.WriteLine("Enter an ID:");
                string tempID = Console.ReadLine() ?? string.Empty;
                tempID = tempID.ToUpper();
                newI.ID = tempID;
                bool check = personService.CheckID(newI.ID);
                while (!check) // checks if ID already exists
                {
                    Console.WriteLine("ID Already Exists. Enter another ID:");
                    tempID = Console.ReadLine() ?? string.Empty;
                    tempID = tempID.ToUpper();
                    newI.ID = tempID;
                    check = personService.CheckID(newI.ID);
                }
                Console.WriteLine("Enter a name:");
                newI.Name = Console.ReadLine() ?? string.Empty;
                personService.Add(newI);
                personService.personList.ForEach(Console.WriteLine);
            }
        }
        public Student StudentPicker() { //lets user pick student and returns a student
            Console.WriteLine("Which student do you want to pick? (Enter ID)"); //pick student
            var students = personService.personList.Where(s => s is Student).ToList();
            students.ForEach(Console.WriteLine);
            var id = Console.ReadLine() ?? string.Empty;
            var selected = personService.personList.FirstOrDefault(s => s.ID.Equals(id, StringComparison.InvariantCultureIgnoreCase));
            return (Student)selected;
        }
        public void SearchForStudent(CourseHelper helper, string? srch = null) //searches for student and allows selection
        {
            if (string.IsNullOrEmpty(srch))
            {
                personService.personList.Where(s => s is Student).ToList().ForEach(Console.WriteLine); //lists all students
            }
            else
            {
                personService.Search(srch).ForEach(Console.WriteLine);
            }
            Console.WriteLine("Which student would you like to select? (Enter ID)");
            var code = Console.ReadLine() ?? string.Empty;
            var selected = personService.personList.FirstOrDefault(s => s.ID.Equals(code, StringComparison.InvariantCultureIgnoreCase) && s is Student);
            if (selected != null)
            {
                DisplayAll((Student)selected);
                helper.ListStudentCourses((Student)selected);
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
                        string oldID = selected.ID; //saves old code to remove from key list

                        Console.WriteLine("Enter new code:");
                        string tempID = Console.ReadLine() ?? string.Empty;
                        tempID = tempID.ToUpper();
                        selected.ID = tempID;
                        bool check = personService.CheckID(selected.ID);
                        while (!check) // checks if code already exists
                        {
                            Console.WriteLine("Course Code Already Exists. Enter another one:");
                            tempID = Console.ReadLine() ?? string.Empty;
                            tempID = tempID.ToUpper();
                            selected.ID = tempID;
                            check = personService.CheckID(selected.ID);
                        }
                        personService.IDDictionary.Remove(oldID); //removes old code and frees it up for possible use later
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
            Console.WriteLine("\tID: " + student.ID);
            Console.WriteLine("\tName: " + student.Name);
            Console.WriteLine("\tClassification: " + student.Classification);

        }
        public void ListAllPeople() //lists people
        {
            personService.personList.ForEach(Console.WriteLine);
        }
    }
}
