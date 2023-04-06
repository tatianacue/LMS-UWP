using Library.LMS.Models;
using System.Collections.Generic;
using UWP.CueLMS.ViewModels;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWP.CueLMS.Dialogs
{
    public sealed partial class AssignmentDialog : ContentDialog
    {
        public AssignmentDialog(List<Assignment> list)
        {
            this.InitializeComponent();
            DataContext = new AssignmentViewModel(list);
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            (DataContext as AssignmentViewModel).AddAssignment();
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
