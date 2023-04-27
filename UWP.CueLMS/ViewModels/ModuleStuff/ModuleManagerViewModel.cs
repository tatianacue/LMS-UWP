using Library.LMS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using UWP.CueLMS.Dialogs.ContentItemDialogs;
using UWP.Library.CueLMS;
/* Tatiana Graciela Cue COP4870-0001*/
namespace UWP.CueLMS.ViewModels.ModuleStuff
{
    public class ModuleManagerViewModel : ObservableObject
    {
        public ModuleManagerViewModel(Module module, ObservableCollection<Assignment> assignments, Course course) 
        {
            Module = module;
            Course = course;
            Course.SelectedModule = module;
            Items = new ObservableCollection<ContentItem>(contentList);
            Assignments = assignments;
        }
        public Module Module { get; set; }
        public Course Course { get; set; }
        public string ModuleName { 
            get { 
                    if (Module != null) 
                    { return Module.Name; }
                    else { return string.Empty; }
                }
        }
        public List<ContentItem> contentList
        {
            get
            {
                var list = new List<ContentItem>();
                foreach (var item in fileitemList) //adds file items
                {
                    list.Add(item);
                }
                foreach (var item in pageitemList) //adds page items
                {
                    list.Add(item);
                }
                foreach (var item in assignmentitemList) //adds assignment items
                {
                    list.Add(item);
                }
                return list.OrderBy(x => x.Id).ToList();
            }
        }
        public List<FileItem> fileitemList
        {
            get
            {
                var payload = new WebRequestHandler().Get($"http://localhost:5100/Module/GetFileItems/{Module.Id}").Result;
                return JsonConvert.DeserializeObject<List<FileItem>>(payload);
            }
        }
        public List<PageItem> pageitemList
        {
            get
            {
                var payload = new WebRequestHandler().Get($"http://localhost:5100/Module/GetPageItems/{Module.Id}").Result;
                return JsonConvert.DeserializeObject<List<PageItem>>(payload);
            }
        }
        public List<AssignmentItem> assignmentitemList
        {
            get
            {
                var payload = new WebRequestHandler().Get($"http://localhost:5100/Module/GetAssignmentItems/{Module.Id}").Result;
                return JsonConvert.DeserializeObject<List<AssignmentItem>>(payload);
            }
        }
        public ObservableCollection<ContentItem> Items { get; set; }
        public ObservableCollection<Assignment> Assignments { get; set; }
        public ContentItem SelectedItem { get; set; }
        public async void AddPageItem()
        {
            var dialog = new AddPageItem(Course);
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            AutoRefresh();
        }
        public async void AddFileItem()
        {
            var dialog = new AddFileItem(Course);
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            AutoRefresh();
        }
        public async void AddAssignmentItem()
        {
            var dialog = new AddAssignmentItem(Course, Assignments);
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            AutoRefresh();
        }
        public void AutoRefresh()
        {
            var copy = contentList;
            Items.Clear();
            foreach (var item in copy)
            {
                Items.Add(item);
            }
        }
        public async void DeleteItem()
        {
            Course.SelectedItem = SelectedItem;
            if (SelectedItem is FileItem)
            {
                var handler = new WebRequestHandler();
                await handler.Post("http://localhost:5100/Module/DeleteFileItem", Course, HttpMethod.Delete);
            }
            else if (SelectedItem is PageItem)
            {
                var handler = new WebRequestHandler();
                await handler.Post("http://localhost:5100/Module/DeletePageItem", Course, HttpMethod.Delete);
            }
            else if (SelectedItem is AssignmentItem)
            {
                var handler = new WebRequestHandler();
                await handler.Post("http://localhost:5100/Module/DeleteAssignmentItem", Course, HttpMethod.Delete);
            }
            AutoRefresh();
        }
        //Updates
        public string NewName { get; set; }
        public string NewDescription { get; set; }
        public string NewFilePath { get; set; }
        public string NewHTMLBody { get; set; }
        public Assignment NewAssignment { get; set; }
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
        public string Assignment
        {
            get
            {
                if (SelectedItem != null)
                {
                    var assignmentitem = (AssignmentItem)SelectedItem;
                    return assignmentitem.Assignment.Display;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        public async void UpdateName()
        {
            SelectedItem.Name = NewName;

            if (SelectedItem is FileItem)
            {
                Course.SelectedFileItem = (FileItem)SelectedItem;
                var handler = new WebRequestHandler();
                await handler.Post("http://localhost:5100/Module/AddUpdateFileItem", Course, HttpMethod.Post);
            }
            else if (SelectedItem is PageItem)
            {
                Course.SelectedPageItem = (PageItem)SelectedItem;
                var handler = new WebRequestHandler();
                await handler.Post("http://localhost:5100/Module/AddUpdatePageItem", Course, HttpMethod.Post);
            }
            else if (SelectedItem is AssignmentItem)
            {
                Course.SelectedAssignmentItem = (AssignmentItem)SelectedItem;
                var handler = new WebRequestHandler();
                await handler.Post("http://localhost:5100/Module/AddUpdateAssignmentItem", Course, HttpMethod.Post);
            }
            NotifyPropertyChanged(nameof(Name));
        }
        public async void UpdateDescription()
        {
            SelectedItem.Description = NewDescription;

            if (SelectedItem is FileItem)
            {
                Course.SelectedFileItem = (FileItem)SelectedItem;
                var handler = new WebRequestHandler();
                await handler.Post("http://localhost:5100/Module/AddUpdateFileItem", Course, HttpMethod.Post);
            }
            else if (SelectedItem is PageItem)
            {
                Course.SelectedPageItem = (PageItem)SelectedItem;
                var handler = new WebRequestHandler();
                await handler.Post("http://localhost:5100/Module/AddUpdatePageItem", Course, HttpMethod.Post);
            }
            else if (SelectedItem is AssignmentItem)
            {
                Course.SelectedAssignmentItem = (AssignmentItem)SelectedItem;
                var handler = new WebRequestHandler();
                await handler.Post("http://localhost:5100/Module/AddUpdateAssignmentItem", Course, HttpMethod.Post);
            }
            NotifyPropertyChanged(nameof(Description));
        }
        public async void UpdateFilePath()
        {
            var fileitem = (FileItem)SelectedItem;
            fileitem.FilePath = NewFilePath;
            Course.SelectedFileItem = fileitem;
            var handler = new WebRequestHandler();
            await handler.Post("http://localhost:5100/Module/AddUpdateFileItem", Course, HttpMethod.Post);
            NotifyPropertyChanged(nameof(FilePath));
        }
        public async void UpdateHTMLBody()
        {
            var pageitem = (PageItem)SelectedItem;
            pageitem.HTMLBody = NewHTMLBody;
            Course.SelectedPageItem = pageitem;
            var handler = new WebRequestHandler();
            await handler.Post("http://localhost:5100/Module/AddUpdatePageItem", Course, HttpMethod.Post);
            NotifyPropertyChanged(nameof(HTMLBody));
        }
        public async void UpdateAssignment()
        {
            var assignmentitem = (AssignmentItem)SelectedItem;
            assignmentitem.Assignment = NewAssignment;
            Course.SelectedAssignmentItem = assignmentitem;
            var handler = new WebRequestHandler();
            await handler.Post("http://localhost:5100/Module/AddUpdateAssignmentItem", Course, HttpMethod.Post);
            NotifyPropertyChanged(nameof(Assignment));
        }
    }
}
