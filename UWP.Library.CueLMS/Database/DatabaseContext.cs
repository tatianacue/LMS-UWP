using Library.LMS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace UWP.Library.CueLMS.Database
{
    public static class DatabaseContext
    {
        public static List<Person> People = new List<Person>() {
                new Student{ ID = "tbc20n", Name = "Tat", Classification = "Junior"},
                new Instructor{ ID = "tlt50n", Name = "Top"},
                new TeachingAssistant { ID = "prt60N", Name = "Patrick" }
            };
    }
}
