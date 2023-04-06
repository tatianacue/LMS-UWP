using System.Collections.Generic;
using System.Linq;
using UWP.CueLMS.ViewModels;
using UWP.CueLMS.ViewModels.ModuleStuff;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWP.CueLMS.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ManageModuleView : Page
    {
        private InstructorViewModel instructorviewmodel { get; set; }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var dictionary = (Dictionary<InstructorViewModel, CourseManagerViewModel>)e.Parameter;
            if (e.Parameter != null)
            {
                var coursemvm = dictionary.Values.First(); //saves coursemanagerviewmodel
                instructorviewmodel = dictionary.Keys.First(); //saves instructorviewmodel
                DataContext = new ModuleManagerViewModel(coursemvm.SelectedModule);
            }
        }
        public ManageModuleView()
        {
            this.InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ManageCourseView), instructorviewmodel);
        }

        private void AddPageItem_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as ModuleManagerViewModel).AddPageItem();
        }
    }
}
