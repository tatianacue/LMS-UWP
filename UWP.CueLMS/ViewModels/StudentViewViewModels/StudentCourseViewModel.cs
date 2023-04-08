using Library.LMS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWP.CueLMS.ViewModels.StudentViewViewModels
{
    public class StudentCourseViewModel
    {
        public StudentCourseViewModel(Course course) 
        {
            Course = course;
            Modules = new ObservableCollection<Module>(Course.Modules);
            Announcements = new ObservableCollection<Announcement>(Course.Announcements);
            Assignments = new ObservableCollection<Assignment>(Course.Assignments);
        }
        public Course Course { get; set;}
        public string CourseCode {
            get
            {
                if (Course != null)
                {
                    return Course.Code;
                }
                else { return string.Empty; }
            }
        }
        public ObservableCollection<Module> Modules { get; set; }
        public ObservableCollection<Announcement> Announcements { get; set; }
        public ObservableCollection<Assignment> Assignments { get; set; }
        public Module SelectedModule { get; set; }
        public Announcement SelectedAnnouncement { get; set; }
        public Assignment SelectedAssignment { get; set; }
    }
}
