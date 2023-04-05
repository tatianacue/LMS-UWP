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
        public List<Course> SpringList { get; set; } //jan 1 - april 30
        public List<Course> SummerList { get; set; } //may 1 - july 31
        public List<Course> FallList { get; set; } //aug 1 - dec 31
        public Dictionary<string, int> Codes { get; set; }
        public CourseService() 
        { 
            courseList = new List<Course>();
            SpringList = new List<Course>() 
            { 
                new Course{Code="COP4080", Name="Computers", CreditHours=3, Description="Stuff", RoomLocation="PaperStreet" }, //temp
                new Course{Code="CNT70", Name="comppooo", CreditHours=2, Description="idk", RoomLocation="SoapCompany" } //temp
            };
            SummerList = new List<Course>();
            FallList = new List<Course>();
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
