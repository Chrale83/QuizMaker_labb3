using QuizMaker_labb3.Command;
using QuizMaker_labb3.Model;

namespace QuizMaker_labb3.ViewModel
{
    public class ConfigurationViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel? _mainWindowViewModel;
        private Question _activeQuestion;
        public ConfigurationViewModel(MainWindowViewModel? mainWindowViewModel)
        {
            this._mainWindowViewModel = mainWindowViewModel;
            AddQuestionCommand = new DelegateCommand(AddQuestion);
            DeleteQuestionCommand = new DelegateCommand(DeleteQuestion, CanDeleteQuestion);
        }

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
        public QuestionPackViewModel? ActivePack { get => _mainWindowViewModel?.ActivePack; }
        
        
        private void AddQuestion(object? obj)
        {
            
            var question = new Question("New Question", string.Empty, string.Empty, string.Empty, string.Empty);
            ActiveQuestion = question;
            _mainWindowViewModel?.ActivePack?.Questions.Add(question);

            _mainWindowViewModel?.UpdateViewCommand.RaiseCanExectueChanged();
            DeleteQuestionCommand.RaiseCanExectueChanged();
            
            

        }
        private void DeleteQuestion(object? obj)
        {
            if (ActivePack != null && ActiveQuestion != null)
            {
                ActivePack.Questions.Remove(ActiveQuestion);
                DeleteQuestionCommand.RaiseCanExectueChanged();
            }
        }
        private bool CanDeleteQuestion(object? arg) 
        {
            return ActivePack.Questions.Any();
        }
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

