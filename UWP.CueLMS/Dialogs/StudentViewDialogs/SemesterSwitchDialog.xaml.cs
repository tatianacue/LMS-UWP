using UWP.CueLMS.ViewModels;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238
/* Tatiana Graciela Cue COP4870-0001*/
namespace UWP.CueLMS.Dialogs.StudentViewDialogs
{
    public sealed partial class SemesterSwitchDialog : ContentDialog
    {
        public SemesterSwitchDialog(StudentViewModel vm)
        {
            this.InitializeComponent();
            DataContext = vm;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (spring.IsChecked == true) //switch to spring
            {
                (DataContext as StudentViewModel).SwitchSemesters(1);
            }
            else if (summer.IsChecked == true) //switch to summer
            {
                (DataContext as StudentViewModel).SwitchSemesters(2);
            }
            else if (fall.IsChecked == true) //switch to fall
            {
                (DataContext as StudentViewModel).SwitchSemesters(3);
            }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
