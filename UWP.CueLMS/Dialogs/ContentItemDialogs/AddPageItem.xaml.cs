using Library.LMS.Models;
using System.Collections.Generic;
using UWP.CueLMS.ViewModels.ModuleStuff;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238
/* Tatiana Graciela Cue COP4870-0001*/
namespace UWP.CueLMS.Dialogs.ContentItemDialogs
{
    public sealed partial class AddPageItem : ContentDialog
    {
        public AddPageItem(Course course)
        {
            this.InitializeComponent();
            DataContext = new PageItemViewModel(course);
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            (DataContext as PageItemViewModel).AddItem();
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
