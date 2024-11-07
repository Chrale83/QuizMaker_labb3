﻿using QuizMaker_labb3.Command;
using QuizMaker_labb3.Dialogs;
using QuizMaker_labb3.Extension;
using QuizMaker_labb3.Model;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using static System.Windows.Forms.Design.AxImporter;

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
                if (value == PlayerViewModel)
                {
                    PlayerViewModel.StartQuiz(ActivePack);

                }
            }
        }

        private ObservableCollection<QuestionPackViewModel> _packs;
        private ObservableCollection<QuestionPackViewModel> _newPack;
        private QuestionPackViewModel? _activePack;
        public List<QuestionPackViewModel> tempPacks = new();
        private double _newPackTimeInSecondsLeft;
        private string _newPackName;
        private bool isFullScreen = false;
        private WindowStyle _windowStyle;
        public WindowStyle WindowStyle
        {
            get => _windowStyle; set
            {
                _windowStyle = value;
                RaisePropertyChanged(nameof(WindowStyle));
            }
        }
        public MainWindowViewModel()
        {
            this.ConfigurationViewModel = new ConfigurationViewModel(this); // gör att dom har en referens tillbax
            this.PlayerViewModel = new PlayerViewModel(this); //ska stå this här Gör så dom har referenser till varandra
            this.DialogsViewModel = new DialogsViewModel();
            this.Packs = new ObservableCollection<QuestionPackViewModel>();

            WindowState = new WindowState();
            WindowStyle = new WindowStyle();
            this.WindowStyle = WindowStyle.SingleBorderWindow;




            SelectedViewModel = ConfigurationViewModel;
            //Packs.Add(ActivePack);



            UpdateViewCommand = new DelegateCommand(ChangeToPlayerView, CanChangeToPlayerView);
            DeleteSelectedPackCommand = new DelegateCommand(RemoveSelectedPack);
            SelectPackCommand = new DelegateCommand(SelectPack);
            CloseWindowCommand = new DelegateCommand(CloseDialogWindow);
            CreateNewPackCommand = new DelegateCommand(AddNewPack, CanAddNewPack);
            FullScreenToggleCommand = new DelegateCommand(FullScreenToggle);
            SavePacksCommand = new DelegateCommand(SavePacksDataCommand);

            LoadPacksData();
            ActivePack = new();
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
        public void SavePacksDataCommand(object? arg)
        {
            // Anropa den asynkrona metoden på ett korrekt sätt
            _ = SavePacksData();
            string debug = "debug";
        }

        public async Task SavePacksData()
        {
            tempPacks = Packs.ToList();
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string fullPathAndFile = Path.Combine(folderPath, "quizpackdata.json");
            var jsonOptions = new JsonSerializerOptions { IncludeFields = true };
            var json = JsonSerializer.Serialize<List<QuestionPackViewModel>>(tempPacks, jsonOptions);
            await File.WriteAllTextAsync(fullPathAndFile, json);
        }



        public void LoadPacksData()
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string fullPathAndFile = Path.Combine(folderPath, "quizpackdata.json");

            if (File.Exists(fullPathAndFile))
            {
                var jsonOptions = new JsonSerializerOptions
                {
                    IncludeFields = true,
                    
                };
                string json = File.ReadAllText(fullPathAndFile);
                tempPacks = JsonSerializer.Deserialize<List<QuestionPackViewModel>>(json, jsonOptions);
                Packs = new ObservableCollection<QuestionPackViewModel>(tempPacks);
                string debug = "debug";
            }
            else
            {

                Packs = new ObservableCollection<QuestionPackViewModel>();
            }
        }









        private void ChangeToPlayerView(object? view)
        {
            SelectedViewModel = PlayerViewModel;
            UpdateViewCommand.RaiseCanExectueChanged();
        }

        private bool CanChangeToPlayerView(object? arg)
        {
            if (ActivePack?.Questions == null)
            {
                // Logga eller sätt en brytpunkt här
                return false;
            }

            return ActivePack.Questions.Any();
        }
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
            get => _activePack ?? (_activePack = new QuestionPackViewModel());
            set
            {
                _activePack = value;
                RaisePropertyChanged(nameof(ActivePack));
                ConfigurationViewModel.RaisePropertyChanged("ActivePack");
            }
        }
        private WindowState _windowState;
        private WindowState _startWindowState;
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

        private void AddNewPack(object? arg)
        {
            var NewPack = new QuestionPackViewModel(new QuestionPack(NewPackName, NewPackDifficulty, (int)NewPackTimeInSecondsLeft));
            Packs.Add(NewPack);
            ActivePack = NewPack;
            CleanUp();

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
        private void CleanUp()
        {
            NewPackName = string.Empty;
        }
    }
}


