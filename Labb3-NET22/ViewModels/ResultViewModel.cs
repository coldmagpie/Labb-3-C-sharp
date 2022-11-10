using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Labb3_NET22.DataModels;
using Labb3_NET22.Managers;

namespace Labb3_NET22.ViewModels
{
    public class ResultViewModel:ObservableObject
    {
        private NavigationManager _navigationManager;
        private readonly QuizManager _quizManager;
        public int _score;
        public int Score { get => _score; }
        public ResultViewModel(NavigationManager navigationManager, QuizManager quizManager, int score)
        {
            _navigationManager = navigationManager;
            _quizManager = quizManager;
            _score = score;
            NavigateGoBackCommand = new RelayCommand(() => _navigationManager.CurrentViewModel = new StartViewModel(_navigationManager, _quizManager));
        }
        public IRelayCommand NavigateGoBackCommand { get; }
    }
}
