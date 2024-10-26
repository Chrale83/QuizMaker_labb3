﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuizMaker_labb3.Command
{
    internal class DelegateCommand : ICommand
    {
        private readonly Action<object> exectue;
        private readonly Func<object?, bool> canExectue;

        public event EventHandler? CanExecuteChanged; //Fungerar som propertyChanged

        public DelegateCommand(Action<object> exectue, Func<object?, bool> canExectue = null) //Den kan vara en referns till en metod som tar ett object in
        {
            ArgumentNullException.ThrowIfNull(exectue);
            this.exectue = exectue;
            this.canExectue = canExectue;
        }

        public void RaiseCanExectueChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        public bool CanExecute(object? parameter) //Den kör denna först, sen Execute() Returnerar en bool
        {
            return canExectue is null? true : canExectue(parameter); //Tenerary operator
        }

        public void Execute(object? parameter) //Den koden som körs när man trycker på knappen, men bara om CanExecute() är true 
        {
            exectue(parameter);
        }

        //public bool CanExecute(object? parameter) => canExectue is null ? true : canExectue(parameter);

        //public void Execute(object? parameter) => exectue(parameter);

    }
}
