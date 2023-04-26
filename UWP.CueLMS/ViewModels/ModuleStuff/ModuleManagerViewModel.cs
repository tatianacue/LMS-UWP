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
        public void DeleteItem()
        {
            Module.Content.Remove(SelectedItem);
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

            Course.SelectedItem = SelectedItem;
            var handler = new WebRequestHandler();
            await handler.Post("http://localhost:5100/Module/PostContent", Course, HttpMethod.Post);
            NotifyPropertyChanged(nameof(Name));
        }
        public async void UpdateDescription()
        {
            SelectedItem.Description = NewDescription;

            Course.SelectedItem = SelectedItem;
            var handler = new WebRequestHandler();
            await handler.Post("http://localhost:5100/Module/PostContent", Course, HttpMethod.Post);
            NotifyPropertyChanged(nameof(Description));
        }
        public async void UpdateFilePath()
        {
            var fileitem = (FileItem)SelectedItem;
            fileitem.FilePath = NewFilePath;

            Course.SelectedItem = fileitem;
            var handler = new WebRequestHandler();
            await handler.Post("http://localhost:5100/Module/PostContent", Course, HttpMethod.Post);
            NotifyPropertyChanged(nameof(FilePath));
        }
        public async void UpdateHTMLBody()
        {
            var pageitem = (PageItem)SelectedItem;
            pageitem.HTMLBody = NewHTMLBody;

            Course.SelectedItem = pageitem;
            var handler = new WebRequestHandler();
            await handler.Post("http://localhost:5100/Module/PostContent", Course, HttpMethod.Post);
            NotifyPropertyChanged(nameof(HTMLBody));
        }
        public async void UpdateAssignment()
        {
            var assignmentitem = (AssignmentItem)SelectedItem;
            assignmentitem.Assignment = NewAssignment;

            Course.SelectedItem = assignmentitem;
            var handler = new WebRequestHandler();
            await handler.Post("http://localhost:5100/Module/PostContent", Course, HttpMethod.Post);
            NotifyPropertyChanged(nameof(Assignment));
        }
    }
}
