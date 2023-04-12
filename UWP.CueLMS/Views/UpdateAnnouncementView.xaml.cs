using UWP.CueLMS.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238
/* Tatiana Graciela Cue COP4870-0001*/
namespace UWP.CueLMS.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UpdateAnnouncementView : Page
    {
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            
            if (e.Parameter != null)
            {
                var viewmodel = (CourseManagerViewModel)e.Parameter;
                DataContext = viewmodel;
            }
        }
        public UpdateAnnouncementView()
        {
            this.InitializeComponent();
        }

        private void EditTitle_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as CourseManagerViewModel).UpdateATitle();
            titlebox.Text = string.Empty;
        }

        private void EditBody_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as CourseManagerViewModel).UpdateAText();
            bodybox.Text = string.Empty;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
