using System;
using System.Collections;
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
        public Dictionary<string, int> Codes { get; set; }
        public CourseService() 
        { 
            courseList = new List<Course>();
            Codes = new Dictionary<string, int>();
        }
        public void Add(Course course) //adds course to course list
        {
            courseList.Add(course);
        }
        public bool CheckCode(string c)
        {
            var result = new ArgumentException();
            try { Codes.Add(c, 0); }
            catch (ArgumentException r)
            {
                result = r;
            }
            if (result.Source == null) //if key already exists
            {
                return true;
            }
            else { return false; }
        }
        public List<Course> Search(string srch) //searches for course based on string
        {
            var searchCourse = courseList.Where(t => t.Name.Contains(srch) ||
             t.Description.Contains(srch));
            List<Course> results = searchCourse.ToList(); //enumerable to list

            return results;
        }
    }
}
