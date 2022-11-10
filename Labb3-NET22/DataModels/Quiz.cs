using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Xml;

namespace Labb3_NET22.DataModels;   

public class Quiz
{
    private Random random = new Random();

    private IEnumerable<Question> _questions;
    public IEnumerable<Question> Questions => _questions;

    private string _title = string.Empty;
    public string Title => _title;

    public Quiz ()
    {
        
    }
    public Quiz(string title)
    {
        _title = title;
        _questions = new List<Question>();
    }

    public Quiz(IEnumerable<Question> questions, string title)
    {
        _questions = questions;
        _title = title;
    }
    public Question GetRandomQuestion()
    {
        int index = random.Next(0, _questions.Count());
        Question question = _questions.ElementAt(index);
        _questions = _questions.Where((q) => !q.Statement.Equals(question.Statement));
        return question;
    }

    public void AddQuestion(string statement, string[] answers, int correctAnswer)
    {
        Question question = new Question(statement, answers, correctAnswer);
        var temp = (List<Question>)Questions;
        temp.Add(question);
    }
    public void RemoveQuestion(int index)
    {
        var temp = (List<Question>)Questions;
        temp.RemoveAt(index);
    }

    public void ReplaceQuestion(int index, Question question)
    {
        _questions = _questions.ToList().Select((q, i) => index == i ? question : q);
    }
}