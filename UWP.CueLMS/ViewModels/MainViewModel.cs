using Library.LMS.Models;
using Library.LMS.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UWP.CueLMS.Dialogs;
using Windows.Graphics.Printing.PrintSupport;
using Windows.UI.Xaml.Controls.Primitives;

namespace UWP.CueLMS.ViewModels
{
    public class MainViewModel
    {
        public MainViewModel() 
        { 
            courseService = new CourseService();
            personService = new PersonService();
            Services = new Dictionary<CourseService, PersonService>() { {courseService, personService} };
        }
        public CourseService courseService { get; set; }
        public PersonService personService { get; set; }
        public Dictionary<CourseService, PersonService> Services { get; set; }
    }
}
