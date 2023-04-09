using Library.LMS.Models;
using Library.LMS.Models.Grading;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWP.CueLMS.Dialogs;
using UWP.CueLMS.Dialogs.StudentViewDialogs;

namespace UWP.CueLMS.ViewModels.StudentViewViewModels
{
    public class StudentCourseViewModel
    {
        public StudentCourseViewModel(Course course, Person student) 
        {
            Course = course;
            Student = (Student)student;
            Modules = new ObservableCollection<Module>(Course.Modules);
            Announcements = new ObservableCollection<Announcement>(Course.Announcements);
            Assignments = new ObservableCollection<Assignment>(Course.Assignments);
        }
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
        public void AddSubmission()
        {
            var submission = new Submission(); //creates new submission with that student and assignment
            submission.Assignment = SelectedAssignment;
            submission.Student = Student;
            Course.Submissions.Add(submission); //adds that submission to course
        }
    }
}
