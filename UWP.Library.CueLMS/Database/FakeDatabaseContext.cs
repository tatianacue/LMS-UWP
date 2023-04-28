using Library.LMS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace UWP.Library.CueLMS.Database
{
    /* Tatiana Graciela Cue COP4870-0001*/
    public static class FakeDatabaseContext
    {
        public static List<Student> Students = new List<Student>();
        public static List<TeachingAssistant> TeachingAssistants = new List<TeachingAssistant>();
        public static List<Instructor> Instructors = new List<Instructor>();
        public static List<Course> SpringCourses = new List<Course>();
        public static List<Course> SummerCourses = new List<Course>();
        public static List<Course> FallCourses = new List<Course>();
        public static List<int> CourseIds = new List<int>();
        public static List<int> ModuleIds = new List<int>();
        public static List<int> AssignmentGroupIds = new List<int>();
        public static List<int> PersonIds = new List<int>();
        public static List<int> ContentItemIds = new List<int>();
    }
}
