using Library.LMS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace UWP.Library.CueLMS.Database
{
    public static class FakeDatabaseContext
    {
        public static List<Person> People = new List<Person>() 
        {
            new Student{ ID = "tbc20n", Name = "Tat", Classification = "Junior", IdNumber=1},
            new Instructor{ ID = "tlt50n", Name = "Top", IdNumber=2},
            new TeachingAssistant { ID = "prt60N", Name = "Patrick", IdNumber=3}
        };
        public static List<Course> SpringCourses = new List<Course>()
        {
            new Course {Code = "COP4080", Name = "C#", Description = "stuff", CreditHours = 3, RoomLocation = "room1", Id=1},
            new Course {Code = "CDA3100", Name = "Computer Org", Description = "work", CreditHours = 3, RoomLocation = "room2", Id=2}
        };
        public static List<Course> SummerCourses = new List<Course>()
        {
            new Course {Code = "summer1", Name = "SUMMER", Description = "stuff", CreditHours = 3, RoomLocation = "room1", Id=3},
            new Course {Code = "summer2", Name = "SUMMER CLASS", Description = "work", CreditHours = 3, RoomLocation = "room2", Id=4}
        };
        public static List<Course> FallCourses = new List<Course>()
        {
            new Course {Code = "fall1", Name = "FALL", Description = "stuff", CreditHours = 3, RoomLocation = "room1", Id = 5},
            new Course {Code = "fall2", Name = "FALL CLASS", Description = "work", CreditHours = 3, RoomLocation = "room2", Id = 6}
        };
        public static List<int> CourseIds = new List<int>();
    }
}
