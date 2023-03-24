using Library.LMS.Models;
using Library.LMS.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWP.CueLMS.ViewModels
{
    public class InstructorViewModel
    {
        public InstructorViewModel() 
        {
            courseService = new CourseService();
            personService = new PersonService();
        }    
        private CourseService courseService { get; set; }
        private PersonService personService { get; set; }
        public List<Course> Courses { get { return courseService.courseList; } }
        public List<Person> People { get { return personService.personList; } }
        public string Query { get; set; }
    }
}
