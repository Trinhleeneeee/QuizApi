using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewQuizApi.Models.Data
{
    public static class QuizSeeder
    {
        public static void Seed(QuizDbContext context)
        {
            if (context.Quiz.Any()) return;

            var quiz = new Quiz
            {
                Title = "General Knowledge",
                Questions = new List<Question>
        {
            new Question
            {
                Text = "What is the capital of France?",
                Answers = new List<Answer>
                {
                    new Answer { Text = "Paris", IsCorrect = true },
                    new Answer { Text = "London", IsCorrect = false },
                    new Answer { Text = "Berlin", IsCorrect = false },
                    new Answer { Text = "Rome", IsCorrect = false },
                }
            },
            new Question
            {
                Text = "Which planet is known as the Red Planet?",
                Answers = new List<Answer>
                {
                    new Answer { Text = "Earth", IsCorrect = false },
                    new Answer { Text = "Mars", IsCorrect = true },
                    new Answer { Text = "Venus", IsCorrect = false },
                    new Answer { Text = "Jupiter", IsCorrect = false },
                }
            }
        }
            };

            context.Quiz.Add(quiz);
            context.SaveChanges();
        }
    }
}