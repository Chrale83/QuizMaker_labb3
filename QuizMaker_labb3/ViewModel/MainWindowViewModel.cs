using QuizMaker_labb3.Command;
using QuizMaker_labb3.Dialogs;
using QuizMaker_labb3.Model;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace QuizMaker_labb3.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase _selectedViewModel;
        private ObservableCollection<QuestionPackViewModel> _packs;
        private ObservableCollection<QuestionPackViewModel> _newPack;
        private QuestionPackViewModel? _activePack;
        public List<QuestionPackViewModel> tempPacks = new();
        private double _newPackTimeInSecondsLeft;
        private string _newPackName;
        private bool isFullScreen = false;
        private WindowStyle _windowStyle;
        private WindowState _windowState;
        private WindowState _startWindowState;
        
        public DelegateCommand SetToConfigurationViewCommand { get; }
        public DelegateCommand CloseApplicationCommand { get; }
        public DelegateCommand SavePacksCommand { get; }
        public DelegateCommand CreateNewPackCommand { get; }
        public DelegateCommand CloseWindowCommand { get; }
        public DelegateCommand SelectPackCommand { get; }
        public DelegateCommand DeleteSelectedPackCommand { get; }
        public DelegateCommand UpdateViewCommand { get; }
        public DelegateCommand FullScreenToggleCommand { get; }
        public Difficulty NewPackDifficulty { get; set; }
        public PlayerViewModel PlayerViewModel { get; }
        public ConfigurationViewModel ConfigurationViewModel { get; }
        public DialogsViewModel DialogsViewModel { get; }
        
        public MainWindowViewModel()
        {
            this.ConfigurationViewModel = new ConfigurationViewModel(this); // gör att dom har en referens tillbax
            this.PlayerViewModel = new PlayerViewModel(this); //ska stå this här Gör så dom har referenser till varandra
            this.DialogsViewModel = new DialogsViewModel();
            this.Packs = new ObservableCollection<QuestionPackViewModel>();
           
            WindowState = WindowState.Normal;
            WindowStyle = WindowStyle.SingleBorderWindow;
            this.WindowStyle = WindowStyle.SingleBorderWindow;

            SelectedViewModel = ConfigurationViewModel;

            CloseApplicationCommand = new DelegateCommand(ShutDownApplication);
            UpdateViewCommand = new DelegateCommand(ChangeToPlayerView, CanChangeToPlayerView);
            DeleteSelectedPackCommand = new DelegateCommand(RemoveSelectedPack);
            SelectPackCommand = new DelegateCommand(SelectPack);
            CloseWindowCommand = new DelegateCommand(CloseDialogWindow);
            CreateNewPackCommand = new DelegateCommand(AddNewPack, CanAddNewPack);
            FullScreenToggleCommand = new DelegateCommand(FullScreenToggle);
            SavePacksCommand = new DelegateCommand(SavePacksDataCommand);
            SetToConfigurationViewCommand = new DelegateCommand(ChangeToEditView);
            LoadPacksDataAsync();
        }

        

        private void FullScreenToggle(object obj)
        {
            if (!isFullScreen)
            {
                StartWindowState = WindowState;
                WindowState = WindowState.Maximized;
                isFullScreen = true;
                WindowStyle = WindowStyle.None;
            }
            else
            {
                WindowState = StartWindowState;
                isFullScreen = false;
                WindowStyle = WindowStyle.SingleBorderWindow;
            }

        }
        public async void SavePacksDataCommand(object? arg)
        {
             await SavePacksData();
        }
        
        public async void LoadPacksDataAsync()
        {
            await LoadPackData();
        }

        public async Task SavePacksData()
        {
            try
            {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string fullPathAndFile = Path.Combine(folderPath, "labb3quiz.json");    
            var inJson = JsonSerializer.Serialize<ObservableCollection<QuestionPackViewModel>>(Packs);
            await File.WriteAllTextAsync(fullPathAndFile, inJson);
            }
            catch (Exception error)
            {
                Console.WriteLine($"Ett fel hände vid sparandet av dina packs{error.Message}");
            }
        }
        public async Task LoadPackData()
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string fullPathAndFile = Path.Combine(folderPath, "labb3quiz.json");
            if (!File.Exists(fullPathAndFile))
            {
                
                ActivePack = new QuestionPackViewModel(new QuestionPack("Your First QuestionPack"));
                Packs.Add(ActivePack);
            }
            else
            {
                string json = await File.ReadAllTextAsync(fullPathAndFile);
                var jsonstring = JsonSerializer.Deserialize<ObservableCollection<QuestionPack>>(json);
                foreach (var item in jsonstring)
                {
                    var tempPack = new QuestionPackViewModel(item);
                    Packs.Add(tempPack);
                }
            }
            // Gör om varje questionpack till   Questionpackviewmodel
            //Stoppa in i en observable collection av questionpackviewmodel
            //Sätt Packs till ovan
           ActivePack = Packs.FirstOrDefault();
        }

        public async void ShutDownApplication(object? arg)
        {
            await SavePacksData();
            Application.Current.Shutdown();
        }

        private void ChangeToEditView(object? view)
        {
            SelectedViewModel = ConfigurationViewModel;
            
            UpdateViewCommand.RaiseCanExectueChanged();
            
        }
        
        private void ChangeToPlayerView(object? view)
        {
            SelectedViewModel = PlayerViewModel; 
            UpdateViewCommand.RaiseCanExectueChanged();
            
        }

        private bool CanChangeToPlayerView(object? arg)
        {
            if (ActivePack?.Questions == null) return false;
            return ActivePack.Questions.Any();
        }
            


        public double NewPackTimeInSecondsLeft
        {
            get => _newPackTimeInSecondsLeft;
            set
            {
                _newPackTimeInSecondsLeft = value;
                RaisePropertyChanged(nameof(NewPackTimeInSecondsLeft));
            }

        }
        private void SelectPack(object obj)
        {
            if (obj is QuestionPackViewModel selectedPack)
            {
                ActivePack = selectedPack;
            }
        }
        public string NewPackName
        {
            get => _newPackName;
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
            SetNewPackToEmpty();

            if (arg is CreateNewPackDialog dialog) dialog.CloseDialog();
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
                RaisePropertyChanged(nameof(selectedPack));
            }
        }
        public ViewModelBase SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                RaisePropertyChanged();
                if (value == PlayerViewModel)
                {
                    PlayerViewModel.StartQuiz(ActivePack);
                    RaisePropertyChanged(nameof(SetToConfigurationViewCommand));
                }
            }
        }
        public WindowState StartWindowState
        {
            get => _startWindowState;
            set
            {
                _windowState = value;
                RaisePropertyChanged(nameof(StartWindowState));
            }
        }
        public WindowState WindowState
        {
            get => _windowState;
            set
            {
                _windowState = value;
                RaisePropertyChanged(nameof(WindowState));
            }
        }
        public WindowStyle WindowStyle
        {
            get => _windowStyle; set
            {
                _windowStyle = value;
                RaisePropertyChanged(nameof(WindowStyle));
            }
        }

        private void CloseDialogWindow(object? arg)
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
        private void SetNewPackToEmpty()
        {
            NewPackName = string.Empty;
        }
    }
}