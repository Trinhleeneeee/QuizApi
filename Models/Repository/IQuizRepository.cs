using NewQuizApi.Models;

namespace NewQuizApi.Repository;

public interface IQuizRepository
{
    Task<Quiz?> GetQuizWithQuestionsAsync(int quizId);
    Task<Question?> GetQuestionByIdAsync(int questionId);
    Task<Answer?> GetAnswerByIdAsync(int answerId);
    Task<QuizResult> StartQuizAsync(int quizId);
    Task<UserAnswer> SubmitAnswerAsync(int quizResultId, int questionId, int answerId);
    Task<QuizResult> CompleteQuizAsync(int quizResultId);
    Task<QuizResult?> GetQuizResultWithDetailsAsync(int quizResultId);
}
