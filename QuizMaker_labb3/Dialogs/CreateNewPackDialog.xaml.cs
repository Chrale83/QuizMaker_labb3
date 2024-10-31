using System.Windows;

namespace QuizMaker_labb3.Dialogs
{
    /// <summary>
    /// Interaction logic for CreateNewPackDialog.xaml
    /// </summary>
    public partial class CreateNewPackDialog : Window
    {
        public CreateNewPackDialog()
        {
            InitializeComponent();
            DataContext = App.Current.MainWindow.DataContext;
            
        }
        public void CloseDialog()
        {
            this.Close();
        }
    }
}

