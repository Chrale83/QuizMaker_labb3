using QuizMaker_labb3.Command;
using System.Collections.ObjectModel;

namespace QuizMaker_labb3.ViewModel
{
    class MainWindowViewModel : ViewModelBase
	{
        public ObservableCollection<QuestionPackViewModel> Packs
        {
            get => _packs;
            set
            {
                _packs = value;
                RaisePropertyChanged();
            }
        }
        public PlayerViewModel PlayerViewModel { get; }
        public ConfigurationViewModel ConfigurationViewModel { get;}
		public DialogsViewModel DialogsViewModel { get; }

        private QuestionPackViewModel? _activePack; //Valda paketet?
        private ObservableCollection<QuestionPackViewModel> _packs = new();

        public DelegateCommand CreateNewPack { get; }

        public QuestionPackViewModel? ActivePack //Frågetecknet = visar för kompilatorn på att vi vet att den kan vara null
		{
			get => _activePack;
			set
			{
				_activePack = value;

				RaisePropertyChanged("ActivePack"); //Ett exempel
			}

		}

        public MainWindowViewModel()
        {
			this.ActivePack = new QuestionPackViewModel(new Model.QuestionPack("My first question pack")); //Denna ska inte ligga i konstruktorn, men kanske inte alls. (denna är hårdkodad)
            this.ConfigurationViewModel = new ConfigurationViewModel(this); // gör att dom har en referens tillbax
            this.PlayerViewModel = new PlayerViewModel(this); //Gör så dom har referenser till varandra
			this.DialogsViewModel = new DialogsViewModel();
            
            CreateNewPack = new DelegateCommand(AddNewPack);
        }


        private void AddNewPack(object? arg)
        {
            
            var newPack = new QuestionPackViewModel(new Model.QuestionPack("My new question pack"));
            Packs.Add(newPack);
            ActivePack = newPack;
            ActivePack.Name = "tes";
            RaisePropertyChanged("Packs");
            
        }

    }
}
