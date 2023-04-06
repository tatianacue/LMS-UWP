﻿using Library.LMS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
            Frame.Navigate(typeof(InstructorView), instructorviewmodel);
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
    }
}
