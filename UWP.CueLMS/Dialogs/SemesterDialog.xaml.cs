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

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWP.CueLMS.Dialogs
{
    public sealed partial class SemesterDialog : ContentDialog
    {
        public SemesterDialog(InstructorViewModel viewmodel)
        {
            this.InitializeComponent();
            DataContext = viewmodel;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if(spring.IsChecked == true)
            {
                (DataContext as InstructorViewModel).SemesterSelect(1);
            }
            else if (summer.IsChecked == true)
            {
                (DataContext as InstructorViewModel).SemesterSelect(2);
            }
            else if (fall.IsChecked == true)
            {
                (DataContext as InstructorViewModel).SemesterSelect(3);
            }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
