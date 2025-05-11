using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Lab3
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private readonly Random _rnd = new Random();
        private readonly List<string> _responses = new List<string>
        {
            "Так",
            "Ні",
            "Скоріше так",
            "Скоріше ні"
        };

        private string _question;
        public string Question
        {
            get => _question;
            set
            {
                _question = value;
                OnPropertyChanged(nameof(Question));
            }
        }

        private string _answer;
        public string Answer
        {
            get => _answer;
            set
            {
                _answer = value;
                OnPropertyChanged(nameof(Answer));
            }
        }

        public ICommand AskCommand { get; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            AskCommand = new RelayCommand(OnAsk, CanAsk);
        }

        private bool CanAsk(object parameter)
        {
           
            return !string.IsNullOrWhiteSpace(Question);
        }

        private void OnAsk(object parameter)
        {
      
            Answer = _responses[_rnd.Next(_responses.Count)];
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
