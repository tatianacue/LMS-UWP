using Library.LMS.Models.Grading;
using Library.LMS.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using UWP.Library.CueLMS;
/* Tatiana Graciela Cue COP4870-0001*/
namespace Library.LMS.Models
{
    public class Course
    {
        //default constructor
        public Course()
        {
            Roster = new List<Person>();
            Modules = new List<Module>();
            AssignmentGroups = new List<AssignmentGroup>();
            Submissions = new List<Submission>();
            Name = string.Empty;
            Description= string.Empty;
            SelectedAnnouncement = new Announcement();
        }

        //properties
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CreditHours { get; set; }
        public List<Person> Roster { get; set; }
        public List<Assignment> Assignments = new List<Assignment>();
        public List<AssignmentGroup> AssignmentGroups { get; set; } //groups of assignments
        public List<Module> Modules { get; set; }
        public List<Submission> Submissions { get; set; }

        public List<Announcement> Announcements = new List<Announcement>(); //list for database
        public Announcement SelectedAnnouncement { get; set; }
        public Assignment SelectedAssignment { get; set; }
        public AssignmentGroup SelectedAssignmentGroup { get; set; }
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
