using Library.LMS.Models;
using Library.LMS.Models.Grading;
using System.Collections.Generic;
using UWP.CueLMS.ViewModels;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWP.CueLMS.Dialogs.AssignmentGroupDialogs
{
    public sealed partial class AssignmentGroupDialog : ContentDialog
    {
        public AssignmentGroupDialog(List<AssignmentGroup> list, Assignment a)
        {
            this.InitializeComponent();
            DataContext = new AssignmentGroupViewModel(list, a);
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
