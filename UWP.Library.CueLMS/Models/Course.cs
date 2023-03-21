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
            Announcements = new List<Announcement>();
            Name = string.Empty;
            Description= string.Empty;
        }

        //properties
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CreditHours { get; set; }
        public List<Person> Roster { get; set; }
        public List<Assignment> Assignments { get; set; }
        public List<AssignmentGroup> AssignmentGroups { get; set; } //groups of assignments
        public List<Module> Modules { get; set; }
        public List<Submission> Submissions { get; set; }
        public List<Announcement> Announcements { get; set; }

        //list manipulation
        public void AddStudent(Student newStudent) //adds student to course
        {
            Roster.Add(newStudent); //adds to roster
        }

        public void RemoveStudent(Student student) //removes student from course EDIT THIS
        {
            Roster.Remove(student);
        }

        public void AddAssignment(Assignment newAssignment) //add assignment to course
        {
            Assignments.Add(newAssignment);
        }
        public void AddAssignmentGroup(AssignmentGroup group)
        {
            AssignmentGroups.Add(group);
        }

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
        public void AddModule(Module newModule) //creates module
        {
            Modules.Add(newModule);
        }
        public void AddAnnouncement(Announcement announcement) //add announcement
        {
            Announcements.Add(announcement);
        }
        public void RemoveAnnouncement(Announcement announcement) //remove announcement
        {
            Announcements.Remove(announcement);
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
