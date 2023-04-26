using Library.LMS.Models;
using System.Collections.Generic;
using System.Net.Http;
using UWP.Library.CueLMS;
/* Tatiana Graciela Cue COP4870-0001*/
namespace UWP.CueLMS.ViewModels
{
    public class ModuleViewModel
    {
        public ModuleViewModel(Course course)
        {
            Module = new Module();
            Course = course;
        }
        public Module Module { get; set; }
        public List<Module> Modules { get; set; }
        public Course Course { get; set; }
        public string Name
        {
            set { Module.Name = value; }
        }
        public string Description
        {
            set { Module.Description = value; }
        }
        public async void AddModule()
        {
            Course.SelectedModule = Module;
            var handler = new WebRequestHandler();
            await handler.Post("http://localhost:5100/Module", Course, HttpMethod.Post);
        }
    }
}
