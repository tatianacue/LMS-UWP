using Library.LMS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace UWP.Library.CueLMS.Database
{
    public static class DatabaseContext
    {
        public static List<Person> People = new List<Person>() {
                new Student{ ID = "tbc20n", Name = "Tat", Classification = "Junior", IdNumber=1},
                new Instructor{ ID = "tlt50n", Name = "Top", IdNumber=2},
                new TeachingAssistant { ID = "prt60N", Name = "Patrick", IdNumber=3}
            };
    }
}
