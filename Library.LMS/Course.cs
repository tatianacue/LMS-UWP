using System.ComponentModel.DataAnnotations;

namespace Library.LMS
{
    public class Course
    {
        //default constructor
        public Course() { }

        //properties
        private string code;
        private string name;
        private string description;
        public string Code 
        {
            get { return code; }
            set { code = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        //lists
        private List<Person> Roster = new List<Person>();
        private List<Assignment> Assignments = new List<Assignment>();
        private List<Module> Modules = new List<Module>();

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
                Console.WriteLine(student.Name);
            }
        }

        //assignment manipulation
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
            Console.WriteLine("\tCourse Details");
            Console.WriteLine("Code: " + Code);
            Console.WriteLine("Name: " + Name);
            Console.WriteLine("Description: " + Description);

            Console.WriteLine("Students in course:");
            ListStudents();
        }
    }   
}
