using QuizMaker_labb3.Command;
using QuizMaker_labb3.Model;
using System.Collections.ObjectModel;
using System.Windows.Forms;

namespace QuizMaker_labb3.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ObservableCollection<QuestionPackViewModel> _packs;
        private ObservableCollection<QuestionPackViewModel> _newPack;
        private QuestionPackViewModel? _activePack;
        private double _newPackTimeInSecondsLeft;
        
        public DelegateCommand CreateNewPackCommand { get; }

        public MainWindowViewModel()
        {
            this.ConfigurationViewModel = new ConfigurationViewModel(this); // gör att dom har en referens tillbax
            this.PlayerViewModel = new PlayerViewModel(this); //Gör så dom har referenser till varandra
            this.DialogsViewModel = new DialogsViewModel();

            this.Packs = new ObservableCollection<QuestionPackViewModel>();
            this.ActivePack = new QuestionPackViewModel(new Model.QuestionPack("My first question pack")); //Denna ska inte ligga i konstruktorn, men kanske inte alls. (denna är hårdkodad)

            Packs.Add(ActivePack);

            CreateNewPackCommand = new DelegateCommand(AddNewPack);
        }
        public string NewPackName { get; set; }
        public Difficulty NewPackDifficulty { get; set; }
        public PlayerViewModel PlayerViewModel { get; }
        public ConfigurationViewModel ConfigurationViewModel { get; }
        public DialogsViewModel DialogsViewModel { get; }
        public double NewPackTimeInSecondsLeft
        {
            get => _newPackTimeInSecondsLeft;
            set { _newPackTimeInSecondsLeft = value; RaisePropertyChanged(nameof(NewPackTimeInSecondsLeft)); }
                    
        }

        public ObservableCollection<QuestionPackViewModel> Packs
        {
            get => _packs;
            set
            {
                _packs = value;

            }
        }
        public ObservableCollection<QuestionPackViewModel> NewPack
        {
            get => _newPack;
            set
            {
                _newPack = value;
                RaisePropertyChanged("NewPack");
            }
        }
        public QuestionPackViewModel? ActivePack //Frågetecknet = visar för kompilatorn på att vi vet att den kan vara null
        {
            get => _activePack;
            set
            {
                _activePack = value;
                //RaisePropertyChanged("ActivePack"); //Ett exempel
                ConfigurationViewModel.RaisePropertyChanged("ActivePack");
            }
        }
        private void AddNewPack(object? arg)
        {
            var NewPack = new QuestionPackViewModel(new Model.QuestionPack("My new question pack"));
            NewPack.Difficulty = NewPackDifficulty;
            NewPack.Name = NewPackName;
            NewPack.TimeLimitInSeconds = (int)NewPackTimeInSecondsLeft;
            Packs.Add(NewPack);
            ActivePack = NewPack;
            
        }
    }
}














