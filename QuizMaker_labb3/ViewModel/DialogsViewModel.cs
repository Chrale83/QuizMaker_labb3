using QuizMaker_labb3.Command;
using QuizMaker_labb3.Dialogs;

namespace QuizMaker_labb3.ViewModel
{
    public class DialogsViewModel : ViewModelBase
    {
        public DelegateCommand OpenNewPackDialog { get; }
        public DelegateCommand OpenEditDialogWindow { get; }
        public DelegateCommand ShowQuizResults { get; }

        public DialogsViewModel()
        {
            OpenNewPackDialog = new DelegateCommand(NewPackDialog);
            OpenEditDialogWindow = new DelegateCommand(EditPackDialog);
            ShowQuizResults = new DelegateCommand(QuizResultDialog);
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

        public void QuizResultDialog(object? obj)
        {
            var quizResultDialog = new QuizResultDialogWindow();
        }
        
    }
}
