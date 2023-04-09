using Library.LMS.Models;
using Library.LMS.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ServiceModel.Channels;
using UWP.CueLMS.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWP.CueLMS.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InstructorView : Page
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
                DataContext = new InstructorViewModel(courseservice, personservice);
            }
        }
        public InstructorView()
        {
            this.InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage), services);
        }

        private void AddPerson_Click(object sender, RoutedEventArgs e)
        {
            querybox.Text = ""; //clears query box
            (DataContext as InstructorViewModel).PersonDialog();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as InstructorViewModel).SearchPeople();
        }
        private void UpdatePerson_Click(object sender, RoutedEventArgs e)
        {
            var viewmodel = DataContext as InstructorViewModel;
            if (viewmodel.SelectedPerson is Student)
            {
                Frame.Navigate(typeof(UpdateStudentView), viewmodel);
            }
            else
            {
                Frame.Navigate(typeof(UpdateTAInstructorView), viewmodel);
            }
            
        }

        private void AddCourse_Click(object sender, RoutedEventArgs e)
        {
            searchbox.Text = string.Empty;
            (DataContext as InstructorViewModel).AddCourse();
        }

        private void SemesterSelect_Click(object sender, RoutedEventArgs e)
        {
            searchbox.Text = string.Empty;
            (DataContext as InstructorViewModel).SemesterDialog();
        }

        private void DeleteCourse_Click(object sender, RoutedEventArgs e)
        {
            searchbox.Text = string.Empty;
            (DataContext as InstructorViewModel).DeleteCourse();
        }

        private void ManageCourse_Click(object sender, RoutedEventArgs e)
        {
            var viewmodel = DataContext as InstructorViewModel;
            Frame.Navigate(typeof(ManageCourseView), viewmodel);
        }

        private void UpdateCourse_Click(object sender, RoutedEventArgs e)
        {
            var viewmodel = DataContext as InstructorViewModel;
            Frame.Navigate(typeof(UpdateCourseView), viewmodel);
        }

        private void SearchCourses_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as InstructorViewModel).SearchCourses();
        }
    }
}
