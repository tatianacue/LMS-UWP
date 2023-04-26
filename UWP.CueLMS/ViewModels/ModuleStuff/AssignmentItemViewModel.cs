using Library.LMS.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using UWP.Library.CueLMS;
/* Tatiana Graciela Cue COP4870-0001*/
namespace UWP.CueLMS.ViewModels.ModuleStuff
{
    public class AssignmentItemViewModel
    {
        public AssignmentItemViewModel(Course course, ObservableCollection<Assignment> a)
        {
            AssignmentItem = new AssignmentItem();
            Assignments = a;
            Course = course;
        }
        public AssignmentItem AssignmentItem { get; set; }
        public Course Course { get; set; } 
        public ObservableCollection<Assignment> Assignments { get; set; }
        public Assignment SelectedAssignment { get; set; }
        public string Name
        {
            set  {   AssignmentItem.Name = value;  }
        }
        public string Description
        {
            set
            {  AssignmentItem.Description = value;  }
        }
        public async void AddItem()
        {
            AssignmentItem.Assignment = SelectedAssignment;
            Course.SelectedItem = AssignmentItem;
            var handler = new WebRequestHandler();
            await handler.Post("http://localhost:5100/Module/PostContent", Course, HttpMethod.Post);
        }
    }
}
