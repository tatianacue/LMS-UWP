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
            Name = string.Empty;
            Description= string.Empty;
            SelectedAnnouncement = new Announcement();
            SelectedAssignment = new Assignment();
            SelectedAssignmentGroup = new AssignmentGroup();
            SelectedModule = new Module();
            SelectedStudent = new Student();
            SelectedFileItem = new FileItem();
            SelectedPageItem = new PageItem();
            SelectedAssignmentItem = new AssignmentItem();
        }

        //properties
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CreditHours { get; set; }

        //lists
        public List<Student> Roster = new List<Student>();
        public List<Assignment> Assignments = new List<Assignment>(); 
        public List<AssignmentGroup> AssignmentGroups = new List<AssignmentGroup>(); 
        public List<Module> Modules = new List<Module>();
        public List<Submission> Submissions = new List<Submission>();
        public List<Announcement> Announcements = new List<Announcement>();

        //selections temporary
        public Announcement SelectedAnnouncement { get; set; }
        public Assignment SelectedAssignment { get; set; }
        public AssignmentGroup SelectedAssignmentGroup { get; set; }
        public Module SelectedModule { get; set; }
        public Student SelectedStudent { get; set; }
        public ContentItem SelectedItem { get; set; } 
        public Submission SelectedSubmission { get; set; }
        public FileItem SelectedFileItem { get; set; }
        public PageItem SelectedPageItem { get; set; }
        public AssignmentItem SelectedAssignmentItem { get; set; }
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
