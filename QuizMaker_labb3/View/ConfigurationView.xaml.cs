using System.Windows.Controls;

namespace QuizMaker_labb3.View
{
    
    public partial class ConfigurationView : UserControl
    {
        public ConfigurationView()
        {
            InitializeComponent();
            DataContext = App.Current.MainWindow.DataContext;

        }
    }
}
