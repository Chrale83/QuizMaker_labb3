using QuizMaker_labb3.Command;
using QuizMaker_labb3.Model;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;

namespace QuizMaker_labb3.ViewModel
{
    internal class ConfigurationViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel? mainWindowViewModel;

        private Question _activeQuestion;
        public Question ActiveQuestion
        {
            get => _activeQuestion;
            set
            {
                _activeQuestion = value;
                RaisePropertyChanged();
            }
        }
        public DelegateCommand AddQuestionCommand { get; }
        public DelegateCommand DeleteQuestionCommand { get; }
        //public DelegateCommand CreateNewPack { get; }
        
        public ConfigurationViewModel(MainWindowViewModel? mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
            AddQuestionCommand = new DelegateCommand(AddQuestion);
            DeleteQuestionCommand = new DelegateCommand(DeleteQuestion, CanDeleteQuestion);
            //CreateNewPack = new DelegateCommand(AddNewPack);
        }

        public QuestionPackViewModel? ActivePack { get => mainWindowViewModel.ActivePack; }
        
        private void AddQuestion(object obj)
        {
            if (ActivePack != null)
            {
            var question = new Question("New Question", string.Empty, string.Empty, string.Empty, string.Empty);
            ActiveQuestion = question;
            mainWindowViewModel.ActivePack.Questions.Add(question);
            }
                
            DeleteQuestionCommand.RaiseCanExectueChanged();
        }

        private void DeleteQuestion(object obj)
        {
            if (ActivePack != null && ActiveQuestion != null)
            {
                ActivePack.Questions.Remove(ActiveQuestion);
                DeleteQuestionCommand.RaiseCanExectueChanged();
            }
        }
        private bool CanDeleteQuestion(object? arg) //denna metod gör det möjligt att disabla knappen --> arg står för argument? (inparametern)
        {
            return ActivePack.Questions.Any();
        }

        //private void AddNewPack(object? arg)
        //{
        //    var newPack = new QuestionPackViewModel(new QuestionPack("hd"));
            
        //}
    }
}
//Här ska logiken bakom configView ligga

//Man
// ska kunna bygga “Question Packs”, det vill säga “paket”(LISTOR?) med frågor. Man
// behöver kunna lägga till, ta bort, och redigera befintliga frågor. Alla
// frågor har fyra alternativ, varav ett korrekt. Man ska även kunna ändra
// inställningar för själva paketet “Pack Options”: Välja namn, märka upp 
//med svårighetsgrad, samt sätta tidsgräns på frågorna. Man ska även kunna
// skapa nya “Packs”, och ta bort befintliga.

//Det ska gå 
//att lägga till / ta bort frågor på flera sätt: Via menyn, Via 
//snabbknappar på tangentbordet, och via knappar i appen. Även “Pack 
//Options” ska gå att öppna på alla 3 sätt.

