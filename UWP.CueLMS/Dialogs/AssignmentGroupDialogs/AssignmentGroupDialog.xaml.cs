using Library.LMS.Models;
using Library.LMS.Models.Grading;
using System.Collections.Generic;
using UWP.CueLMS.ViewModels;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238
/* Tatiana Graciela Cue COP4870-0001*/
namespace UWP.CueLMS.Dialogs.AssignmentGroupDialogs
{
    public sealed partial class AssignmentGroupDialog : ContentDialog
    {
        public AssignmentGroupDialog(Course course)
        {
            this.InitializeComponent();
            DataContext = new AssignmentGroupViewModel(course);
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            (DataContext as AssignmentGroupViewModel).Add();
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
