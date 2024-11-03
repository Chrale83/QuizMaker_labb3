using QuizMaker_labb3.Command;
using QuizMaker_labb3.Dialogs;
using QuizMaker_labb3.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace QuizMaker_labb3.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {

        private ViewModelBase _selectedViewModel;
        public ViewModelBase SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<QuestionPackViewModel> _packs;
        private ObservableCollection<QuestionPackViewModel> _newPack;
        private QuestionPackViewModel? _activePack;
        private double _newPackTimeInSecondsLeft;
        private string _newPackName;
        public MainWindowViewModel()
        {
            this.ConfigurationViewModel = new ConfigurationViewModel(this); // gör att dom har en referens tillbax
            this.PlayerViewModel = new PlayerViewModel(this); //ska stå this här Gör så dom har referenser till varandra
            this.DialogsViewModel = new DialogsViewModel();
            this.Packs = new ObservableCollection<QuestionPackViewModel>();
            this.ActivePack = new QuestionPackViewModel(new QuestionPack("My first question pack")); //Denna ska inte ligga i konstruktorn, men kanske inte alls. (denna är hårdkodad)
            
            SelectedViewModel = ConfigurationViewModel;
            Packs.Add(ActivePack);
            
            UpdateViewCommand = new DelegateCommand(ChangeToPlayerView, CanChangeToPlayerView);
            DeleteSelectedPackCommand = new DelegateCommand(RemoveSelectedPack);
            SelectPackCommand = new DelegateCommand(SelectPack);
            CloseWindowCommand = new DelegateCommand(CloseDialogWindow);
            CreateNewPackCommand = new DelegateCommand(AddNewPack, CanAddNewPack);
            
        }
        private void ChangeToPlayerView(object? view)
        {
            SelectedViewModel = PlayerViewModel;
            UpdateViewCommand.RaiseCanExectueChanged();
        }
        private bool CanChangeToPlayerView(object? arg)
        {
            return ActivePack.Questions.Any();
        }
        public DelegateCommand CreateNewPackCommand { get; }
        public DelegateCommand CloseWindowCommand { get; }
        public DelegateCommand SelectPackCommand { get; }
        public DelegateCommand DeleteSelectedPackCommand { get; }
        public DelegateCommand UpdateViewCommand { get; }
        
        public Difficulty NewPackDifficulty { get; set; }
        public PlayerViewModel PlayerViewModel { get; }
        public ConfigurationViewModel ConfigurationViewModel { get; }
        public DialogsViewModel DialogsViewModel { get; }
        
        public double NewPackTimeInSecondsLeft
        {
            get => _newPackTimeInSecondsLeft;
            set { _newPackTimeInSecondsLeft = value; RaisePropertyChanged(nameof(NewPackTimeInSecondsLeft)); }
                    
        }
        private void SelectPack(object obj)
        {
            if (obj is QuestionPackViewModel selectedPack)
            {
                ActivePack = selectedPack;
            }
        }
        public string NewPackName { get => _newPackName;
            set 
            { 
                _newPackName = value; 
                RaisePropertyChanged(nameof(NewPackName));
                CreateNewPackCommand.RaiseCanExectueChanged();
            }
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
        public QuestionPackViewModel? ActivePack
        {
            get => _activePack;
            set
            {
                _activePack = value;
                RaisePropertyChanged(nameof(ActivePack));
                ConfigurationViewModel.RaisePropertyChanged("ActivePack");
            }
        }
                
        private void AddNewPack(object? arg)
        {
            var NewPack = new QuestionPackViewModel(new QuestionPack(NewPackName, NewPackDifficulty, (int)NewPackTimeInSecondsLeft));
            Packs.Add(NewPack);
            ActivePack = NewPack;
            CleanUp();
            
            if (arg is CreateNewPackDialog dialog)  dialog.CloseDialog();
        }

        private bool CanAddNewPack(object? arg)
        {
            return !string.IsNullOrEmpty(NewPackName);
        }

        private void RemoveSelectedPack(object obj)
        {
            if (obj is QuestionPackViewModel selectedPack)
            {
                Packs.Remove(selectedPack);
            }
        }

        private bool CanRemoveSelectedPack(object? arg)
        {
            return !Packs.Any();
        }
        private void CloseDialogWindow(object? arg) // <------------- Fixa onödig If-sats? går att göra bättre (PRIO)
        {
            if (arg is CreateNewPackDialog dialogNew)
            {
                dialogNew.CloseDialog();
            }
            else if (arg is PackOptionsDialog dialogEdit)
            {
                dialogEdit.CloseDialog();
            }
        }
        private void CleanUp()
        {
            NewPackName = string.Empty;
        }
    }
}    



            


            
        

            




        





                

