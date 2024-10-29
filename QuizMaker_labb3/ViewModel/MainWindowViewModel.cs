using System.Collections.ObjectModel;

namespace QuizMaker_labb3.ViewModel
{
    class MainWindowViewModel : ViewModelBase
	{
		public ObservableCollection<QuestionPackViewModel> Packs { get; set; }
        public PlayerViewModel PlayerViewModel { get; }
        public ConfigurationViewModel ConfigurationViewModel { get;}

        private QuestionPackViewModel? _activePack; //Valda paketet?

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
			this.ActivePack = new QuestionPackViewModel(new Model.QuestionPack("My Question Pack")); //Denna ska inte ligga i konstruktorn, men kanske inte alls. (denna är hårdkodad)
            this.ConfigurationViewModel = new ConfigurationViewModel(this); // gör att dom har en referens tillbax
            this.PlayerViewModel = new PlayerViewModel(this); //Gör så dom har referenser till varandra
            
        }
		

    }
}
