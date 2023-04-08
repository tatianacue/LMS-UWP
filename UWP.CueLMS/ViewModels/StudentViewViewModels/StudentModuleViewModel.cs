using Library.LMS.Models;
using System.Collections.ObjectModel;

namespace UWP.CueLMS.ViewModels.StudentViewViewModels
{
    public class StudentModuleViewModel
    {
        public StudentModuleViewModel(Module module) 
        {
            Module = module;
            Content = new ObservableCollection<ContentItem>(Module.Content);
        }
        public Module Module { get; set; }
        public string ModuleName { 
            get
            {
                if (Module != null)
                {
                    return Module.Name;
                }
                else { return string.Empty; }
            }
        }
        public ObservableCollection<ContentItem> Content { get; set; }
        public ContentItem SelectedItem { get; set; }
    }
}
