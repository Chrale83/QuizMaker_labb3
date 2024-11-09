using QuizMaker_labb3.Command;
using QuizMaker_labb3.Dialogs;
using QuizMaker_labb3.Extension;
using QuizMaker_labb3.Model;
using System.Windows.Media;
using System.Windows.Threading;

namespace QuizMaker_labb3.ViewModel
{
    public class PlayerViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel? mainWindowViewModel;
        private DispatcherTimer dispatcherTimer;
        private TimeSpan timeLeft;

        
        private List<string> _playingQuestion;
        private List<Question> activePlayingPack;
        private int currentQuestionIndex;
        private bool canCheckAnswer = true;
        private int timerForQuestionPack;
        private string currentCorrectAnswer;
        private int _currentQuestionView;
        private int _maxQuestions;
        private string _currentQuery;
        private int _correctAnswers = 0;
        private int _countDownTimer;
        private AnswerOption _answerOption1;
        private AnswerOption _answerOption2;
        private AnswerOption _answerOption3;
        private AnswerOption _answerOption4;
        public PlayerViewModel(MainWindowViewModel? mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
            _playingQuestion = new();
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            AnswerOption1 = new AnswerOption("button1");
            AnswerOption2 = new AnswerOption("button2");
            AnswerOption3 = new AnswerOption("button3");
            AnswerOption4 = new AnswerOption("button4");
            CheckAnswerCommand = new DelegateCommand(CheckAnswer, CanCheckAnswer);
        }

        public int CorrectAnswerCounter
        {
            get => _correctAnswers;
            set
            {
                _correctAnswers = value;
                RaisePropertyChanged(nameof(CorrectAnswerCounter));
            }
        }
        public AnswerOption AnswerOption1
        {
            get => _answerOption1;
            set
            {
                _answerOption1 = value;
                RaisePropertyChanged(nameof(AnswerOption1));
            }
        }
        public AnswerOption AnswerOption2
        {
            get => _answerOption2;
            set
            {
                _answerOption2 = value;
                RaisePropertyChanged(nameof(AnswerOption2));
            }
        }
        public AnswerOption AnswerOption3
        {
            get => _answerOption3;
            set
            {
                _answerOption3 = value;
                RaisePropertyChanged(nameof(AnswerOption3));
            }
        }
        public AnswerOption AnswerOption4
        {
            get => _answerOption4;
            set
            {
                _answerOption4 = value;
                RaisePropertyChanged(nameof(AnswerOption4));
            }
        }
        public void StartQuiz(QuestionPackViewModel activePack)
        {
            SetupGame(activePack);
            Shuffle(activePlayingPack);
            PlayQuiz(activePlayingPack[currentQuestionIndex]);
        }
        public void PlayQuiz(Question playingQuestion)
        {
            currentCorrectAnswer = playingQuestion.CorrectAnswer;
            CurrentQuery = playingQuestion.Query;
            SetupPlayingQuestions(playingQuestion);
            Shuffle(PlayingQuestion);
            SetButtons();
            SetTimer();
        }
        private async void CheckAnswer(object? returnValues)
        {
            if (!canCheckAnswer) return;
            canCheckAnswer = false;
            await CheckAnswerAndShow(returnValues as string[]);
            canCheckAnswer = true;
            dispatcherTimer.Stop();
            
            QuestionCounterPlus();
            NextQuestion();
        }
        private void NextQuestion()
        {
            if (currentQuestionIndex < _maxQuestions)
            {
                PlayQuiz(activePlayingPack[currentQuestionIndex]);
            }
            else
            {
                var quizResultDialog = new QuizResultDialogWindow();
                quizResultDialog.ShowDialog();
                mainWindowViewModel.SelectedViewModel = mainWindowViewModel.ConfigurationViewModel;
            }
        }
        private void QuestionCounterPlus()
        {
            CurrentQuestionView++;
            currentQuestionIndex++;
        }
        private async Task CheckAnswerAndShow(string[] values)
        {
            string answer = values[0];
            string selectedButton = values[1];

            List<AnswerOption> answerOptions = new List<AnswerOption> { AnswerOption1, AnswerOption2, AnswerOption3, AnswerOption4 };

            var selectedOption = answerOptions.FirstOrDefault(opt => opt.Answer == answer);

            if (selectedOption.Answer == currentCorrectAnswer)
            {
                selectedOption.BackGroundColor = SetToGreenColor();
                CorrectAnswerCounter++;
            }

            if (selectedOption.Answer != currentCorrectAnswer)
            {
                foreach (var option in answerOptions)
                {
                    if (option.Answer == currentCorrectAnswer)
                    {
                        option.BackGroundColor = SetToGreenColor();
                        selectedOption.BackGroundColor = SetToRedColor();
                        break;
                    }
                }
            }
            await Task.Delay(1300);

            foreach (var item in answerOptions)
            {
                item.BackGroundColor = SetToDefaultColor();
            }
        }
        private async Task CheckAnswerAndShow()
        {
            List<AnswerOption> answerOptions = new List<AnswerOption> { AnswerOption1, AnswerOption2, AnswerOption3, AnswerOption4 };

            foreach (var option in answerOptions)
            {
                if (option.Answer == currentCorrectAnswer)
                {
                    option.BackGroundColor = SetToGreenColor();
                    await Task.Delay(1300);
                    option.BackGroundColor = SetToDefaultColor();
                    break;
                }
            }
        }

        public SolidColorBrush SetToGreenColor() => new SolidColorBrush(Colors.Green);
        public SolidColorBrush SetToRedColor() => new SolidColorBrush(Colors.Red);
        public SolidColorBrush SetToDefaultColor() => new SolidColorBrush(Colors.LightGray);
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
        public int CountDownTimer
        {
            get => _countDownTimer;
            set
            {
                _countDownTimer = value;
                RaisePropertyChanged(nameof(CountDownTimer));
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
       
       
        public void SetButtons()
        {
            AnswerOption1.Answer = PlayingQuestion[0].ToString();
            AnswerOption2.Answer = PlayingQuestion[1].ToString();
            AnswerOption3.Answer = PlayingQuestion[2].ToString();
            AnswerOption4.Answer = PlayingQuestion[3].ToString();
        }
        private void SetupGame(QuestionPackViewModel activePack)
        {
            activePlayingPack = new List<Question>(activePack.Questions);
            timerForQuestionPack = activePack.TimeLimitInSeconds;
            MaxQuestions = activePlayingPack.Count();
            CurrentQuestionView = 1;
            currentQuestionIndex = 0;
        }
        private void SetupPlayingQuestions(Question playingQuestion)
        {
            PlayingQuestion.Clear();
            PlayingQuestion.Add(playingQuestion.CorrectAnswer.ToString());
            PlayingQuestion.Add(playingQuestion.InCorrectAnswers[0].ToString());
            PlayingQuestion.Add(playingQuestion.InCorrectAnswers[1].ToString());
            PlayingQuestion.Add(playingQuestion.InCorrectAnswers[2].ToString());
        }        
        private void SetTimer()
        {
            timeLeft = TimeSpan.FromSeconds(timerForQuestionPack);
            dispatcherTimer.Start();         
        }

        private async void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (timeLeft == TimeSpan.Zero)
            {
                dispatcherTimer.Stop();
                await CheckAnswerAndShow();
                QuestionCounterPlus();
                NextQuestion();
            }
            else
            {
                timeLeft = timeLeft.Subtract(TimeSpan.FromSeconds(1));
                CountDownTimer = (int)timeLeft.TotalSeconds;
            }
        }
        private bool CanCheckAnswer(object? returnValues)
        {
            return canCheckAnswer;
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