using QuizMaker_labb3.Extension;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace QuizMaker_labb3.Extension
{
    public class AnswerButton : INotifyPropertyChanged
    {
        

        public event PropertyChangedEventHandler? PropertyChanged;

        private string _answer = string.Empty;
        public string Answer
        {
            get => _answer;
            set
            {
                _answer = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Answer)));
            }
        }

        private SolidColorBrush _backGroundColor = new SolidColorBrush(Colors.Red);
        public SolidColorBrush BackGroundColor
        {
            get => _backGroundColor;
            set
            {
                _backGroundColor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BackGroundColor)));
            }
        }

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
