using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QuizMaker_labb3.Dialogs
{
    /// <summary>
    /// Interaction logic for QuizDone.xaml
    /// </summary>
    public partial class QuizResultDialogWindow : Window
    {
        public QuizResultDialogWindow()
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
