using Library.LMS.Models;
using System.Collections.Generic;

namespace UWP.CueLMS.ViewModels
{
    public class AssignmentViewModel
    {
        public AssignmentViewModel(List<Assignment> assignments)
        {
            Assignment = new Assignment();
            Assignments = assignments;
        }
        public Assignment Assignment { get; set; }
        public List<Assignment> Assignments { get; set; }
        public string Name
        {
            set { Assignment.Name = value; }
        }
        public string Description
        {
            set { Assignment.Description = value; }
        }
        public string DueDate
        {
            set { Assignment.DueDate = value; }
        }
        public int TotalAvailablePoints
        {
            set { Assignment.TotalAvailablePoints = value; }
        }
        public void AddAssignment()
        {
            Assignments.Add(Assignment);
        }
    }
}
