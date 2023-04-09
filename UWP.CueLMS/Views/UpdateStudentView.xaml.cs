using Library.LMS.Models;
using Library.LMS.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Xml.Linq;
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
    public sealed partial class UpdateStudentView : Page
    {
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var viewModel = (InstructorViewModel)e.Parameter;
            DataContext = viewModel;
            updateid.IsEnabled = false;
        }
        public UpdateStudentView()
        {
            this.InitializeComponent();
        }
        private void UpdateStudentId_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as InstructorViewModel).UpdateId();
            idbox.Text = string.Empty;
            updateid.IsEnabled = false;
        }

        private void UpdateStudentName_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as InstructorViewModel).UpdateName();
            namebox.Text = string.Empty;
        }

        private void UpdateClassification_Click(object sender, RoutedEventArgs e)
        {
            var main = DataContext as InstructorViewModel;
            if (fresh.IsChecked == true) //freshman
            {
                main.Classification("f");
            }
            else if (soph.IsChecked == true) //sophomore
            {
                main.Classification("s");
            }
            else if (junior.IsChecked == true) //junior
            {
                main.Classification("j");
            }
            else if (senior.IsChecked == true) //senior
            {
                main.Classification("e");
            }
            (DataContext as InstructorViewModel).UpdateClassification();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void CheckId_Click(object sender, RoutedEventArgs e)
        {
            var main = DataContext as InstructorViewModel;
            if (main.CheckId() == true)
            {
                updateid.IsEnabled = true;
            }
            else
            {
                updateid.IsEnabled = false;
            }
        }
    }
}
