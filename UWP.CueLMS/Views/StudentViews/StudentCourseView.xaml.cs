﻿using Library.LMS.Models;
using Library.LMS.Services;
using System.Collections.Generic;
using UWP.CueLMS.ViewModels;
using UWP.CueLMS.ViewModels.StudentViewViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWP.CueLMS.Views.StudentViews
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StudentCourseView : Page
    {
        private StudentViewModel studentviewmodel { get; set; }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var viewModel = (StudentViewModel)e.Parameter;
            if (e.Parameter != null)
            {
                studentviewmodel = viewModel; //student view model
                var course = viewModel.SelectedCourse;
                var student = viewModel.Student;
                DataContext = new StudentCourseViewModel(course, student);
            }
        }
        public StudentCourseView()
        {
            this.InitializeComponent();
        }

        private void Exit_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Dictionary<CourseService, PersonService> services = new Dictionary<CourseService, PersonService>() //pass services through
            { {studentviewmodel.CourseService, studentviewmodel.PersonService } };
            Frame.Navigate(typeof(StudentView), services);
        }

        private void ViewAnnouncement_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var main = DataContext as StudentCourseViewModel;
            Frame.Navigate(typeof(StudentAnnouncementView), main);
        }

        private void ViewModule_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var main = DataContext as StudentCourseViewModel;
            var module = main.SelectedModule;
            Dictionary<StudentViewModel, Module> dictionary = new Dictionary<StudentViewModel, Module>
            { { studentviewmodel, module }};
            Frame.Navigate(typeof(StudentModuleView), dictionary); //passes in viewmodel and selected module
        }

        private void ViewAssignment_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var main = DataContext as StudentCourseViewModel;
            Frame.Navigate(typeof(StudentAssignmentView), main);
        }
    }
}
