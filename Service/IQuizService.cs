using System.Collections.Generic;
using System.Threading.Tasks;
using NewQuizApi.Models;

namespace NewQuizApi.Service
{
    public interface IQuizService
    {
        Task<Quiz?> GetQuizAsync(int quizId);
        Task<QuizResult> StartQuizAsync(int quizId);
        Task<UserAnswer> SubmitAnswerAsync(int quizResultId, int questionId, int answerId);
        Task<QuizResult> CompleteQuizAsync(int quizResultId);
        Task<QuizResult?> GetResultAsync(int quizResultId);
    }
}