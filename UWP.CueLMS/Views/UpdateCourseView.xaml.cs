using UWP.CueLMS.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWP.CueLMS.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UpdateCourseView : Page
    {
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var viewmodel = (InstructorViewModel)e.Parameter;
            if (e.Parameter != null)
            {
                DataContext = viewmodel;
            }
        }
        public UpdateCourseView()
        {
            this.InitializeComponent();
            savecode.IsEnabled = false;
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
        private void SaveCode_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as InstructorViewModel).UpdateCourseCode();
            codebox.Text = string.Empty;
            savecode.IsEnabled = false;
        }
        private void SaveName_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as InstructorViewModel).UpdateCourseName();
            namebox.Text = string.Empty;
        }
        private void SaveDescription_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as InstructorViewModel).UpdateCourseDescription();
            descriptionbox.Text = string.Empty;
        }
        private void SaveCreditHours_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as InstructorViewModel).UpdateCourseCreditHours();
            chbox.Text = string.Empty;
        }
        private void SaveRoom_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as InstructorViewModel).UpdateCourseRoom();
            roombox.Text = string.Empty;
        }
        private void CheckCode_Click(object sender, RoutedEventArgs e)
        {
            var main = DataContext as InstructorViewModel;
            if (main.CheckCode() == true)
            {
                savecode.IsEnabled = true;
            }
            else
            {
                savecode.IsEnabled = false;
            }
        }
    }
}
