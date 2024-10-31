using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace QuizMaker_labb3.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string? propertyName = null) //CallerMemberName då sätter den objektet automatiskt
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); //This = sender 
        }
    }
}
