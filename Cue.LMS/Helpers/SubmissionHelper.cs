using Library.LMS.Models;
using Library.LMS.Models.Grading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
/* Tatiana Graciela Cue COP4870-0001*/
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
            submission.Course = course;
            course.Submissions.Add(submission);
            Console.WriteLine("Assignment Submitted!");
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
            double totalPts = submission.Assignment.TotalAvailablePoints;
            double decimalGrade = points/totalPts; //calculates grade
            double percentGrade = decimalGrade * 100;

            submission.Grade = points; //points
            submission.Student.AddAssignmentGrade(submission.Assignment, percentGrade); //adds to student individual assignment grades
        }
        public double CalculateCourseGrade(Student student, Course course)
        {
            var tempList = course.Submissions.Where(s => s.Student == student).ToList(); //list of submissions by specific student
            Dictionary<AssignmentGroup, double> GroupGrades = new Dictionary<AssignmentGroup, double>();
            double courseGrade = 0;
            foreach (var group in course.AssignmentGroups) //each group in course
            {
                double totalGrades = 0;
                double totalPoints = 0;
                foreach (var submission in tempList) //for each submission
                {
                    foreach (var assignment in group.Group) //each assignment in group
                    {
                        if (submission.Assignment == assignment)
                        {
                            totalGrades += submission.Grade;
                            totalPoints += assignment.TotalAvailablePoints;
                        }
                    }
                } 
               var totalGroupGrade = totalGrades / totalPoints; //total grade for entire group
               GroupGrades.Add(group, totalGroupGrade); //adds group and grade to dictionary
            }
            foreach (var pair in GroupGrades) //for each total grade in group, multiply by the weight
            {
                double weightedTotal = (pair.Key.Weight) * (pair.Value);
                courseGrade += weightedTotal;
            }
            student.Grades[course] = courseGrade; //update course grade dictionary in student
            return courseGrade;
        }

    }
}
