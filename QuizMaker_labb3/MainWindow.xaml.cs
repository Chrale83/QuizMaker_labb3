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

            

            
        }
    }
}