using Library.LMS.Models;
using System.Collections.Generic;
/* Tatiana Graciela Cue COP4870-0001*/
namespace UWP.CueLMS.ViewModels
{
    public class ModuleViewModel
    {
        public ModuleViewModel(List<Module> modules)
        {
            Module = new Module();
            Modules = modules;
        }
        public Module Module { get; set; }
        public List<Module> Modules { get; set; }
        public string Name
        {
            set { Module.Name = value; }
        }
        public string Description
        {
            set { Module.Description = value; }
        }
        public void AddModule()
        {
            Modules.Add(Module);
        }
    }
}
