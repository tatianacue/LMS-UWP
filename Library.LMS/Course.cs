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

        public void RemoveStudent(Person newStudent) //removes student from course
        {
            Roster.Remove(newStudent); // removes from roster
            Console.WriteLine(newStudent.Name + " removed from " + Code);
        }

        public void ListStudents()
        {
            foreach (var student in Roster)
            {
                Console.WriteLine("Students in " + Code + ":");
                Console.WriteLine(student.Name);
            }
        }

        public void AddAssignment(Assignment newAssignment) //add assignment to course
        {
            Assignments.Add(newAssignment);
            Console.WriteLine(newAssignment.Name + " added to " + Code);
        }
    }
}
