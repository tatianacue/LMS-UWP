using Library.LMS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UWP.CueLMS.Dialogs.ContentItemDialogs;

namespace UWP.CueLMS.ViewModels.ModuleStuff
{
    public class ModuleManagerViewModel
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
    }
}
