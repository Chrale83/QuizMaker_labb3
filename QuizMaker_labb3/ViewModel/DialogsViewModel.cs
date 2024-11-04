using QuizMaker_labb3.Command;
using QuizMaker_labb3.Dialogs;

namespace QuizMaker_labb3.ViewModel
{
    public class DialogsViewModel : ViewModelBase
    {
        public DelegateCommand OpenNewPackDialog { get; }
        public DelegateCommand OpenEditDialogWindow { get; }

        public DialogsViewModel()
        {
            OpenNewPackDialog = new DelegateCommand(NewPackDialog);
            OpenEditDialogWindow = new DelegateCommand(EditPackDialog);
        }

        private void EditPackDialog(object obj)
        {
            var packOptionDialog = new PackOptionsDialog();
            packOptionDialog.ShowDialog();
            
        }

        public void NewPackDialog(object? obj)
        {
            var createNewPackDialog = new CreateNewPackDialog();
            createNewPackDialog.ShowDialog();
        }
        
    }
}
