using System.Windows.Input;

namespace QuizMaker_labb3.Command
{
    public class DelegateCommand : ICommand
    {
        private readonly Action<object> _exectue;
        private readonly Func<object?, bool>? _canExectue;

        public event EventHandler? CanExecuteChanged; //Fungerar som propertyChanged

        public DelegateCommand(Action<object> exectue, Func<object?, bool>? canExectue = null) //Den kan vara en referns till en metod som tar ett object in
        {
            ArgumentNullException.ThrowIfNull(exectue);
            _exectue = exectue;
            _canExectue = canExectue;
        }

        public void RaiseCanExectueChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        public bool CanExecute(object? parameter) //Den kör denna först(om jag använder den), sen Execute() Returnerar en bool
        {
            return _canExectue is null? true : _canExectue(parameter); //Tenerary operator
        }

        public void Execute(object? parameter) //Den koden som körs när man trycker på knappen, men bara om CanExecute() är true 
        {
            _exectue(parameter);
        }

        //public bool CanExecute(object? parameter) => canExectue is null ? true : canExectue(parameter);

        //public void Execute(object? parameter) => exectue(parameter);

    }
}
