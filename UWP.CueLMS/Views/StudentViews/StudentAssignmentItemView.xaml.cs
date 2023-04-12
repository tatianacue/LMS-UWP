using UWP.CueLMS.ViewModels.StudentViewViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWP.CueLMS.Views.StudentViews
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StudentAssignmentItemView : Page
    {
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter != null)
            {
                var viewmodel = (StudentModuleViewModel)e.Parameter;
                DataContext = viewmodel;
            }
        }
        public StudentAssignmentItemView()
        {
            this.InitializeComponent();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
