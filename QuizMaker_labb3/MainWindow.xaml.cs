using QuizMaker_labb3.Command;
using QuizMaker_labb3.ViewModel;
using System.Text;
using System.Windows;


namespace QuizMaker_labb3
{

    public partial class MainWindow : Window
    {

        

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(); //DataContex får hela det fönstret och dess properties
            FullScreenCommand = new DelegateCommand(FullScreenSwap);
        }
        public DelegateCommand FullScreenCommand { get; }
        private void FullScreenSwap(object obj)
        {
            WindowState = WindowState.Maximized;
        }


    }
}