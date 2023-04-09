using Library.LMS.Services;
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
    public sealed partial class UpdateTAInstructorView : Page
    {
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var viewModel = (InstructorViewModel)e.Parameter;
            DataContext = viewModel;
            updateid.IsEnabled = false;
        }
        public UpdateTAInstructorView()
        {
            this.InitializeComponent();
        }
        private void UpdateId_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as InstructorViewModel).UpdateId();
            idbox.Text = string.Empty;
            updateid.IsEnabled = false;
        }

        private void UpdateName_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as InstructorViewModel).UpdateName();
            namebox.Text = string.Empty;
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
