using Library.LMS.Models;
using Library.LMS.Models.Grading;
using System.Collections.Generic;
using System.Net.Http;
using UWP.Library.CueLMS;
/* Tatiana Graciela Cue COP4870-0001*/
namespace UWP.CueLMS.ViewModels
{
    public class AssignmentGroupViewModel
    {
        public AssignmentGroupViewModel(Course course)
        {
            AssignmentGroup = new AssignmentGroup();
            Course = course;
        }
        public Course Course { get; set; }
        public AssignmentGroup AssignmentGroup { get; set; }
        public string Name 
        { 
            set { AssignmentGroup.Name = value; }
        }
        public int Weight 
        { 
            set { AssignmentGroup.Weight = value; }
        }
        public async void Add()
        {
            Course.SelectedAssignmentGroup = AssignmentGroup;
            var handler = new WebRequestHandler();
            await handler.Post($"http://localhost:5100/AssignmentGroup", Course, HttpMethod.Post);
        }
    }
}
