﻿using UWP.CueLMS.ViewModels;
using UWP.CueLMS.Views;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWP.CueLMS
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void Search_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            (DataContext as MainViewModel).SearchPeople();
        }

        private void AddPerson_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            querybox.Text=""; //clears query box
            var main = DataContext as MainViewModel;
            //(DataContext as MainViewModel).Adder(main);
        }

        private void InstructorView_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(InstructorView));
        }
    }
}
