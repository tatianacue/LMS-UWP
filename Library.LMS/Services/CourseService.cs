using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/* Tatiana Graciela Cue COP4870-0001*/
namespace Library.LMS.Services
{
    public class CourseService
    {
        public List<Course> courseList { get; set; }
        public CourseService() 
        { 
            courseList = new List<Course>();
        }
        public void Add(Course course) //adds course to course list
        {
            courseList.Add(course);
        }
    }
}
