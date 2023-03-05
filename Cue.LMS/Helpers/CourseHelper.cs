using Library.LMS;
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
        private ListNavigator<Course> listNavigator;
        private CourseService courseService;
        private SubmissionHelper submissionHelper;
        public CourseHelper()
        {
            courseService = new CourseService();
            submissionHelper = new SubmissionHelper();
            listNavigator = new ListNavigator<Course>(courseService.courseList);
        }
        private Course CoursePicker() //course selection menu
        {
            Console.WriteLine("SELECT A COURSE:");
            ListOptions();
            Console.WriteLine("Which course would you like to select?");
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
            var navigator = new ListNavigator<Course>(studentCourses);
            Console.WriteLine("SELECT A COURSE:");
            ListOptions(navigator); //puts through list navigator
            Console.WriteLine("Which course would you like to select?");
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

            Console.WriteLine("Enter number of credit hours:");
            int creditHours;
            while (!int.TryParse(Console.ReadLine(), out creditHours))
            {
                Console.WriteLine("Invalid. Try Again.");
                int.TryParse(Console.ReadLine(), out creditHours);
            }
            newCourse.CreditHours = creditHours;

            Console.WriteLine("Enter a Description:");
            newCourse.Description = Console.ReadLine() ?? string.Empty;

            courseService.Add(newCourse);
        }
        public void AddStudentToCourse(Student student) //takes in student and adds to course
        {
            var selected = CoursePicker();

            selected.AddStudent(student);
            Console.WriteLine(student.Name + " added to " + selected.Code.ToUpper()); //says added to list
        }
        public void RemoveStudent() //removes student from course
        {
            var selected = CoursePicker();
            selected.RemoveStudent();
        }
        public void SearchForCourse(string ? srch = null) //searches for course and allows you to select it
        {
            if (string.IsNullOrEmpty(srch))
            {
                ListOptions();
            }
            else
            {
                var list = courseService.Search(srch);
                var navigator = new ListNavigator<Course>(list);
                ListOptions(navigator);
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
        public List<Course> ListStudentCourses(Student student) //find student in course
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
            return studentCourses;
        }
        public void AddAssignment() //add assignment to a course
        {
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
        public void ListOptions(ListNavigator<Course>? list = null)
        {
            ListNavigator<Course> navigator;
            if (list != null)
            {
                navigator = list; //if its another list
            }
            else
            {
                navigator = listNavigator; //make it normal list
            }
            var display = navigator.GoToFirstPage();
            foreach (var course in display)
            {
                Console.WriteLine(course.Value);
            }
            bool menu = true;
            while (menu)
            {
                Console.WriteLine("(1)BACK    (2)NEXT     (3)SELECT/EXIT");
                string pick = Console.ReadLine() ?? string.Empty;

                if (int.TryParse(pick, out int pickInt))
                {
                    if (pickInt == 1) //goes back
                    {
                        display = navigator.GoBackward();
                        foreach (var course in display)
                        {
                            Console.WriteLine(course.Value);
                        }
                    }
                    else if (pickInt == 2) //go next
                    {
                        display = navigator.GoForward();
                        foreach (var course in display)
                        {
                            Console.WriteLine(course.Value);
                        }
                    }
                    else if (pickInt == 3)
                    {
                        menu = false;
                    }
                }
            }
        }
        //grading stuff
        public void AddSubmissionToCourse(Student student)
        {
            var course = StudentCoursePicker(student);
            submissionHelper.AddSubmission(course, student);
        }
        public void AddGradeSubmission()
        {
            var course = CoursePicker();
            submissionHelper.AddGrade(course);
        }
        public void CalculateGrade(Student student)
        {
            var course = CoursePicker();
            double courseGrade = submissionHelper.CalculateCourseGrade(student, course);
            string letterGrade;
            if(courseGrade >= 96) 
            {
                letterGrade = "A+";
            }
            else if(courseGrade <= 95 && courseGrade >= 91)
            {
                letterGrade = "A";
            }
            else if(courseGrade <= 91 && courseGrade >= 90)
            {
                letterGrade = "A-";
            }
            else if(courseGrade <= 89 && courseGrade >= 86)
            {
                letterGrade = "B+";
            }
            else if (courseGrade <= 85 && courseGrade >= 81)
            {
                letterGrade = "B";
            }
            else if (courseGrade <= 81 && courseGrade >= 80)
            {
                letterGrade = "B-";
            }
            else if (courseGrade <= 79 && courseGrade >= 76)
            {
                letterGrade = "C+";
            }
            else if (courseGrade <= 75 && courseGrade >= 71)
            {
                letterGrade = "C";
            }
            else if (courseGrade <= 71 && courseGrade >= 70)
            {
                letterGrade = "C-";
            }
            else if (courseGrade <= 69 && courseGrade >= 66)
            {
                letterGrade = "D+";
            }
            else if (courseGrade <= 65 && courseGrade >= 61)
            {
                letterGrade = "D";
            }
            else if (courseGrade <= 61 && courseGrade >= 60)
            {
                letterGrade = "D-";
            }
            else
            {
                letterGrade = "F";
            }
            Console.WriteLine(student.Name + " total grade for " + course + ": " + courseGrade + "% " + letterGrade);
        }
        //module stuff
        public void UpdateModule()
        {
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
                Console.WriteLine("7. Update Item");
                Console.WriteLine("8. Exit");

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
                    else if (pickInt == 7) //update item
                    {
                        UpdateItem(selected);
                    }
                    else if (pickInt == 8) //exits back to main menu
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
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid. Try Again.");
                int.TryParse(Console.ReadLine(), out id);
            }
            var selected = course.Modules.FirstOrDefault(m => m.Id == id);
            return selected;
        }
        public void DeleteItem(Course course) //deletes item
        {
            Console.WriteLine("Which module do you want to delete from? (Enter Name)");
            var module = ModulePicker(course);

            Console.WriteLine("MODULE CONTENT");
            module.Content.ForEach(Console.WriteLine);
            Console.WriteLine("Select item to delete:");
            var name = Console.ReadLine() ?? string.Empty;
            var selected = module.Content.FirstOrDefault(c => c.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));

            module.RemoveContent(selected); //removes item
        }
        public void AddAssignmentItem(Course course)
        {
            Console.WriteLine("Which module do you want to add to?");
            var module = ModulePicker(course);
            var assignmentItem = new AssignmentItem();

            Console.WriteLine("Enter assignment item name:");
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
            Console.WriteLine("Which module do you want to add to?");
            var module = ModulePicker(course);
            var pageItem = new PageItem();

            Console.WriteLine("Enter page item name:");
            pageItem.Name = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Enter item description:");
            pageItem.Description = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Enter HTML Body:");
            pageItem.HTMLBody = Console.ReadLine() ?? string.Empty;

            module.AddContent(pageItem); //adds content item
        }
        public void AddFileItem(Course course)
        {
            Console.WriteLine("Which module do you want to add to?");
            var module = ModulePicker(course);
            var fileItem = new FileItem();

            Console.WriteLine("Enter file item name:");
            fileItem.Name = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Enter item description:");
            fileItem.Description = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Enter file path:");
            fileItem.FilePath = Console.ReadLine() ?? string.Empty;

            module.AddContent(fileItem); //adds content item
        }
        public void SelectContent(Course course)
        {
            Console.WriteLine("Select module:");
            var module = ModulePicker(course);

            module.Content.ForEach(Console.WriteLine);
            Console.WriteLine("Select item (Enter Id):");
            var name = Console.ReadLine() ?? string.Empty;
            var selected = module.Content.FirstOrDefault(c => c.Id.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            if (selected != null)
            {
                Console.WriteLine("ITEM:");
                Console.WriteLine(selected.DisplayAll());
            }
        }
        public void UpdateItem(Course course)
        {
            Console.WriteLine("Select module:");
            var module = ModulePicker(course);
            module.Content.ForEach(Console.WriteLine);
            Console.WriteLine("Select item to update (Enter Id):");
            var name = Console.ReadLine() ?? string.Empty;
            var selected = module.Content.FirstOrDefault(c => c.Id.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            if (selected != null)
            {
                if(selected is AssignmentItem) //update assignment item
                {
                    Console.WriteLine("Would you like to update the (N)ame, (D)escription, or (A)ssignment?");
                    string choiceChar = Console.ReadLine() ?? string.Empty;
                    if (choiceChar.Equals("n", StringComparison.InvariantCultureIgnoreCase))
                    {
                        Console.WriteLine("Enter new name:");
                        selected.Name = Console.ReadLine() ?? string.Empty;
                    }
                    else if (choiceChar.Equals("d", StringComparison.InvariantCultureIgnoreCase))
                    {
                        Console.WriteLine("Enter new description:");
                        selected.Description = Console.ReadLine() ?? string.Empty;
                    }
                    else if (choiceChar.Equals("a", StringComparison.InvariantCultureIgnoreCase))
                    {
                        Console.WriteLine("Pick new assignment:");
                        course.Assignments.ForEach(Console.WriteLine);
                        int Id;
                        while (!int.TryParse(Console.ReadLine(), out Id))
                        {
                            Console.WriteLine("Invalid. Try Again.");
                            int.TryParse(Console.ReadLine(), out Id);
                        }
                        var selection = course.Assignments.FirstOrDefault(m => m.Id == Id);
                        AssignmentItem item = (AssignmentItem)selected;
                        item.Assignment = selection;
                    }
                }
                else if(selected is PageItem) //update page item
                {
                    Console.WriteLine("Would you like to update the (N)ame, (D)escription, or (H)TML body?");
                    string choiceChar = Console.ReadLine() ?? string.Empty;
                    if (choiceChar.Equals("n", StringComparison.InvariantCultureIgnoreCase))
                    {
                        Console.WriteLine("Enter new name:");
                        selected.Name = Console.ReadLine() ?? string.Empty;
                    }
                    else if (choiceChar.Equals("d", StringComparison.InvariantCultureIgnoreCase))
                    {
                        Console.WriteLine("Enter new description:");
                        selected.Description = Console.ReadLine() ?? string.Empty;
                    }
                    else if (choiceChar.Equals("h", StringComparison.InvariantCultureIgnoreCase))
                    {
                        Console.WriteLine("Enter new HTML body:");
                        PageItem item = (PageItem)selected;
                        item.HTMLBody = Console.ReadLine() ?? string.Empty;
                    }
                }
                else if (selected is FileItem) //update file item
                {
                    Console.WriteLine("Would you like to update the (N)ame, (D)escription, or (F)ile Path?");
                    string choiceChar = Console.ReadLine() ?? string.Empty;
                    if (choiceChar.Equals("n", StringComparison.InvariantCultureIgnoreCase))
                    {
                        Console.WriteLine("Enter new name:");
                        selected.Name = Console.ReadLine() ?? string.Empty;
                    }
                    else if (choiceChar.Equals("d", StringComparison.InvariantCultureIgnoreCase))
                    {
                        Console.WriteLine("Enter new description:");
                        selected.Description = Console.ReadLine() ?? string.Empty;
                    }
                    else if (choiceChar.Equals("f", StringComparison.InvariantCultureIgnoreCase))
                    {
                        Console.WriteLine("Enter new File Path:");
                        FileItem item = (FileItem)selected;
                        item.FilePath = Console.ReadLine() ?? string.Empty;
                    }
                }
            }
        }
        //announcements stuff
        public void ShowAnnouncmentsMenu()
        {
            var selected = CoursePicker();
            bool menu = true;
            while (menu)
            {
                Console.WriteLine("ANNOUNCEMENTS MENU");
                Console.WriteLine("1. Create Announcement");
                Console.WriteLine("2. Delete Announcement");
                Console.WriteLine("3. List All Announcements");
                Console.WriteLine("4. Update Announcement");
                Console.WriteLine("5. Exit");

                string pick = Console.ReadLine() ?? string.Empty;

                if (int.TryParse(pick, out int pickInt))
                {
                    if (pickInt == 1) //create and add announcement
                    {
                        CreateAnnouncement(selected);
                    }
                    else if (pickInt == 2) //delete
                    {
                        DeleteAnnouncement(selected);
                    }
                    else if (pickInt == 3) //list and read announcement
                    {
                        ListAnnouncements(selected);
                    }
                    else if (pickInt == 4) //update announcement
                    {
                        UpdateAnnouncement(selected);
                    }
                    else if (pickInt == 5) //exits back to main menu
                    {
                        menu = false;
                    }
                }
            }
        }
        public void CreateAnnouncement(Course course) //creates announcement and adds to announcement list in course
        {
            Announcement tempAnnounce = new Announcement();
            Console.WriteLine("Enter announcement title:");
            tempAnnounce.Title = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Enter announcement:");
            tempAnnounce.Text = Console.ReadLine() ?? string.Empty;
            course.AddAnnouncement(tempAnnounce);
        }
        public void ListAnnouncements(Course course)
        {
            course.Announcements.ForEach(Console.WriteLine);
            Console.WriteLine("Which announcement would you like to read? (Enter Id)");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid. Try Again.");
                int.TryParse(Console.ReadLine(), out id);
            }
            var selected = course.Announcements.FirstOrDefault(c => c.Id == id);
            if (selected != null)
            {
                Console.WriteLine(selected.DisplayAll()); //displays full announcement
            }
        }
        public void DeleteAnnouncement(Course course) //deletes announcement
        {
            course.Announcements.ForEach(Console.WriteLine);
            Console.WriteLine("Which announcement would you like to delete? (Enter Id)");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid. Try Again.");
                int.TryParse(Console.ReadLine(), out id);
            }
            var selected = course.Announcements.FirstOrDefault(c => c.Id == id);
            if (selected != null)
            {
                course.RemoveAnnouncement(selected);
            }
        }
        public void UpdateAnnouncement(Course course)
        {
            course.Announcements.ForEach(Console.WriteLine);
            Console.WriteLine("Which announcement would you like to update? (Enter Id)");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid. Try Again.");
                int.TryParse(Console.ReadLine(), out id);
            }
            var selected = course.Announcements.FirstOrDefault(c => c.Id == id);
            if (selected != null)
            {
                Console.WriteLine("Which would you like to update? (T)itle or (B)ody?");
                string choice = Console.ReadLine() ?? string.Empty;
                if (choice.Equals("t", StringComparison.InvariantCultureIgnoreCase))
                {
                    Console.WriteLine("Enter new title:");
                    selected.Title = Console.ReadLine() ?? string.Empty;
                }
                else if (choice.Equals("b", StringComparison.InvariantCultureIgnoreCase))
                {
                    Console.WriteLine("Enter new text body:");
                    selected.Text = Console.ReadLine() ?? string.Empty;
                }
                Console.WriteLine("Updated - " + selected.DisplayAll());
            }
        }
    }
}
