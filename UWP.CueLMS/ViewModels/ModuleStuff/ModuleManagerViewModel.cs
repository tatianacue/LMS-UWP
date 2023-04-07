using Library.LMS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UWP.CueLMS.Dialogs.ContentItemDialogs;

namespace UWP.CueLMS.ViewModels.ModuleStuff
{
    public class ModuleManagerViewModel : ObservableObject
    {
        public ModuleManagerViewModel(Module module, ObservableCollection<Assignment> assignments) 
        {
            Module = module;
            Items = new ObservableCollection<ContentItem>(Module.Content);
            Assignments = assignments;
        }
        public Module Module { get; set; }
        public ObservableCollection<ContentItem> Items { get; set; }
        public ObservableCollection<Assignment> Assignments { get; set; }
        public ContentItem SelectedItem { get; set; }
        public async void AddPageItem()
        {
            var dialog = new AddPageItem(Module.Content);
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            AutoRefresh();
        }
        public async void AddFileItem()
        {
            var dialog = new AddFileItem(Module.Content);
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            AutoRefresh();
        }
        public async void AddAssignmentItem()
        {
            var dialog = new AddAssignmentItem(Module.Content, Assignments);
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            AutoRefresh();
        }
        public void AutoRefresh()
        {
            var copy = Module.Content;
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
        public void UpdateName()
        {
            SelectedItem.Name = NewName;
            NotifyPropertyChanged(nameof(Name));
        }
        public void UpdateDescription()
        {
            SelectedItem.Description = NewDescription;
            NotifyPropertyChanged(nameof(Description));
        }
        public void UpdateFilePath()
        {
            var fileitem = (FileItem)SelectedItem;
            fileitem.FilePath = NewFilePath;
            NotifyPropertyChanged(nameof(FilePath));
        }
        public void UpdateHTMLBody()
        {
            var pageitem = (PageItem)SelectedItem;
            pageitem.HTMLBody = NewHTMLBody;
            NotifyPropertyChanged(nameof(HTMLBody));
        }
        public void UpdateAssignment()
        {
            var assignmentitem = (AssignmentItem)SelectedItem;
            assignmentitem.Assignment = NewAssignment;
            NotifyPropertyChanged(nameof(Assignment));
        }
    }
}
