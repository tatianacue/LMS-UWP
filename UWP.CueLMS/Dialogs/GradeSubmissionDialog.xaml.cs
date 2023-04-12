using UWP.CueLMS.ViewModels;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238
/* Tatiana Graciela Cue COP4870-0001*/
namespace UWP.CueLMS.Dialogs
{
    public sealed partial class GradeSubmissionDialog : ContentDialog
    {
        public GradeSubmissionDialog(CourseManagerViewModel vm)
        {
            this.InitializeComponent();
            DataContext = vm;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            (DataContext as CourseManagerViewModel).GradeSubmission();
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
