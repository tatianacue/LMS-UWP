using Library.LMS.Models;
using Library.LMS.Models.Grading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.LMS.Helpers
{
    public class SubmissionHelper
    {
        public SubmissionHelper()  {  }
        public void AddSubmission(Course course, Student student) //adds submission from student to list
        {
            Console.WriteLine("Which assignment do you want to submit?");
            course.Assignments.ForEach(Console.WriteLine);
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid. Try Again.");
                int.TryParse(Console.ReadLine(), out id);
            }
            var assignment = course.Assignments.FirstOrDefault(a => a.Id == id);
            var submission = new Submission();
            submission.Student = student;
            submission.Assignment = assignment;
            course.Submissions.Add(submission);
        }
        public void AddGrade(Course course)
        {
            Console.WriteLine("Which submission do you want to add a grade to?");
            course.Submissions.ForEach(Console.WriteLine);
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid. Try Again.");
                int.TryParse(Console.ReadLine(), out id);
            }
            var submission = course.Submissions.FirstOrDefault(s => s.Id == id);
            Console.WriteLine("Enter grade (out of " + submission.Assignment.TotalAvailablePoints + " points)");
            int points;
            while (!int.TryParse(Console.ReadLine(), out points))
            {
                Console.WriteLine("Invalid. Try Again.");
                int.TryParse(Console.ReadLine(), out points);
            }
            submission.Grade = points;
        }
    }
}
