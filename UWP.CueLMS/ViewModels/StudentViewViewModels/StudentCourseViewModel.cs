using Library.LMS.Models;
using Library.LMS.Models.Grading;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using UWP.CueLMS.Dialogs.StudentViewDialogs;
using UWP.Library.CueLMS;
/* Tatiana Graciela Cue COP4870-0001*/
namespace UWP.CueLMS.ViewModels.StudentViewViewModels
{
    public class StudentCourseViewModel
    {
        public StudentCourseViewModel(Course course, Person student) 
        {
            Course = course;
            Student = (Student)student;
            Modules = new ObservableCollection<Module>(moduleList);
            Announcements = new ObservableCollection<Announcement>(announcementList);
            Assignments = new ObservableCollection<Assignment>(assignmentList);
        }
        //database lists
        public List<Announcement> announcementList
        {
            get
            {
                var id = Course.Id;
                var payload = new WebRequestHandler().Get($"http://localhost:5100/Announcement/GetList/{id}").Result;
                return JsonConvert.DeserializeObject<List<Announcement>>(payload).OrderBy(x => x.Id).ToList();
            }
        }
        public List<Module> moduleList
        {
            get
            {
                var id = Course.Id;
                var payload = new WebRequestHandler().Get($"http://localhost:5100/Module/GetList/{id}").Result;
                return JsonConvert.DeserializeObject<List<Module>>(payload).OrderBy(x => x.Id).ToList();
            }
        }
        public List<Assignment> assignmentList
        {
            get
            {
                var id = Course.Id;
                var payload = new WebRequestHandler().Get($"http://localhost:5100/Assignment/GetList/{id}").Result;
                return JsonConvert.DeserializeObject<List<Assignment>>(payload).OrderBy(x => x.Id).ToList();
            }
        }
        //other
        public Course Course { get; set;}
        private Student Student { get; set;}
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
        //announcement stuff
        public string AnnouncementTitle
        {
            get
            {
                if (SelectedAnnouncement != null)
                {
                    return SelectedAnnouncement.Title;
                }
                else { return string.Empty; }
            }
        }
        public string AnnouncementBody
        {
            get
            {
                if (SelectedAnnouncement != null)
                {
                    return SelectedAnnouncement.Text;
                }
                else { return string.Empty; }
            }
        }
        //assignment stuff
        public string AssignmentName
        {
            get
            {
                if (SelectedAssignment != null)
                {
                    return SelectedAssignment.Name;
                }
                else { return string.Empty; }
            }
        }
        public string AssignmentDescription
        {
            get
            {
                if (SelectedAssignment != null)
                {
                    return SelectedAssignment.Description;
                }
                else { return string.Empty; }
            }
        }
        public string AssignmentDueDate
        {
            get
            {
                if (SelectedAssignment != null)
                {
                    return SelectedAssignment.DueDate;
                }
                else { return string.Empty; }
            }
        }
        public int AssignmentPoints
        {
            get
            {
                if (SelectedAssignment != null)
                {
                    return SelectedAssignment.TotalAvailablePoints;
                }
                else { return 0; }
            }
        }
        //submission stuff
        public async void SubmissionDialog()
        {
            var dialog = new SubmissionDialog(this);
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
        }
        public async void AddSubmission()
        {
            var submission = new Submission(); //creates new submission with that student and assignment
            submission.Assignment = SelectedAssignment;
            submission.Student = Student;

            Course.SelectedSubmission = submission;
            var handler = new WebRequestHandler();
            await handler.Post("http://localhost:5100/Submission", Course, HttpMethod.Post);
        }
    }
}
