using System;
using System.Collections.Generic;
using Library.LMS.Models;
/* Tatiana Graciela Cue COP4870-0001*/
namespace Library.LMS.Services
{
    public class CourseService
    {
        public List<Course> SpringList { get; set; } //jan 1 - april 30
        public List<Course> SummerList { get; set; } //may 1 - july 31
        public List<Course> FallList { get; set; } //aug 1 - dec 31
        public Dictionary<string, int> Codes { get; set; }
        public CourseService() 
        {
            SpringList = new List<Course>();
            SummerList = new List<Course>();
            FallList = new List<Course>();
            Codes = new Dictionary<string, int>();
        }
        public bool CheckCode(string c)
        {
            var result = new ArgumentException();
            try { Codes.Add(c, 0); }
            catch (ArgumentException r)
            {
                result = r;
            }
            if (result.Source == null) //true if it doesn't exist
            {
                return true;
            }
            else { return false; }
        }
    }
}
