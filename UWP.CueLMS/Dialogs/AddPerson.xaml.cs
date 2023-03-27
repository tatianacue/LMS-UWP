using Library.LMS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class AddPerson : ContentDialog
    {
        public AddPerson(InstructorViewModel viewmodel)
        {
            this.InitializeComponent();
            DataContext = viewmodel;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            this.Hide(); //hides first dialog box
            (DataContext as InstructorViewModel).AddTeachingAssistant();
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            this.Hide();
            (DataContext as InstructorViewModel).AddInstructor();
        }
        private void Student_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            (DataContext as InstructorViewModel).AddStudent();
        }
    }
}
