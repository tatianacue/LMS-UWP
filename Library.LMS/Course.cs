using System.ComponentModel.DataAnnotations;

namespace Library.LMS
{
    public class Course
    {
        //lists
        public List<Person> Roster { get; set; }
        public List<Assignment> Assignments { get; set; }
        public List<Module> Modules { get; set; }

        //default constructor
        public Course() 
        {
            Roster = new List<Person>();
            Assignments = new List<Assignment>();
            Modules = new List<Module>();
        }

        //properties
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //list manipulation
        public void AddStudent(Person newStudent) //adds student to course
        {
            Roster.Add(newStudent); //adds to roster
            Console.WriteLine(newStudent.Name + " added to " + Code);
        }

        public void RemoveStudent() //removes student from course
        {
            Console.WriteLine("Which student do you want to remove from " + Code + "?");
            int displayNum = 1;
            foreach (var student in Roster)
            {
                Console.WriteLine(displayNum + ". " + student.Name);
                displayNum++;
            }

            int studentNum;
            while (!int.TryParse(Console.ReadLine(), out studentNum))
            {
                int dn = 1;
                foreach (var student in Roster)
                {
                    Console.WriteLine(dn + ". " + student.Name);
                    dn++;
                }
                int.TryParse(Console.ReadLine(), out studentNum);
            }
            studentNum--;

            Roster.RemoveAt(studentNum);
        }

        public void ListStudents()
        {
            foreach (var student in Roster)
            {
                Console.WriteLine("\t" + student.Name);
            }
        }

        public void AddAssignment(Assignment newAssignment) //add assignment to course
        {
            Assignments.Add(newAssignment);
            Console.WriteLine(newAssignment.Name + " added to " + Code);
        }

        public bool FindStudent(Person student) //finds if student is in roster
        {
            int check = -1;
            foreach (var person in Roster)
            {
                if (person.Equals(student)) {
                    check = 1;
                    break;
                }
            }
            if (check == -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //output
        public override string ToString() //override output course
        {
            return (this.Code + "\t\t" + this.Name);
        }

        public void DisplayAll()
        {
            Console.WriteLine("\t\tCourse Details");
            Console.WriteLine("\tCode: " + Code);
            Console.WriteLine("\tName: " + Name);
            Console.WriteLine("\tDescription: " + Description);

            Console.WriteLine("\tStudents in course:");
            ListStudents();
        }
    }   
}
