using Library.LMS.Models;
using Library.LMS.Services;
using System.Collections.Generic;
using UWP.Library.CueLMS;
/* Tatiana Graciela Cue COP4870-0001*/
namespace UWP.CueLMS.ViewModels
{
    public class CourseViewModel
    {
        public CourseViewModel(CourseService service, int s) 
        {
            Course = new Course();
            Service = service;
            semester = s;
        }
        private Course Course { get; set; }
        private int semester { get; set; }
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
        public async void AddCourse()
        {
            Course.Code = Code;
            if (semester == 1) //add to spring
            {
                var handler = new WebRequestHandler();
                await handler.Post("http://localhost:5100/SpringCourse", Course);
            }
            else if (semester == 2) //add to summer
            {
                var handler = new WebRequestHandler();
                await handler.Post("http://localhost:5100/SummerCourse", Course);
            }
            else if (semester == 3) //add to fall
            {
                var handler = new WebRequestHandler();
                await handler.Post("http://localhost:5100/FallCourse", Course);
            }
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
