using QuizMaker_labb3.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace QuizMaker_labb3.ViewModel
{
    internal class PlayerViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel? mainWindowViewModel;

        private DispatcherTimer timer;
        private string _testData;

        public string TestData
        {
            get => _testData;
            private set
            {
                _testData = value;
                RaisePropertyChanged();
            }
        }

        public DelegateCommand UpdateButtonCommand { get; }

        public PlayerViewModel(MainWindowViewModel? mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
            TestData = "Start Value";
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); //Varje sekund så skriver den ut ett x
            timer.Tick += Timer_Tick;
            //timer.Start();

            UpdateButtonCommand = new DelegateCommand(updateButton, CanUpdateButton);
        }

        private bool CanUpdateButton(object? arg) //denna metod gör det möjligt att disabla knappen --> arg står för argument? (inparametern)
        {
            return TestData.Length < 20;
        }

        private void updateButton(object obj)
        {
            TestData += "x";
            UpdateButtonCommand.RaiseCanExectueChanged(); //Denna gör så att den blir disablad när villkoret i "canUpdate" är uppfyllt
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            TestData += "x";
        }
    }
}
