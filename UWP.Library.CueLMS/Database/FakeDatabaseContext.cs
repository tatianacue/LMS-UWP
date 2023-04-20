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
            new Course {Code = "COP4080", Name = "C#", Description = "stuff", CreditHours = 3, RoomLocation = "room1"},
            new Course {Code = "CDA3100", Name = "Computer Org", Description = "work", CreditHours = 3, RoomLocation = "room2"}
        };
    }
}
