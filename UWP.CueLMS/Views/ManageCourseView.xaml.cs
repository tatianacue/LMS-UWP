using Library.LMS.Services;
using System.Collections.Generic;
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
    public sealed partial class ManageCourseView : Page
    {
        private InstructorViewModel instructorviewmodel {  get; set; }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var viewModel = (InstructorViewModel)e.Parameter;
            if (e.Parameter != null)
            {
                instructorviewmodel = viewModel; //instructor view model
                var list = instructorviewmodel.People; //all people
                var course = viewModel.SelectedCourse;
                DataContext = new CourseManagerViewModel(course, list);
            }
        }
        public ManageCourseView()
        {
            this.InitializeComponent();
        }

        private void AddAnnouncement_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as CourseManagerViewModel).AnnouncementDialog();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<CourseService, PersonService> services = new Dictionary<CourseService, PersonService>() //pass services through
            { {instructorviewmodel.courseService, instructorviewmodel.personService } };
            Frame.Navigate(typeof(InstructorView), services);
        }

        private void DeleteAnnouncement_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as CourseManagerViewModel).DeleteAnnouncement();
        }

        private void UpdateAnnouncement_Click(object sender, RoutedEventArgs e)
        {
            var main = DataContext as CourseManagerViewModel;
            Dictionary<InstructorViewModel, CourseManagerViewModel> dictionary = new Dictionary<InstructorViewModel, CourseManagerViewModel>
            { { instructorviewmodel, main }}; //pass both viewmodels through
            Frame.Navigate(typeof(UpdateAnnouncementView), dictionary);
        }

        private void AddModule_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as CourseManagerViewModel).ModuleDialog();
        }

        private void DeleteModule_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as CourseManagerViewModel).DeleteModule();
        }

        private void AddStudent_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as CourseManagerViewModel).StudentToCourseDialog();
        }

        private void DeleteStudent_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as CourseManagerViewModel).RemoveFromRoster();
        }

        private void ManageModule_Click(object sender, RoutedEventArgs e)
        {
            var main = DataContext as CourseManagerViewModel;
            Dictionary<InstructorViewModel, CourseManagerViewModel> dictionary = new Dictionary<InstructorViewModel, CourseManagerViewModel>
            { { instructorviewmodel, main }}; //pass both viewmodels through
            Frame.Navigate(typeof(ManageModuleView), dictionary);
        }

        private void AddAssignment_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as CourseManagerViewModel).AssignmentDialog();
        }

        private void DeleteAssignment_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as CourseManagerViewModel).DeleteAssignment();
        }

        private void AssignmentGroup_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as CourseManagerViewModel).AddToGroupDialog();
        }
    }
}
