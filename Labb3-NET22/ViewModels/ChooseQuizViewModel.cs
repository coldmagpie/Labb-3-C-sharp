using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Xml.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Labb3_NET22.DataModels;
using Labb3_NET22.Managers;
using Microsoft.Win32;

namespace Labb3_NET22.ViewModels
{
    public enum Choice
    {
        PlayQuiz,
        EditQuiz
    }
    public class ChooseQuizViewModel : ObservableObject
    {
        private NavigationManager _navigationManager;
        private QuizManager _quizManager;
        private Choice _choice;
        private async Task LoadQuiz()
        {
            await _quizManager.OpenFolder();
            if (_quizManager.CurrentQuiz == null)
            {
                _navigationManager.CurrentViewModel = new StartViewModel(_navigationManager, _quizManager);
                return;
            }

            if (_choice.Equals(Choice.PlayQuiz))
            {
                _navigationManager.CurrentViewModel = new PlayViewModel(_navigationManager, _quizManager);
            }
            else
            {
                _navigationManager.CurrentViewModel = new EditingViewModel(_navigationManager, _quizManager);
            }
        }
        public IRelayCommand NavigateGoBackCommand { get; }
        public IRelayCommand NavigateOpenFileCommand { get; }
        public ChooseQuizViewModel(NavigationManager navigationManager, QuizManager quizManager, Choice choice)
        {
            _navigationManager = navigationManager;
            _quizManager = quizManager;
            _choice = choice;
            NavigateGoBackCommand = new RelayCommand(() => _navigationManager.CurrentViewModel = new StartViewModel(_navigationManager, _quizManager));
            NavigateOpenFileCommand = new AsyncRelayCommand(LoadQuiz);
        }
    }
}
