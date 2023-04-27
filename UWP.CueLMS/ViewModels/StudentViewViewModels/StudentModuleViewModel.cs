using Library.LMS.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UWP.Library.CueLMS;
/* Tatiana Graciela Cue COP4870-0001*/
namespace UWP.CueLMS.ViewModels.StudentViewViewModels
{
    public class StudentModuleViewModel
    {
        public StudentModuleViewModel(Module module) 
        {
            Module = module;
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
        public List<ContentItem> contentList 
        { 
            get
            {
                var id = Module.Id;
                var payload = new WebRequestHandler().Get($"http://localhost:5100/Module/GetContent/{id}").Result;
                var filelist = JsonConvert.DeserializeObject<List<FileItem>>(payload);
                var pagelist = JsonConvert.DeserializeObject<List<PageItem>>(payload);
                var assignmentlist = JsonConvert.DeserializeObject<List<AssignmentItem>>(payload);
                var content = new List<ContentItem>();
                foreach (var item in filelist) //make sure type is derived type
                {
                    if (item.Type == 0)
                    {
                        content.Add(item);
                    }
                }
                foreach (var item in pagelist)
                {
                    if (item.Type == 1)
                    {
                        content.Add(item);
                    }
                }
                foreach (var item in assignmentlist)
                {
                    if (item.Type == 2)
                    {
                        content.Add(item);
                    }
                }
                return content.OrderBy(x => x.Id).ToList(); //default list order
            }
        }
        public ObservableCollection<ContentItem> Content 
        { 
            get 
            {
                return new ObservableCollection<ContentItem>(contentList);
            } 
        }
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
