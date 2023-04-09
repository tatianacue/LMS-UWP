using Library.LMS.Services;
using UWP.CueLMS.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWP.CueLMS.Dialogs
{
    public sealed partial class StudentDialog : ContentDialog
    {
        public StudentDialog(PersonService service)
        {
            this.InitializeComponent();
            DataContext = new PersonViewModel(service);
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var main = DataContext as PersonViewModel;
            if (fresh.IsChecked == true) //freshman
            {
                main.Classification("f");
            }
            else if (soph.IsChecked == true) //sophomore
            {
                main.Classification("s");
            }
            else if (junior.IsChecked == true) //junior
            {
                main.Classification("j");
            }
            else if (senior.IsChecked == true) //senior
            {
                main.Classification("e");
            }
            (DataContext as PersonViewModel).AddStudent();
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
