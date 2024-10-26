using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizMaker_labb3.ViewModel
{
	class MainWindowViewModel : ViewModelBase
	{
		public ObservableCollection<QuestionPackViewModel> Packs { get; set; }
        public PlayerViewModel PlayerViewModel { get; }
        public ConfigurationViewModel ConfigurationViewModel { get;}

        private QuestionPackViewModel? _activePack;

		public QuestionPackViewModel? ActivePack //Frågetecknet = visar för kompilatorn på att vi vet att den kan vara null
		{
			get => _activePack;
			set
			{
				_activePack = value;
				RaisePropertyChanged();
				ConfigurationViewModel.RaisePropertyChanged("ActivePack"); //Ett exempel
			}

		}

        public MainWindowViewModel()
        {
            this.ConfigurationViewModel = new ConfigurationViewModel(this); // gör att dom har en refernes tillbax
            this.PlayerViewModel = new PlayerViewModel(this); //Gör så dom har referenser till varandra
            
			this.ActivePack = new QuestionPackViewModel(new Model.QuestionPack("My Question Pack")); //Denna ska inte ligga i konstruktorn, men kanske inte alls. (denna är hårdkodad)
        }
    }
}
