using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Library.LMS.Models;
using Newtonsoft.Json;
using UWP.Library.CueLMS;
/* Tatiana Graciela Cue COP4870-0001*/
namespace Library.LMS.Services
{
    public class CourseService
    {
        public List<Course> SpringList 
        { 
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5100/SpringCourse").Result;
                return JsonConvert.DeserializeObject<List<Course>>(payload).OrderBy(x => x.Id).ToList();
            }
        } //jan 1 - april 30
        public List<Course> SummerList
        { 
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5100/SummerCourse").Result;
                return JsonConvert.DeserializeObject<List<Course>>(payload).OrderBy(x => x.Id).ToList();
            }
        } //may 1 - july 31
        public List<Course> FallList 
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5100/FallCourse").Result;
                return JsonConvert.DeserializeObject<List<Course>>(payload).OrderBy(x => x.Id).ToList();
            }
        } //aug 1 - dec 31
        public Dictionary<string, int> Codes { get; set; }
        public CourseService() 
        {
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
