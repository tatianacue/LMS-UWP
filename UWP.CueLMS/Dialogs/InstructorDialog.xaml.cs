using Library.LMS.Services;
using UWP.CueLMS.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238
/* Tatiana Graciela Cue COP4870-0001*/
namespace UWP.CueLMS.Dialogs
{
    public sealed partial class InstructorDialog : ContentDialog
    {
        public InstructorDialog(PersonService service)
        {
            this.InitializeComponent();
            DataContext = new PersonViewModel(service);
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            (DataContext as PersonViewModel).AddInstructor();
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void CheckId_Click(object sender, RoutedEventArgs e)
        {
            var main = DataContext as PersonViewModel;
            if (main.CheckId() == true)
            {
                IsPrimaryButtonEnabled = true;
            }
            else
            {
                IsPrimaryButtonEnabled = false;
            }
        }
    }
}
