using QuizMaker_labb3.Model;
using System.Collections.ObjectModel;

namespace QuizMaker_labb3.ViewModel
{
    public class QuestionPackViewModel : ViewModelBase
    {
        private readonly QuestionPack _model;

        public QuestionPackViewModel(QuestionPack model)
        {
            _model = model;
            Questions = new ObservableCollection<Question>(model.Questions);
        }
        public ObservableCollection<Question> Questions { get; }
        public Difficulty Difficulty
        {
            get => _model.Difficulty;
            set
            {
                _model.Difficulty = value;
                RaisePropertyChanged();
            }
        }
        public int TimeLimitInSeconds
        {
            get => _model.TimeLimitInSeconds;
            set
            {
                _model.TimeLimitInSeconds = value;
                RaisePropertyChanged("TimeInSeconds");
            }
        }
    }
}
            
        
        
         

