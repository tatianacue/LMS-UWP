using Library.LMS.Models;
using Library.LMS.Services;
using System.Collections.Generic;
using UWP.CueLMS.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238
/* Tatiana Graciela Cue COP4870-0001*/
namespace UWP.CueLMS.Dialogs
{
    public sealed partial class CourseDialog : ContentDialog
    {
        public CourseDialog(List<Course> courses,CourseService service)
        {
            this.InitializeComponent();
            DataContext = new CourseViewModel(courses,service);
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            (DataContext as CourseViewModel).AddCourse();
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
        private void CheckCode_Click(object sender, RoutedEventArgs e)
        {
            var main = DataContext as CourseViewModel;
            if (main.CheckCode() == true)
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
