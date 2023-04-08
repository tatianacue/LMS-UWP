using Library.LMS.Services;
using System.Collections.Generic;
using System.Linq;
using UWP.CueLMS.ViewModels;
using UWP.CueLMS.Views;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWP.CueLMS
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter != null)
            {
                var main = DataContext as MainViewModel;
                if (e.Parameter is Dictionary<CourseService,PersonService>) {
                    var updatedservices = (Dictionary<CourseService, PersonService>)e.Parameter;
                    main.Services = updatedservices;
                    main.courseService = updatedservices.Keys.First();
                    main.personService = updatedservices.Values.First();
                    main.AutoRefresh(); //updates list for student selector if modified
                }
            }
        }
        public MainPage()
        {
            this.InitializeComponent();
            DataContext = new MainViewModel();
            studentview.IsEnabled = false;
        }
        private void InstructorView_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var main = DataContext as MainViewModel;
            Frame.Navigate(typeof(InstructorView), main.Services);
        }

        private void SelectStudent_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            (DataContext as MainViewModel).SelectStudentDialog();
            var main = DataContext as MainViewModel;
            studentview.IsEnabled = true;
        }
        private void StudentView_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var main = DataContext as MainViewModel;
            Frame.Navigate(typeof(StudentView), main.Services);
        }
    }
}
