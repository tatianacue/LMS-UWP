using UWP.CueLMS.ViewModels.ModuleStuff;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238
/* Tatiana Graciela Cue COP4870-0001*/
namespace UWP.CueLMS.Views.ContentItemUpdates
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UpdateFileItemView : Page
    {
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var viewmodel = (ModuleManagerViewModel)e.Parameter;
            if (e.Parameter != null)
            {
                DataContext = viewmodel;
            }
        }
        public UpdateFileItemView()
        {
            this.InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void SaveName_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as ModuleManagerViewModel).UpdateName();
            namebox.Text = string.Empty;
        }

        private void SaveDescription_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as ModuleManagerViewModel).UpdateDescription();
            descriptionbox.Text = string.Empty;
        }
        private void SavePath_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as ModuleManagerViewModel).UpdateFilePath();
            filebox.Text = string.Empty;
        }
    }
}
