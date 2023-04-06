using Library.LMS.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using UWP.CueLMS.Dialogs.ContentItemDialogs;

namespace UWP.CueLMS.ViewModels.ModuleStuff
{
    public class ModuleManagerViewModel
    {
        public ModuleManagerViewModel(Module module) 
        {
            Module = module;
            Items = new ObservableCollection<ContentItem>(Module.Content);
        }
        public Module Module { get; set; }
        public ObservableCollection<ContentItem> Items { get; set; }
        public async void AddPageItem()
        {
            var dialog = new AddPageItem(Module.Content);
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
    }
}
