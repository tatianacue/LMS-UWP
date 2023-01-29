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

        public void AddStudent(Person newStudent) //adds student to course
        {
            Roster.Add(newStudent);
            foreach (var student in Roster) //temporary
            {
                Console.WriteLine(student.Name);
            }
        }
    }
}
