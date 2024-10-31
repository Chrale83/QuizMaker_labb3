using QuizMaker_labb3.Command;
using QuizMaker_labb3.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuizMaker_labb3.ViewModel
{
    public class DialogsViewModel : ViewModelBase
    {
        public DelegateCommand OpenNewPackDialog { get; }
        public DelegateCommand PackOptionsDialog { get; }

        public DialogsViewModel()
        {
            OpenNewPackDialog = new DelegateCommand(NewPackDialog);
        }
        public void NewPackDialog(object? obj)
        {
            var createNewPackDialog = new CreateNewPackDialog();
            createNewPackDialog.ShowDialog();
        }
        
    }
}
