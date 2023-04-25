using Library.LMS.Models;
using System.Collections.Generic;
using UWP.CueLMS.ViewModels;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238
/* Tatiana Graciela Cue COP4870-0001*/
namespace UWP.CueLMS.Dialogs
{
    public sealed partial class AnnouncementDialog : ContentDialog
    {
        public AnnouncementDialog(Course course)
        {
            this.InitializeComponent();
            DataContext = new AnnouncementViewModel(course);
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            (DataContext as AnnouncementViewModel).AddAnnouncement();
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
