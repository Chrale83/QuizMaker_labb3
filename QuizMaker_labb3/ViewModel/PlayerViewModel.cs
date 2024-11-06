using QuizMaker_labb3.Command;
using QuizMaker_labb3.Extension;
using QuizMaker_labb3.Model;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Drawing;
using System.Reflection.Metadata;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Threading;

namespace QuizMaker_labb3.ViewModel
{
    public class PlayerViewModel : ViewModelBase
    {
        //<-----------------Föra att byta tillbaka---------------------->
        //mainWindowViewModel.SelectedViewModel = mainWindowViewModel.ConfigurationViewModel; 

        private readonly MainWindowViewModel? mainWindowViewModel;
        private DispatcherTimer dispatcherTimer;
        private TimeSpan _timeLeft;
        private QuestionPackViewModel _playingPack;
        private List<string> _playingQuestion = new();
        private List<Question> _activePlayingPack;
        private int _currentQuestionIndex;
        private int _timerForQuestionPack;
        private int _currentQuestionView;
        private int _maxQuestions;
        private string _currentQuery;
        private string _currentCorrectAnswer;
        public AnswerButton _answerOption1;
        public AnswerButton _answerOption2;
        public AnswerButton _answerOption3;
        public AnswerButton _answerOption4;
        public AnswerButton AnswerOption1
        {
            get => _answerOption1;
            set
            {
                _answerOption1 = value;
                RaisePropertyChanged(nameof(AnswerOption1));
            }
        }
        public AnswerButton AnswerOption2
        {
            get => _answerOption2;
            set
            {
                _answerOption2 = value;
                RaisePropertyChanged(nameof(AnswerOption2));
            }
        }
        public AnswerButton AnswerOption3
        {
            get => _answerOption3;
            set
            {
                _answerOption3 = value;
                RaisePropertyChanged(nameof(AnswerOption3));
            }
        }
        public AnswerButton AnswerOption4
        {
            get => _answerOption4;
            set
            {
                _answerOption4 = value;
                RaisePropertyChanged(nameof(AnswerOption4));
            }
        }
        public PlayerViewModel(MainWindowViewModel? mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
            dispatcherTimer = new DispatcherTimer();
            CheckAnswerCommand = new DelegateCommand(AnswerButton);
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            _answerOption1 = new AnswerButton();
            _answerOption2 = new AnswerButton();
            _answerOption3 = new AnswerButton();
            _answerOption4 = new AnswerButton();
            

        }

        private async void AnswerButton(object returnValues)
        {
            await CheckAnswerAndSet(returnValues as string[]);
            
            dispatcherTimer.Stop();

            CurrentQuestionView++;
            _currentQuestionIndex++;
            if (_currentQuestionIndex < _maxQuestions)
            {
                PlayQuiz(_activePlayingPack[_currentQuestionIndex]);
            }
            else
            {

                mainWindowViewModel.SelectedViewModel = mainWindowViewModel.ConfigurationViewModel;
            }
        }

        private async Task CheckAnswerAndSet(string[] values)
        {
            string answer = values[0];
            string whatButton = values[1];

            if (answer == _currentCorrectAnswer)
            {
                switch (whatButton)
                {
                    case "Button1":
                        AnswerOption1.BackGroundColor = SetToGreen();
                        await Task.Delay(2200);
                        AnswerOption1.BackGroundColor = SetToDefault();
                        break;
                    case "Button2":
                        AnswerOption2.BackGroundColor = SetToGreen();
                        await Task.Delay(2200);
                        AnswerOption2.BackGroundColor = SetToDefault();
                        break;
                    case "Button3":
                        AnswerOption3.BackGroundColor = SetToGreen();
                        await Task.Delay(2200);
                        AnswerOption3.BackGroundColor = SetToDefault();
                        break;
                    case "Button4":
                        AnswerOption4.BackGroundColor = SetToGreen();
                        await Task.Delay(2200);
                        AnswerOption4.BackGroundColor = SetToDefault();
                        break;
                }
            }
            else
            {
                switch (whatButton)
                {
                    case "Button1":
                        AnswerOption1.BackGroundColor = SetToRed();
                        await Task.Delay(2200);
                        AnswerOption1.BackGroundColor = SetToDefault();
                        break;
                    case "Button2":
                        AnswerOption2.BackGroundColor = SetToRed();
                        await Task.Delay(2200);
                        AnswerOption2.BackGroundColor = SetToDefault();
                        break;
                    case "Button3":
                        AnswerOption3.BackGroundColor = SetToRed();
                        await Task.Delay(2200);
                        AnswerOption3.BackGroundColor = SetToDefault();
                        break;
                    case "Button4":
                        AnswerOption4.BackGroundColor = SetToRed();
                        await Task.Delay(2200);
                        AnswerOption4.BackGroundColor = SetToDefault();
                        break;
                }

            }
        }

