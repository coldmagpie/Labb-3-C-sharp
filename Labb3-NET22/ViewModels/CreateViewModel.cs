using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Labb3_NET22.DataModels;
using Labb3_NET22.Managers;


namespace Labb3_NET22.ViewModels;

public class CreateViewModel : ObservableObject
{
    private NavigationManager _navigationManager;
    private readonly QuizManager _quizManager;

    public Quiz? CurrentQuiz
    {
        get { return _quizManager.CurrentQuiz; }
        set { _quizManager.CurrentQuiz = value; }
    }

    private string _title;

    public string Title
    {
        get { return _title; }
        set
        {
            SetProperty(ref _title, value);
            CurrentQuiz = new Quiz(value);

            SaveQuestionCommand.NotifyCanExecuteChanged();
            SaveQuizCommand.NotifyCanExecuteChanged();
        }
    }

    public IRelayCommand NavigateGoBackCommand { get; }
    public IRelayCommand SaveQuestionCommand { get; }
    public IRelayCommand SaveQuizCommand { get; }

    public CreateViewModel(NavigationManager navigationManager, QuizManager quizManager)
    {
        _navigationManager = navigationManager;
        _quizManager = quizManager;

        _quizManager.CurrentQuizChanged += QuizManagerOnCurrentQuizChanged;

        NavigateGoBackCommand = new RelayCommand(() =>
            _navigationManager.CurrentViewModel = new StartViewModel(_navigationManager, _quizManager));
        

        SaveQuestionCommand = new RelayCommand(() =>
        {
            string[] newAnswers = { Answer1, Answer2, Answer3, Answer4 };
            CurrentQuiz.AddQuestion(NewStatement, newAnswers, CorrectAnswerIndex);
            NewStatement = string.Empty;
            Answer1 = string.Empty;
            Answer2 = string.Empty;
            Answer3 = string.Empty;
            Answer4 = string.Empty;
        }, () => CurrentQuiz != null);

        SaveQuizCommand = new RelayCommand(() =>
        {
            _quizManager.SaveCurrentQuiz();
            Title = string.Empty;
        }, () => CurrentQuiz != null);
    }

    private void QuizManagerOnCurrentQuizChanged()
    {
        OnPropertyChanged(nameof(CurrentQuiz));
    }

    private string _newStatement;

    public string NewStatement
    {
        get { return _newStatement; }
        set { SetProperty(ref _newStatement, value); }
    }

    private string _answer1;

    public string Answer1
    {
        get { return _answer1; }
        set { SetProperty(ref _answer1, value); }
    }

    private string _answer2;

    public string Answer2
    {
        get { return _answer2; }
        set { SetProperty(ref _answer2, value); }
    }

    private string _answer3;

    public string Answer3
    {
        get { return _answer3; }
        set { SetProperty(ref _answer3, value); }
    }

    private string _answer4;

    public string Answer4
    {
        get { return _answer4; }
        set { SetProperty(ref _answer4, value); }
    }

    private int _correctAnswerIndex;

    public int CorrectAnswerIndex
    {
        get { return _correctAnswerIndex; }
        set { SetProperty(ref _correctAnswerIndex, value); }
    }
}