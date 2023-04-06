using Library.LMS.Models;
using System.Collections.Generic;
using UWP.CueLMS.ViewModels.ModuleStuff;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWP.CueLMS.Dialogs.ContentItemDialogs
{
    public sealed partial class AddFileItem : ContentDialog
    {
        public AddFileItem(List<ContentItem> list)
        {
            this.InitializeComponent();
            DataContext = new FileItemViewModel(list);
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            (DataContext as FileItemViewModel).AddItem();
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
