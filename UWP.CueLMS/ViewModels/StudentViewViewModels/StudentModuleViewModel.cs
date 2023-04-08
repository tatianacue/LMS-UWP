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
        //content item stuff
        public string Name
        {
            get
            {
                if (SelectedItem != null)
                {
                    return SelectedItem.Name;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        public string Description
        {
            get
            {
                if (SelectedItem != null)
                {
                    return SelectedItem.Description;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        public string FilePath
        {
            get
            {
                if (SelectedItem != null)
                {
                    var fileitem = (FileItem)SelectedItem;
                    return fileitem.FilePath;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        public string HTMLBody
        {
            get
            {
                if (SelectedItem != null)
                {
                    var pageitem = (PageItem)SelectedItem;
                    return pageitem.HTMLBody;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        public string AssignmentName
        {
            get
            {
                if (SelectedItem != null)
                {
                    var aitem = (AssignmentItem)SelectedItem;
                    return aitem.Assignment.Name;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        public string AssignmentDescription
        {
            get
            {
                if (SelectedItem != null)
                {
                    var aitem = (AssignmentItem)SelectedItem;
                    return aitem.Assignment.Description;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        public int AssignmentPoints
        {
            get
            {
                if (SelectedItem != null)
                {
                    var aitem = (AssignmentItem)SelectedItem;
                    return aitem.Assignment.TotalAvailablePoints;
                }
                else
                {
                    return 0;
                }
            }
        }
        public string AssignmentDueDate
        {
            get
            {
                if (SelectedItem != null)
                {
                    var aitem = (AssignmentItem)SelectedItem;
                    return aitem.Assignment.DueDate;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }
}
