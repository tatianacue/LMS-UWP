using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.LMS.Models;
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
        public List<Course> Search(string srch) //searches for course based on string
        {
            var searchCourse = courseList.Where(t => t.Name.Contains(srch, StringComparison.InvariantCultureIgnoreCase) ||
             t.Description.Contains(srch, StringComparison.InvariantCultureIgnoreCase));
            List<Course> results = searchCourse.ToList(); //enumerable to list

            return results;
        }
    }
}
