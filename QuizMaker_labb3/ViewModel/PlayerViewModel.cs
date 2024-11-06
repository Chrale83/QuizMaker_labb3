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
        private ObservableCollection<string> _playingQuestion = new();
        private List<Question> _activePlayingPack;
        private int _currentQuestionIndex;
        private int _timerForQuestionPack;
        private int _currentQuestionView;
        private int _maxQuestions;
        private string _currentQuery;
        
        private string _answerOption2;
        private string _answerOption3;
        private string _answerOption4;
        private string _currentCorrectAnswer;
        public AnswerButton _answerOption1;

        public AnswerButton TempAnswerOption1
        {
            get => _answerOption1;
            set
            {
                _answerOption1 = value;
                RaisePropertyChanged(nameof(TempAnswerOption1));
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

        }



        private void AnswerButton(object returnValues)
        {
            if (returnValues is string[] Returns)
            {
                string answer = Returns[0];
                string whatButton = Returns[1];

                if (answer == _currentCorrectAnswer)
                {
                    switch (whatButton)
                    {
                        case "Button1":
                            TempAnswerOption1.BackGroundColor = new SolidColorBrush(Colors.Green);
                            break;
                        case "Button2":
                            TempAnswerOption1.BackGroundColor = new SolidColorBrush(Colors.Green);
                            break;
                        case "Button3":
                            TempAnswerOption1.BackGroundColor = new SolidColorBrush(Colors.Green);
                            break;
                        case "Button4":
                            TempAnswerOption1.BackGroundColor = new SolidColorBrush(Colors.Green);
                            break;
                    }
                }




                //answer = "test";

                //    if (answer == _currentCorrectAnswer)
                //    {
                //        if (buttonNr == "Button4")
                //        {
                //            TempAnswerOption1.BackGroundColor = new SolidColorBrush(Colors.Green);

                //        }
                //    }
                //    else
                //    {
                //        //FEL SVAR
                //    }


                dispatcherTimer.Stop();



                CurrentQuestionView++;
                _currentQuestionIndex++;
                if (_currentQuestionIndex < _maxQuestions)
                {
                    PlayQuiz(_activePlayingPack[_currentQuestionIndex]);
                }
            }
        }

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
        public string AnswerOption1
        {
            get => _answerOption1;
            set
            {
                _answerOption1 = value;
                RaisePropertyChanged(nameof(AnswerOption1));
            }
        }
        public string AnswerOption2
        {
            get => _answerOption2;
            set
            {
                _answerOption2 = value;
                RaisePropertyChanged(nameof(AnswerOption2));
            }
        }
        public string AnswerOption3
        {
            get => _answerOption3;
            set
            {
                _answerOption3 = value;
                RaisePropertyChanged(nameof(AnswerOption3));
            }
        }
        public string AnswerOption4
        {
            get => _answerOption4;
            set
            {
                _answerOption4 = value;
                RaisePropertyChanged(nameof(AnswerOption4));
            }
        }
        public ObservableCollection<string> PlayingQuestion
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


            _currentQuestionIndex++;
        }

        public void SetButtons()
        {


            AnswerOption1 = PlayingQuestion[0];
            AnswerOption2 = PlayingQuestion[1];
            AnswerOption3 = PlayingQuestion[2];
            //AnswerOption4 = PlayingQuestion[3];
            TempAnswerOption1.Answer = PlayingQuestion[3];
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
            //_timeLeft är en inte??
            _timeLeft = TimeSpan.FromSeconds(TimerForQuestionPack); //<---
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