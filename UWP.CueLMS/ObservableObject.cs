using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
/* Tatiana Graciela Cue COP4870-0001*/
namespace UWP.CueLMS
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}
