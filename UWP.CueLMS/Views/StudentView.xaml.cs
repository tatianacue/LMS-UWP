using Library.LMS.Services;
using System.Collections.Generic;
using System.Linq;
using UWP.CueLMS.ViewModels;
using UWP.CueLMS.Views.StudentViews;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWP.CueLMS.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StudentView : Page
    {
        private Dictionary<CourseService, PersonService> services { get; set; }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter != null)
            {
                services = (Dictionary<CourseService, PersonService>)e.Parameter;
                var courseservice = services.Keys.First();
                var personservice = services.Values.First();
                DataContext = new StudentViewModel(courseservice, personservice, personservice.SelectedStudent);
            }
        }
        public StudentView()
        {
            this.InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage), services);
        }

        private void EnterCourse_Click(object sender, RoutedEventArgs e)
        {
            var main = DataContext as StudentViewModel;
            Frame.Navigate(typeof(StudentCourseView), main); //takes you to course view for students
        }

        private void SwitchStudent_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as StudentViewModel).SwitchStudent();
        }

        private void ViewCourseGrade_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as StudentViewModel).GetCourseGradeDialog();
        }

        private void CalculateGPA_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as StudentViewModel).GetCurrentGPADialog();
        }

        private void ViewPastSemesters_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as StudentViewModel).ViewPastSemesters();
        }
    }
}
