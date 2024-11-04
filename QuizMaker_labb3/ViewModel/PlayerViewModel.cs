using QuizMaker_labb3.Command;
using QuizMaker_labb3.Model;
using System.DirectoryServices.ActiveDirectory;
using System.Windows.Threading;

namespace QuizMaker_labb3.ViewModel
{
    public class PlayerViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel? mainWindowViewModel;

        private DispatcherTimer timer;
        private QuestionPackViewModel _playingPack;
        private Question _playingQuestion;
        public Question PlayingQuestion
        {
            get => _playingQuestion; 
            set
            {
                _playingQuestion = value;
                RaisePropertyChanged(nameof(PlayingQuestion));
            }
        }

        public DelegateCommand UpdateButtonCommand { get; }

        public PlayerViewModel(MainWindowViewModel? mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
           
            //timer = new DispatcherTimer();
            //timer.Interval = TimeSpan.FromSeconds(1);
        }

        public void StartQuiz(QuestionPackViewModel activePack)
        {
            _playingPack = activePack;
            ShufflePackOrder(_playingPack);
            SetTimer(_playingPack);
            
        }

        private void SetTimer(QuestionPackViewModel _playingPack)
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer = _playingPack.TimeLimitInSeconds;
            
        }

        public void PlayQuiz(Question question)
        {
            
        }

        public QuestionPackViewModel ShufflePackOrder(QuestionPackViewModel playingPack)
        {
            ShuffleExtension.Shuffle(playingPack.Questions);
            return playingPack;
          
        }






    }
    
    
    
    
    public static class ShuffleExtension
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }





}