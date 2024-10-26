﻿using QuizMaker_labb3.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QuizMaker_labb3.ViewModel
{
    internal class QuestionPackViewModel : ViewModelBase
    {
        private readonly QuestionPack model;


        public QuestionPackViewModel(QuestionPack model)
        {
            this.model = model;
            this.Questions = new ObservableCollection<Question>(model.Questions);
        }
        public string Name 
        {
            get => model.Name;
            set
            {
                model.Name = value;
                RaisePropertyChanged();

            }
        }
        public Difficulty Difficulty
        {
            get => model.Difficulty;
            set
            {
                model.Difficulty = value;
                RaisePropertyChanged();
            }
        }
        public int TimeLimitInSeconds
        {
            get => model.TimeLimitInSeconds;
            set
            {
                model.TimeLimitInSeconds = value;
                RaisePropertyChanged("TimeInSeconds");
            }
        }
        public ObservableCollection<Question> Questions { get; }

        
    }
}
