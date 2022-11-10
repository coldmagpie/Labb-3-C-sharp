using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using Labb3_NET22.DataModels;
using Microsoft.Win32;

namespace Labb3_NET22.Managers
{
    public class QuizManager
    {
        private Quiz _currentQuiz;
        string path = (Path.Combine(Environment.GetFolderPath(folder: Environment.SpecialFolder.LocalApplicationData), "QuizGame"));

        public Quiz? CurrentQuiz
        {
            get { return _currentQuiz; }
            set
            {
                _currentQuiz = value; 
                OnCurrentQuizChanged();
            }
        }
        public async Task OpenFolder()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Json files (*.json)|*.json|Text files (*.txt)|*.txt";

            if (dlg.ShowDialog() == true)
            {
                var name = Path.GetFileNameWithoutExtension(dlg.FileName);
                await LoadCurrentQuiz(name);
            }
        }

        public async Task LoadCurrentQuiz(string quizName)
        {
            var filePath = Path.Combine(path, quizName + ".json");
            using (StreamReader r = new StreamReader(filePath))
            {
                string json = await r.ReadToEndAsync();
                var questions = JsonSerializer.Deserialize<List<Question>>(json);
                CurrentQuiz = new Quiz(questions,quizName);
            }
        }
        public async Task SaveCurrentQuiz()
        {
            var filePath = Path.Combine(path, CurrentQuiz.Title + ".json");
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                await sw.WriteLineAsync(JsonSerializer.Serialize(CurrentQuiz.Questions,
                    new JsonSerializerOptions() { WriteIndented = true }));
            }
        }
        private void OnCurrentQuizChanged()
        {
            CurrentQuizChanged?.Invoke();
        }

        public event Action CurrentQuizChanged;
    }
}
