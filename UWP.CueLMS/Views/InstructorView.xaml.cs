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
        public InstructorView()
        {
            this.InitializeComponent();
            DataContext = new InstructorViewModel();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void AddPerson_Click(object sender, RoutedEventArgs e)
        {
            querybox.Text = ""; //clears query box
            var viewmodel = DataContext as InstructorViewModel;
            (DataContext as InstructorViewModel).PersonDialog(viewmodel);
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as InstructorViewModel).SearchPeople();
        }
    }
}
