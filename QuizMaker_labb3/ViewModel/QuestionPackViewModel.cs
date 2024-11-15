﻿using QuizMaker_labb3.Model;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace QuizMaker_labb3.ViewModel
{
    public class QuestionPackViewModel : ViewModelBase
    {
        public readonly QuestionPack _model;

        public QuestionPackViewModel(QuestionPack model)
        {
            _model = model;
            Questions = new ObservableCollection<Question>(model.Questions);
        }
        [JsonConstructor]
        public QuestionPackViewModel() {
            _model = new QuestionPack() { Questions = [] };
            Questions = new ObservableCollection<Question>(_model.Questions);
            
        }

        public ObservableCollection<Question> Questions { get; }
        public Difficulty Difficulty
        {
            get => _model.Difficulty;
            set
            {
                _model.Difficulty = value;
                RaisePropertyChanged(nameof(Difficulty));
            }
        }
        public string Name
        {
            get => _model.Name;
            set
            {
                _model.Name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }
        public int TimeLimitInSeconds
        {
            get => _model.TimeLimitInSeconds;
            set
            {
                _model.TimeLimitInSeconds = value;
                RaisePropertyChanged(nameof(TimeLimitInSeconds));
            }
        }
    }
}
