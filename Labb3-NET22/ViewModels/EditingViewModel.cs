using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Labb3_NET22.DataModels;
using Labb3_NET22.Managers;

namespace Labb3_NET22.ViewModels;

public class EditingViewModel : ObservableObject
{
    private NavigationManager _navigationManager;
    private readonly QuizManager _quizManager;
    private Quiz _currentQuiz;
    public ObservableCollection<Question> Questions { get; set; } = new ();
    public IRelayCommand NavigateGoBackCommand { get; }
    public IRelayCommand NavigateRemoveCommand { get; }
    public IRelayCommand NavigateUpdateCommand { get; }

    private Question _selectedQuestion;
    private int _selectedQuestionIndex;
    public Question SelectedQuestion
    {
        get { return _selectedQuestion; }
        set
        {
            SetProperty(ref _selectedQuestion, value);
            _selectedQuestionIndex = Questions.IndexOf(_selectedQuestion);
            if (_selectedQuestionIndex > -1)
            {
                Statement = _selectedQuestion.Statement;
                Answer1 = _selectedQuestion.Answers[0];
                Answer2 = _selectedQuestion.Answers[1];
                Answer3 = _selectedQuestion.Answers[2];
                Answer4 = _selectedQuestion.Answers[3];
                CorrectAnswerIndex = _selectedQuestion.CorrectAnswer;
                NavigateUpdateCommand.NotifyCanExecuteChanged();
                NavigateRemoveCommand.NotifyCanExecuteChanged();
            }
        }
    }
    public EditingViewModel(NavigationManager navigationManager, QuizManager quizManager)
    {
        _navigationManager = navigationManager;
        _quizManager = quizManager;
        _currentQuiz = _quizManager.CurrentQuiz;
        UpdateQuestions();
        _selectedQuestionIndex = Questions.Count; 
        NavigateUpdateCommand = new RelayCommand(() =>
        {
            string[] newAnswers = { Answer1, Answer2, Answer3, Answer4 };
            _currentQuiz.ReplaceQuestion(_selectedQuestionIndex, new Question(Statement, newAnswers, CorrectAnswerIndex)); 
            _quizManager.SaveCurrentQuiz();
            UpdateQuestions();
            Statement = string.Empty;
            Answer1 = string.Empty;
            Answer2 = string.Empty;
            Answer3 = string.Empty;
            Answer4 = string.Empty;
        }, () => _selectedQuestionIndex < Questions.Count);
        NavigateRemoveCommand = new RelayCommand( () =>
        {
            _currentQuiz.RemoveQuestion(_selectedQuestionIndex);
            _quizManager.SaveCurrentQuiz();
            UpdateQuestions();
            SelectedQuestion = Questions[0];
            Statement = string.Empty;
            Answer1 = string.Empty;
            Answer2 = string.Empty;
            Answer3 = string.Empty;
            Answer4 = string.Empty;
        }, () => _selectedQuestionIndex < Questions.Count);
        NavigateGoBackCommand = new RelayCommand(() => _navigationManager.CurrentViewModel = new StartViewModel(_navigationManager, _quizManager));
    }

    private string _statement;
    public string Statement
    {
        get { return _statement; }
        set { SetProperty(ref _statement, value); }
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
    private void UpdateQuestions()
    {
        Questions.Clear();
        foreach (var currentQuizQuestion in _quizManager.CurrentQuiz.Questions)
        {
            Questions.Add(currentQuizQuestion);
        }
    }
}