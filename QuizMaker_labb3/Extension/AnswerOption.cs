using QuizMaker_labb3.Extension;
using QuizMaker_labb3.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace QuizMaker_labb3.Extension
{
    public class AnswerOption : ViewModelBase
    {
        public AnswerOption(string optionId)
        {
            OptionId = optionId;
        }


        private string optionId;

        public event PropertyChangedEventHandler? PropertyChanged;

        private string _answer = string.Empty;
        public string Answer
        {
            get => _answer;
            set
            {
                _answer = value;
                RaisePropertyChanged(nameof(Answer));
            }
        }

        private SolidColorBrush _backGroundColor = new SolidColorBrush(Colors.LightGray);
        public SolidColorBrush BackGroundColor
        {
            get => _backGroundColor;
            set
            {
                _backGroundColor = value;
                RaisePropertyChanged(nameof(BackGroundColor));
            }
        }

        public string OptionId { get => optionId;
            set
            {
                optionId = value;
                RaisePropertyChanged(nameof(OptionId));
            }
        }

        
    }
}
