using Library.LMS.Models;
using Library.LMS.Models.Grading;
using Library.LMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml.Serialization;

/* Tatiana Graciela Cue COP4870-0001*/
namespace App.LMS.Helpers
{
    public class CourseHelper
    {
        private CourseService courseService;
        private SubmissionHelper submissionHelper;
        public CourseHelper()
        {
            courseService = new CourseService();
            submissionHelper = new SubmissionHelper();
        }
        private Course CoursePicker() //course selection menu
        {
            courseService.courseList.ForEach(Console.WriteLine);
            var code = Console.ReadLine() ?? string.Empty;
            var selected = courseService.courseList.FirstOrDefault(c => c.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase));
            return selected;
        }
        public Course StudentCoursePicker(Student student)
        {
            var studentCourses = new List<Course>();
            foreach (var tempCourse in courseService.courseList)
            {
                bool real = tempCourse.FindStudent(student); //if student found in course then it will add to temp list
                if (real)
                {
                    studentCourses.Add(tempCourse);
                }
            }
            studentCourses.ForEach(Console.WriteLine);
            var code = Console.ReadLine() ?? string.Empty;
            var selected = courseService.courseList.FirstOrDefault(c => c.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase));
            return selected;
        }
        public void AddCourse() //adds course to courseList
        {
            var newCourse = new Course();

            Console.WriteLine("Enter a Course Code:");
            string tempCode = Console.ReadLine() ?? string.Empty;
            tempCode = tempCode.ToUpper();
            newCourse.Code = tempCode;
            bool check = courseService.CheckCode(newCourse.Code);
            while (!check) // checks if code already exists
            {
                Console.WriteLine("Course Code Already Exists. Enter another one:");
                tempCode = Console.ReadLine() ?? string.Empty;
                tempCode = tempCode.ToUpper();
                newCourse.Code = tempCode;
                check = courseService.CheckCode(newCourse.Code);
            }

            Console.WriteLine("Enter a Name:");
            newCourse.Name = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Enter a Description:");
            newCourse.Description = Console.ReadLine() ?? string.Empty;

            courseService.Add(newCourse);
            courseService.courseList.ForEach(Console.WriteLine); //lists all courses after adding a course
        }
        public void AddStudentToCourse(Student student) //takes in student and adds to course
        {
            Console.WriteLine("Which course do you want to add to?"); //pick course
            var selected = CoursePicker();

            selected.AddStudent(student);
            Console.WriteLine(student.Name + " added to " + selected.Code.ToUpper()); //says added to list
        }
        public void RemoveStudent() //removes student from course
        {
            Console.WriteLine("Which course do you want to remove student from?");
            var selected = CoursePicker();
            selected.RemoveStudent();
        }
        public void SearchForCourse(string ? srch = null) //searches for course and allows you to select it
        {
            if (string.IsNullOrEmpty(srch))
            {
                courseService.courseList.ForEach(Console.WriteLine);
            }
            else
            {
                courseService.Search(srch).ForEach(Console.WriteLine);
            }
            Console.WriteLine("Which course would you like to select? (Enter Code)");
            var code = Console.ReadLine() ?? string.Empty;
            var selected = courseService.courseList.FirstOrDefault(c => c.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase));
            if (selected != null)
            {
                Console.WriteLine(selected.DisplayAll());
            }
        }
        public void UpdateCourse() //update course menu and features
        {
            Console.WriteLine("Which course do you want to update?");
            var selected = CoursePicker();

            bool menu = true;
            while (menu)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Update Code");
                Console.WriteLine("2. Update Name");
                Console.WriteLine("3. Update Description");
                Console.WriteLine("4. Exit");

                string pick = Console.ReadLine() ?? string.Empty;

                if (int.TryParse(pick, out int pickInt))
                {
                    if (pickInt == 1) //update course code
                    {
                        string oldCode = selected.Code; //saves old code to remove from key list

                        Console.WriteLine("Enter new code:");
                        string tempCode = Console.ReadLine() ?? string.Empty;
                        tempCode = tempCode.ToUpper();
                        selected.Code = tempCode;
                        bool check = courseService.CheckCode(selected.Code);
                        while (!check) // checks if code already exists
                        {
                            Console.WriteLine("Course Code Already Exists. Enter another one:");
                            tempCode = Console.ReadLine() ?? string.Empty;
                            tempCode = tempCode.ToUpper();
                            selected.Code = tempCode;
                            check = courseService.CheckCode(selected.Code);
                        }
                        courseService.Codes.Remove(oldCode); //removes old code and frees it up for possible use later
                        Console.WriteLine("Updated info: " + selected);
                    }
                    else if (pickInt == 2) //update course name
                    {
                        Console.WriteLine("Enter new name:");
                        selected.Name = Console.ReadLine() ?? string.Empty;
                        Console.WriteLine("Updated info: " + selected);
                    }
                    else if (pickInt == 3) //update course description
                    {
                        Console.WriteLine("Enter new description");
                        selected.Description = Console.ReadLine() ?? string.Empty;
                        Console.WriteLine("Updated info: " + selected + " - " + selected.Description);
                    }
                    else if (pickInt == 4) //exits back to main menu
                    {
                        menu = false;
                    }
                }
            }
        }
        public void ListStudentCourses(Student student) //find student in course
        {
            var studentCourses = new List<Course>();
            foreach (var tempCourse in courseService.courseList)
            {
                bool real = tempCourse.FindStudent(student); //if student found in course then it will add to temp list
                if (real)
                {
                    studentCourses.Add(tempCourse);
                }
            }
            //lists courses in student's lists
            Console.WriteLine("\tCourses that " + student.Name + " is taking:");
            foreach (var course in studentCourses)
            {
                Console.WriteLine("\t" + course);
            }
        }
        public void AddAssignment() //add assignment to a course
        {
            Console.WriteLine("Which course do you want to add to?"); //pick course
            var selected = CoursePicker();

            var tempAssignment = new Assignment();
            Console.WriteLine("Enter name for assignment:"); //set name
            tempAssignment.Name = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Enter description for assignment:"); //set description
            tempAssignment.Description = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Enter total available points for assignment:"); //set total available points
            int points;
             while (!int.TryParse(Console.ReadLine(), out points))
            {
                Console.WriteLine("Invalid. Try Again.");
                int.TryParse(Console.ReadLine(), out points);
            }
            tempAssignment.TotalAvailablePoints = points;

            Console.WriteLine("Enter due date:");
            tempAssignment.DueDate = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Which group do you want to add assignment to?");
            selected.AssignmentGroups.ForEach(Console.WriteLine);
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid. Try Again.");
                int.TryParse(Console.ReadLine(), out id);
            }
            var group = selected.AssignmentGroups.FirstOrDefault(g => g.Id == id);
            tempAssignment.Group = group; //assigns group to assignment
            group.AddAssignment(tempAssignment); //assigns assignment to group
            selected.AddAssignment(tempAssignment); //adds assignment to that specific course
        }
        public void AddAssignmentGroup()
        {
            Console.WriteLine("Which course do you want to add to? (Enter code)"); //pick course
            var selected = CoursePicker();
            var tempGroup = new AssignmentGroup();
            Console.WriteLine("Enter name for group:");
            tempGroup.Name = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Enter weight for group:");
            int weight;
            while (!int.TryParse(Console.ReadLine(), out weight))
            {
                Console.WriteLine("Invalid. Try Again.");
                int.TryParse(Console.ReadLine(), out weight);
            }
            tempGroup.Weight = weight;

            selected.AddAssignmentGroup(tempGroup);
        }
        //grading stuff
        public void AddSubmissionToCourse(Student student)
        {
            Console.WriteLine("Pick a course:");
            var course = StudentCoursePicker(student);
            submissionHelper.AddSubmission(course, student);
        }
        public void AddGradeSubmission()
        {
            Console.WriteLine("Pick a course:");
            var course = CoursePicker();
            submissionHelper.AddGrade(course);
        }
        public void CalculateGrade(Student student)
        {
            Console.WriteLine("Pick a course:");
            var course = CoursePicker();
            double courseGrade = submissionHelper.CalculateCourseGrade(student, course);
            Console.WriteLine(student.Name + " total grade for " + course + ": " + courseGrade + "%");
        }
        //module stuff
        public void UpdateModule()
        {
            Console.WriteLine("Which course do you want to edit modules for?");
            var selected = CoursePicker();

            bool menu = true;
            while (menu)
            {
                Console.WriteLine("MODULE UPDATE MENU");
                Console.WriteLine("1. Add Module");
                Console.WriteLine("2. List Content Items");
                Console.WriteLine("3. Add Assignment Item");
                Console.WriteLine("4. Add Page Item");
                Console.WriteLine("5. Add File Item");
                Console.WriteLine("6. Delete Item");
                Console.WriteLine("7. Exit");

                string pick = Console.ReadLine() ?? string.Empty;

                if (int.TryParse(pick, out int pickInt))
                {
                    if (pickInt == 1) //add module
                    {
                        CreateModule(selected);
                    }
                    else if (pickInt == 2) //lists and select content item
                    {
                        SelectContent(selected);
                    }
                    else if (pickInt == 3) //add assignment item
                    {
                        AddAssignmentItem(selected);
                    }
                    else if (pickInt == 4) //add page item
                    {
                        AddPageItem(selected);
                    }
                    else if (pickInt == 5) //add file item
                    {
                        AddFileItem(selected);
                    }
                    else if (pickInt == 6) //delete content
                    {
                        DeleteItem(selected);
                    }
                    else if (pickInt == 7) //exits back to main menu
                    {
                        menu = false;
                    }
                }
            }
        }
        public void CreateModule(Course course)
        {
            var tempModule = new Module();
            Console.WriteLine("Enter module name:");
            tempModule.Name = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Enter module description:");
            tempModule.Description = Console.ReadLine() ?? string.Empty;
            
            course.AddModule(tempModule);
        }
        public Module ModulePicker(Course course) //picks module from course
        {
            course.Modules.ForEach(Console.WriteLine);
            var name = Console.ReadLine() ?? string.Empty;
            var selected = course.Modules.FirstOrDefault(m => m.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            return selected;
        }
        public void DeleteItem(Course course) //deletes item
        {
            Console.WriteLine("Which module do you want to delete from? (Enter Name)");
            var module = ModulePicker(course);

            Console.WriteLine("\nMODULE CONTENT");
            module.Content.ForEach(Console.WriteLine);
            Console.WriteLine("Select item to delete:");
            var name = Console.ReadLine() ?? string.Empty;
            var selected = module.Content.FirstOrDefault(c => c.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));

            module.RemoveContent(selected); //removes item
        }
        public void AddAssignmentItem(Course course)
        {
            Console.WriteLine("Which module do you want to add to? (Enter Name)");
            var module = ModulePicker(course);
            var assignmentItem = new AssignmentItem();

            Console.WriteLine("Enter item name:");
            assignmentItem.Name = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Enter item description:");
            assignmentItem.Description = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Which assignment do you want to add?");
            course.Assignments.ForEach(Console.WriteLine);
            int Id;
            while (!int.TryParse(Console.ReadLine(), out Id))
            {
                Console.WriteLine("Invalid. Try Again.");
                int.TryParse(Console.ReadLine(), out Id);
            }
            var selection = course.Assignments.FirstOrDefault(m => m.Id == Id);
            assignmentItem.Assignment = selection;

            module.AddContent(assignmentItem); //adds assignment content item
        }
        public void AddPageItem(Course course)
        {
            Console.WriteLine("Which module do you want to add to? (Enter Name)");
            var module = ModulePicker(course);
            var pageItem = new PageItem();

            Console.WriteLine("Enter item name:");
            pageItem.Name = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Enter item description:");
            pageItem.Description = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Enter HTML Body:");
            pageItem.HTMLBody = Console.ReadLine() ?? string.Empty;

            module.AddContent(pageItem); //adds content item
        }
        public void AddFileItem(Course course)
        {
            Console.WriteLine("Which module do you want to add to? (Enter Name)");
            var module = ModulePicker(course);
            var fileItem = new FileItem();

            Console.WriteLine("Enter item name:");
            fileItem.Name = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Enter item description:");
            fileItem.Description = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Enter File Path:");
            fileItem.FilePath = Console.ReadLine() ?? string.Empty;

            module.AddContent(fileItem); //adds content item
        }
        public void SelectContent(Course course)
        {
            Console.WriteLine("Select module (Enter Name)");
            var module = ModulePicker(course);

            Console.WriteLine("\nMODULE CONTENT");
            module.Content.ForEach(Console.WriteLine);
            Console.WriteLine("Select item:"); //maybe create IDs for item?
            var name = Console.ReadLine() ?? string.Empty;
            var selected = module.Content.FirstOrDefault(c => c.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            if (selected != null)
            {
                Console.WriteLine("\nITEM:");
                Console.WriteLine(selected.DisplayAll());
            }
        }
    }
}
