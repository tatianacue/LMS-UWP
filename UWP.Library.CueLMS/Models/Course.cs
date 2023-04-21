using Library.LMS.Models.Grading;
using Library.LMS.Services;
using System.Collections.Generic;
using System.Linq;
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
            AssignmentGroups = new List<AssignmentGroup>();
            Submissions = new List<Submission>();
            Announcements = new List<int>();
            Name = string.Empty;
            Description= string.Empty;
        }

        //properties
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CreditHours { get; set; }
        public List<Person> Roster { get; set; }
        public List<Assignment> Assignments { get; set; }
        public List<AssignmentGroup> AssignmentGroups { get; set; } //groups of assignments
        public List<Module> Modules { get; set; }
        public List<Submission> Submissions { get; set; }
        public List<int> Announcements { get; set; } //list of announcement Id's to grab from database
        public string RoomLocation { get; set; }

        public bool FindStudent(Person student) //finds if student is in roster
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

        //output
        public override string ToString() //override output course
        {
            return $"{Code.ToUpper()} - {Name}";
        }
        public string Display => $"[{Code}] - {Name}";
    }
}
