using Library.LMS.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UWP.CueLMS.ViewModels.ModuleStuff;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238
/* Tatiana Graciela Cue COP4870-0001*/
namespace UWP.CueLMS.Dialogs.ContentItemDialogs
{
    public sealed partial class AddAssignmentItem : ContentDialog
    {
        public AddAssignmentItem(List<ContentItem> list, ObservableCollection<Assignment> a)
        {
            this.InitializeComponent();
            DataContext = new AssignmentItemViewModel(list, a);
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            (DataContext as AssignmentItemViewModel).AddItem();
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
