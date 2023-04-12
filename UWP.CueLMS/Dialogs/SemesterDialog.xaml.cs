using UWP.CueLMS.ViewModels;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238
/* Tatiana Graciela Cue COP4870-0001*/
namespace UWP.CueLMS.Dialogs
{
    public sealed partial class SemesterDialog : ContentDialog
    {
        public SemesterDialog(InstructorViewModel viewmodel)
        {
            this.InitializeComponent();
            DataContext = viewmodel;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if(spring.IsChecked == true)
            {
                (DataContext as InstructorViewModel).SemesterSelect(1);
            }
            else if (summer.IsChecked == true)
            {
                (DataContext as InstructorViewModel).SemesterSelect(2);
            }
            else if (fall.IsChecked == true)
            {
                (DataContext as InstructorViewModel).SemesterSelect(3);
            }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