        public SolidColorBrush SetToGreen() => new SolidColorBrush(Colors.Green);
        public SolidColorBrush SetToRed() => new SolidColorBrush(Colors.Red);
        public SolidColorBrush SetToDefault() => new SolidColorBrush(Colors.LightGray);

        public DelegateCommand CheckAnswerCommand { get; }

        public int CurrentQuestionView
        {
            get => _currentQuestionView;
            set
            {
                _currentQuestionView = value;
                RaisePropertyChanged(nameof(CurrentQuestionView));
            }
        }
        public int MaxQuestions
        {
            get => _maxQuestions;
            set
            {
                _maxQuestions = value;
                RaisePropertyChanged(nameof(MaxQuestions));
            }
        }

        public List<string> PlayingQuestion
        {
            get => _playingQuestion;
            set
            {
                _playingQuestion = value;
                RaisePropertyChanged(nameof(PlayingQuestion));
            }
        }
        public int TimerForQuestionPack
        {
            get => _timerForQuestionPack;

            set
            {
                _timerForQuestionPack = value;
                RaisePropertyChanged(nameof(TimerForQuestionPack));
            }
        }
        public string CurrentQuery
        {
            get => _currentQuery;
            set
            {
                _currentQuery = value;
                RaisePropertyChanged(nameof(CurrentQuery));
            }
        }

        public void StartQuiz(QuestionPackViewModel activePack)
        {

            SetupGame(activePack);
            Shuffle(_activePlayingPack);
            SetTimer(TimerForQuestionPack);

            PlayQuiz(_activePlayingPack[_currentQuestionIndex]);

        }
        public void PlayQuiz(Question playingQuestion)
        {
            _currentCorrectAnswer = playingQuestion.CorrectAnswer;
            CurrentQuery = playingQuestion.Query;
            SetupPlayingQuestions(playingQuestion);
            Shuffle(PlayingQuestion);
            SetButtons();


        }

        public void SetButtons()
        {

            AnswerOption1.Answer = PlayingQuestion[0].ToString();
            AnswerOption2.Answer = PlayingQuestion[1].ToString();
            AnswerOption3.Answer = PlayingQuestion[2].ToString();
            AnswerOption4.Answer = PlayingQuestion[3].ToString();
        }
        private void SetupGame(QuestionPackViewModel activePack)
        {
            _activePlayingPack = new List<Question>(activePack.Questions);

            TimerForQuestionPack = activePack.TimeLimitInSeconds;
            MaxQuestions = _activePlayingPack.Count();
            CurrentQuestionView = 1;
            _currentQuestionIndex = 0;
        }


        private void SetupPlayingQuestions(Question playingQuestion)
        {
            PlayingQuestion.Clear();
            PlayingQuestion.Add(playingQuestion.CorrectAnswer.ToString());
            PlayingQuestion.Add(playingQuestion.InCorrectAnswers[0].ToString());
            PlayingQuestion.Add(playingQuestion.InCorrectAnswers[1].ToString());
            PlayingQuestion.Add(playingQuestion.InCorrectAnswers[2].ToString());
        }
        private void SetTimer(int TimerForQuestionPack)
        {
            //_timeLeft är en int??
            _timeLeft = TimeSpan.FromSeconds(TimerForQuestionPack);
            dispatcherTimer.Start();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (_timeLeft == TimeSpan.Zero) dispatcherTimer.Stop();

            else
            {
                _timeLeft = _timeLeft.Add(TimeSpan.FromSeconds(-1));
                TimerForQuestionPack = (int)_timeLeft.TotalSeconds;
            }
        }
        private void Shuffle<T>(IList<T> list)
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