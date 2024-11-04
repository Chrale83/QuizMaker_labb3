using QuizMaker_labb3.Command;
using QuizMaker_labb3.Model;
using System.DirectoryServices.ActiveDirectory;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace QuizMaker_labb3.ViewModel
{
    public class PlayerViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel? mainWindowViewModel;
        DispatcherTimer dispatcherTimer;
        public TimeSpan _timeLeft;
        private QuestionPackViewModel _playingPack;
        private Question _playingQuestion;
        private string _timeLeftTxt;
        private int _counterQuestionIndex = 0;


        public Question PlayingQuestion
        {
            get => _playingQuestion;
            set
            {
                _playingQuestion = value;
                RaisePropertyChanged(nameof(PlayingQuestion));
            }
        }

        public string TimeLeftText { get => _timeLeftTxt;

            set
            {
                _timeLeftTxt = value;
                RaisePropertyChanged(nameof(TimeLeftText));
            }
        }

        public DelegateCommand UpdateButtonCommand { get; }

        public PlayerViewModel(MainWindowViewModel? mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;

            
        }

       

        public void StartQuiz(QuestionPackViewModel activePack)
        {
            _playingPack = activePack;
            ShufflePackOrder(_playingPack);
            SetTimer(_playingPack);
            PlayQuiz(_playingPack.Questions[_counterQuestionIndex]);
            
        }

        public void PlayQuiz(Question question)
        {
            PlayingQuestion = question;
        }

        private void SetTimer(QuestionPackViewModel _playingPack)
        {
            int tempTimeInSec = _playingPack.TimeLimitInSeconds;
            _timeLeft = TimeSpan.FromSeconds(tempTimeInSec);
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Start();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (_timeLeft == TimeSpan.Zero) dispatcherTimer.Stop();
            
            else
            {
                _timeLeft = _timeLeft.Add(TimeSpan.FromSeconds(-1));
                TimeLeftText = _timeLeft.ToString("c");
                
            }
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