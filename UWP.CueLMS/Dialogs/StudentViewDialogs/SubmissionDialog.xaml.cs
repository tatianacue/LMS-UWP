using UWP.CueLMS.ViewModels.StudentViewViewModels;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238
/* Tatiana Graciela Cue COP4870-0001*/
namespace UWP.CueLMS.Dialogs.StudentViewDialogs
{
    public sealed partial class SubmissionDialog : ContentDialog
    {
        public SubmissionDialog(StudentCourseViewModel vm)
        {
            this.InitializeComponent();
            DataContext = vm;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            (DataContext as StudentCourseViewModel).AddSubmission();
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
