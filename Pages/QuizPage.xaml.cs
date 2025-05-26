using System;
using System.Collections.Generic;
using System.Linq;

namespace Recraft.Pages
{
    public partial class QuizPage : ContentPage
    {
        private class QuizQuestion
        {
            public string QuestionText { get; set; }
            public string[] Choices { get; set; }
            public int CorrectIndex { get; set; }
        }

        private List<QuizQuestion> allQuestions = new List<QuizQuestion>
        {
            new QuizQuestion
            {
                QuestionText = "What is the best way to recycle plastic?",
                Choices = new[] { "Throw in trash", "Burn it", "Reuse or send to recycling center", "Bury it" },
                CorrectIndex = 2
            },
            new QuizQuestion
            {
                QuestionText = "How long does it take for paper to decompose?",
                Choices = new[] { "2 weeks", "2 months", "2 years", "200 years" },
                CorrectIndex = 1
            },
            new QuizQuestion
            {
                QuestionText = "What is composting?",
                Choices = new[] { "Burning trash", "Decomposing organic waste", "Throwing food away", "Recycling metal" },
                CorrectIndex = 1
            },
            new QuizQuestion
            {
                QuestionText = "Which material is not recyclable?",
                Choices = new[] { "Glass", "Aluminum", "Styrofoam", "Paper" },
                CorrectIndex = 2
            },
            new QuizQuestion
            {
                QuestionText = "What does 'eco-friendly' mean?",
                Choices = new[] { "Harmful to nature", "Safe for the environment", "Expensive", "Plastic-free" },
                CorrectIndex = 1
            },
            new QuizQuestion
            {
                QuestionText = "Which one is a renewable energy source?",
                Choices = new[] { "Coal", "Natural gas", "Wind", "Plastic" },
                CorrectIndex = 2
            },
            new QuizQuestion
            {
                QuestionText = "How can you save water at home?",
                Choices = new[] { "Keep tap running", "Take shorter showers", "Water lawn daily", "Flush often" },
                CorrectIndex = 1
            }
        };

        private List<QuizQuestion> quizQuestions;
        private int currentQuestionIndex = 0;
        private int score = 0;

        public QuizPage()
        {
            InitializeComponent();
            quizQuestions = allQuestions.OrderBy(q => Guid.NewGuid()).Take(5).ToList();
            ShowQuestion();
        }

        private void ShowQuestion()
        {
            if (currentQuestionIndex < quizQuestions.Count)
            {
                var q = quizQuestions[currentQuestionIndex];

                QuestionNumberLabel.Text = $"Question {currentQuestionIndex + 1} of {quizQuestions.Count}";
                QuestionLabel.Text = q.QuestionText;

                OptionA.Content = q.Choices[0];
                OptionB.Content = q.Choices[1];
                OptionC.Content = q.Choices[2];
                OptionD.Content = q.Choices[3];

                OptionA.IsChecked = false;
                OptionB.IsChecked = false;
                OptionC.IsChecked = false;
                OptionD.IsChecked = false;
            }
            else
            {
                QuestionNumberLabel.Text = "Quiz Complete!";
                QuestionLabel.Text = $"Your Score: {score} / {quizQuestions.Count}";

                OptionA.IsVisible = false;
                OptionB.IsVisible = false;
                OptionC.IsVisible = false;
                OptionD.IsVisible = false;

                NextButton.IsVisible = false;
            }
        }

        private void OnNextClicked(object sender, EventArgs e)
        {
            var selected = GetSelectedAnswerIndex();

            if (selected == -1)
            {
                DisplayAlert("Please select an answer", "Choose one of the options before proceeding.", "OK");
                return;
            }

            if (selected == quizQuestions[currentQuestionIndex].CorrectIndex)
            {
                score++;
            }

            currentQuestionIndex++;
            ShowQuestion();
        }

        private int GetSelectedAnswerIndex()
        {
            if (OptionA.IsChecked) return 0;
            if (OptionB.IsChecked) return 1;
            if (OptionC.IsChecked) return 2;
            if (OptionD.IsChecked) return 3;
            return -1;
        }
    }
}
