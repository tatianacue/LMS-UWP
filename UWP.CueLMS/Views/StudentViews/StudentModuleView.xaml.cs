using System.Collections.Generic;
using System.Linq;
using UWP.CueLMS.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Library.LMS.Models;
using UWP.CueLMS.ViewModels.StudentViewViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWP.CueLMS.Views.StudentViews
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StudentModuleView : Page
    {
        private StudentViewModel studentviewmodel { get; set; }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter != null)
            {
                var dictionary = (Dictionary<StudentViewModel, Module>)e.Parameter;
                studentviewmodel = dictionary.Keys.First(); //saves viewmodel
                var module = dictionary.Values.First();
                DataContext = new StudentModuleViewModel(module);
            }
        }
        public StudentModuleView()
        {
            this.InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(StudentCourseView), studentviewmodel);
        }

        private void SelectItem_Click(object sender, RoutedEventArgs e)
        {
            var viewmodel = DataContext as StudentModuleViewModel;
            if (viewmodel.SelectedItem is FileItem)
            {
                Frame.Navigate(typeof(StudentFileItemView), viewmodel);
            }
            else if (viewmodel.SelectedItem is PageItem)
            {
                Frame.Navigate(typeof(StudentPageItemView), viewmodel);
            }
            else if (viewmodel.SelectedItem is AssignmentItem)
            {
                Frame.Navigate(typeof(StudentAssignmentItemView), viewmodel);
            }
        }
    }
}
