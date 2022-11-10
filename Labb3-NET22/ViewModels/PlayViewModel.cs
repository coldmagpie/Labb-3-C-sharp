using CommunityToolkit.Mvvm.Input;
using Labb3_NET22.DataModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Controls;
using Labb3_NET22.Managers;

namespace Labb3_NET22.ViewModels
{
    public class PlayViewModel : ObservableObject
    {
        private NavigationManager _navigationManager;
        private Question _question;
        private static Quiz _quiz;
        private QuizManager _quizManager;
        public int _questionCounter = 1;

        public int QuestionCounter
        {
            get { return _questionCounter;}
            set { SetProperty(ref _questionCounter, value); }

        }

        private int _quizLength;
        public int QuizLength { get => _quizLength; }


        public string Statement { get => _question.Statement; }
        public string[] Answers { get => _question.Answers; }

        private string _correctAnswer = string.Empty;

        private int _score;

        public int Score
        {
            get { return _score; }
            set
            {
                SetProperty(ref _score, value);
            }
        }

        public string CorrectAnswer
        {
            get
            {
                return _correctAnswer;
            }
            set
            {
                if (value.Equals(""))
                { 
                    SetProperty(ref _correctAnswer, value);
                }
                else
                {
                    SetProperty(ref _correctAnswer, (int.Parse(value) + 1).ToString());
                }
            }
        }

        private bool _selectedAnswerOne;
        public bool SelectedAnswerOne
        {
            get
            {
                return _selectedAnswerOne;
            }
            set
            {
                SetProperty(ref _selectedAnswerOne, value);
            }
        }

        private bool _selectedAnswerTwo;

        public bool SelectedAnswerTwo
        {
            get
            {
                return _selectedAnswerTwo;
            }
            set
            {
                SetProperty(ref _selectedAnswerTwo, value);
            }
        }

        private bool _selectedAnswerThree;

        public bool SelectedAnswerThree
        {
            get
            {
                return _selectedAnswerThree;
            }
            set
            {
                SetProperty(ref _selectedAnswerThree, value);
            }
        }

        private bool _selectedAnswerFour;

        public bool SelectedAnswerFour
        {
            get
            {
                return _selectedAnswerFour;
            }
            set
            {
                SetProperty(ref _selectedAnswerFour, value);
            }
        }

        public PlayViewModel(NavigationManager navigationManager, QuizManager quizManager)
        {
            _quizManager = quizManager;
            _quiz = _quizManager.CurrentQuiz;
            _navigationManager = navigationManager;
            _quizLength = _quiz.Questions.Count();
            _question = _quiz.GetRandomQuestion(); 
            

            NavigateGoBackCommand = new RelayCommand(() => _navigationManager.CurrentViewModel = new StartViewModel(_navigationManager, _quizManager));
            UpdateCorrectCommand = new RelayCommand(() => { ValidateAnswer(); });
            NextQuestionCommand = new RelayCommand(() =>
            {
                if (QuestionCounter < QuizLength)
                {
                    QuestionCounter++;
                    _question = _quiz.GetRandomQuestion();
                    OnPropertyChanged("Statement");
                    OnPropertyChanged("Answers");
                    CorrectAnswer = string.Empty;
                }
                else
                {
                    _navigationManager.CurrentViewModel = new ResultViewModel(_navigationManager, _quizManager, Score);
                }
            });
        }
        private void ValidateAnswer()
        {
            CorrectAnswer = _question.CorrectAnswer.ToString();
            switch (_question.CorrectAnswer)
            {
                case 0:
                    if (_selectedAnswerOne)
                    {
                        Score++;
                    }
                    break;
                case 1:
                    if (_selectedAnswerTwo)
                    {
                        Score++;
                    }
                    break;
                case 2:
                    if (_selectedAnswerThree)
                    {
                        Score++;
                    }
                    break;
                case 3:
                    if (_selectedAnswerFour)
                    {
                        Score++;
                    }
                    break;
            }
        }
        public IRelayCommand NavigateGoBackCommand { get; }
        public IRelayCommand UpdateCorrectCommand { get; }
        public IRelayCommand NextQuestionCommand { get; }
    }
}



