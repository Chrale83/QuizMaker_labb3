using System.Windows.Controls;

namespace QuizMaker_labb3.View
{
    /// <summary>
    /// Interaction logic for PlayerView.xaml
    /// </summary>
    public partial class PlayerView : UserControl
    {
        public PlayerView()
        {
            InitializeComponent();
            DataContext = App.Current.MainWindow.DataContext;
        }
    }
}
