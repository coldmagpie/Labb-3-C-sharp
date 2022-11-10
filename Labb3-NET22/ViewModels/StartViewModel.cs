using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Labb3_NET22.Managers;

namespace Labb3_NET22.ViewModels
{
    public class StartViewModel : ObservableObject
    {
        private NavigationManager _navigationManager;
        private readonly QuizManager _quizManager;
        public IRelayCommand NavigatePlayCommand { get; }
        public IRelayCommand NavigateEditCommand { get; }
        public IRelayCommand NavigateCreateCommand { get; }

        public StartViewModel(NavigationManager navigationManager, QuizManager quizManager)
        {
            _navigationManager = navigationManager;
            _quizManager = quizManager;
            NavigatePlayCommand = new RelayCommand(() => _navigationManager.CurrentViewModel = new ChooseQuizViewModel(_navigationManager, _quizManager, Choice.PlayQuiz));
            NavigateEditCommand = new RelayCommand(() => _navigationManager.CurrentViewModel = new ChooseQuizViewModel(_navigationManager, _quizManager, Choice.EditQuiz));
            NavigateCreateCommand = new RelayCommand(() => _navigationManager.CurrentViewModel = new CreateViewModel(_navigationManager, _quizManager));
        }
    }
}
