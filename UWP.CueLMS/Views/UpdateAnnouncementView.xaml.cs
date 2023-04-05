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
    public sealed partial class UpdateAnnouncementView : Page
    {
        private InstructorViewModel instructorviewmodel { get; set; }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var dictionary = (Dictionary<InstructorViewModel, CourseManagerViewModel>)e.Parameter;
            if (e.Parameter != null)
            {
                var coursemvm = dictionary.Values.First();
                instructorviewmodel = dictionary.Keys.First(); //saves instructorviewmodel
                DataContext = coursemvm;
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
            Frame.Navigate(typeof(ManageCourseView), instructorviewmodel);
        }
    }
}
