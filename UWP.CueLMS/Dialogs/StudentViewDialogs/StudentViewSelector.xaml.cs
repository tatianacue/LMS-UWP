using UWP.CueLMS.ViewModels;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWP.CueLMS.Dialogs.StudentViewDialogs
{
    public sealed partial class StudentViewSelector : ContentDialog
    {
        public StudentViewSelector(StudentViewModel vm)
        {
            this.InitializeComponent();
            DataContext = vm;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            (DataContext as StudentViewModel).ChangeStudent();
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
