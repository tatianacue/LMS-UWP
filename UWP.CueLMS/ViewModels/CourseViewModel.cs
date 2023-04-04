﻿using Library.LMS.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWP.CueLMS.ViewModels
{
    public class CourseViewModel
    {
        public CourseViewModel(List<Course> courseList) 
        {
            Course = new Course();
            courses = courseList;
        }
        private Course Course { get; set; }
        private List<Course> courses {  get; set; }
        public string Name
        {
            set { Course.Name = value; }
        }
        public string Code
        {
            set { Course.Code = value; }
        }
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
            courses.Add(Course);
        }
    }
}
