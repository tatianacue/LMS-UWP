using Library.LMS.Models;
using Library.LMS.Services;
using System.Collections.Generic;

namespace UWP.CueLMS.ViewModels
{
    public class CourseViewModel
    {
        public CourseViewModel(List<Course> courses, CourseService service) 
        {
            Course = new Course();
            Service = service;
            Courses = courses; //list of courses depending on semester
        }
        private Course Course { get; set; }
        private CourseService Service { get; set; }
        private List<Course> Courses {  get; set; }
        public string Name
        {
            set { Course.Name = value; }
        }
        public string Code
        {
            get { return code; }
            set { code = value.ToUpper(); }
        }
        private string code { get; set; }
        public int CreditHours
        {
            set { Course.CreditHours = value; }
        }
        public string Description
        {
            set { Course.Description = value; }
        }
        public string Room
        {
            set { Course.RoomLocation = value; }
        }
        public void AddCourse()
        {
            Course.Code = Code;
            Courses.Add(Course);
        }
        public bool CheckCode() //checks if Id doesnt exist
        {
            if (Service.CheckCode(Code) == true) //if it doesnt exist
            {
                return true;
            }
            else //if it does
            {
                return false;
            }
        }
    }
}
