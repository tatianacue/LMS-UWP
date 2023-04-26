using Library.LMS.Models;
using System.Collections.Generic;
using System.Net.Http;
using UWP.Library.CueLMS;
/* Tatiana Graciela Cue COP4870-0001*/
namespace UWP.CueLMS.ViewModels
{
    public class AssignmentViewModel
    {
        public AssignmentViewModel(Course c)
        {
            Assignment = new Assignment();
            Course = c;
        }
        public Assignment Assignment { get; set; }
        public Course Course { get; set; }
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
        public async void AddAssignment()
        {
            Course.SelectedAssignment = Assignment;
            var handler = new WebRequestHandler();
            await handler.Post("http://localhost:5100/Assignment", Course, HttpMethod.Post);
        }
    }
}
