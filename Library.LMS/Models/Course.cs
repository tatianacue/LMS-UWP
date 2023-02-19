using System.ComponentModel.DataAnnotations;
/* Tatiana Graciela Cue COP4870-0001*/
namespace Library.LMS.Models
{
    public class Course
    {
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
        public List<Person> Roster { get; set; }
        public List<Assignment> Assignments { get; set; }
        public List<Module> Modules { get; set; }

        //list manipulation
        public void AddStudent(Student newStudent) //adds student to course
        {
            Roster.Add(newStudent); //adds to roster
        }

        public void RemoveStudent() //removes student from course
        {
            Console.WriteLine("Which student do you want to remove from " + Code.ToUpper() + "? (Enter ID)");
            Roster.ForEach(Console.WriteLine);
            var id = Console.ReadLine() ?? string.Empty;
            var selected = Roster.FirstOrDefault(s => s.ID.Equals(id, StringComparison.InvariantCultureIgnoreCase));

            Console.WriteLine("Removing " + selected.Name + " from " + Code.ToUpper());
            Roster.Remove(selected);
        }

        public void AddAssignment(Assignment newAssignment) //add assignment to course
        {
            Assignments.Add(newAssignment);
            Console.WriteLine(newAssignment.Name + " added to " + Code.ToUpper());
        }

        public bool FindStudent(Student student) //finds if student is in roster
        {
            int check = -1;
            foreach (var person in Roster)
            {
                if (person.Equals(student))
                {
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
        public void CreateModule(Module newModule) //creates module
        {
            Modules.Add(newModule);
            Console.WriteLine(newModule.Name + " added to " + Code.ToUpper());
        }

        //output
        public override string ToString() //override output course
        {
            return $"{Code.ToUpper()} - {Name}";
        }

        public string DisplayAll() //displays detailed view
        {
            return $"\t\tCOURSE DETAILS\n" +
                $"\tCode: {Code.ToUpper()}\n" +
                $"\tName: {Name}\n" +
                $"\tDescription: {Description}\n" +
                $"\tRoster:\n\t{string.Join("\n\t", Roster.Select(s => s.ToString()).ToArray())}\n" +
                $"\tAssignments:\n\t{string.Join("\n\t", Assignments.Select(s => s.ToString()).ToArray())}";
        }
    }
}
