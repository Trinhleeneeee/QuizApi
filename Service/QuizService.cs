using NewQuizApi.Models;
using NewQuizApi.Repository;
namespace NewQuizApi.Service
{
public class QuizService : IQuizService
{
    private readonly IQuizRepository _repo;
    public QuizService(IQuizRepository repo) => _repo = repo;

    public Task<Quiz?> GetQuizAsync(int quizId) => _repo.GetQuizWithQuestionsAsync(quizId);
    public Task<QuizResult> StartQuizAsync(int quizId) => _repo.StartQuizAsync(quizId);
    public Task<UserAnswer> SubmitAnswerAsync(int quizResultId, int questionId, int answerId) =>
        _repo.SubmitAnswerAsync(quizResultId, questionId, answerId);
    public Task<QuizResult> CompleteQuizAsync(int quizResultId) =>
        _repo.CompleteQuizAsync(quizResultId);
    public Task<QuizResult?> GetResultAsync(int quizResultId) =>
        _repo.GetQuizResultWithDetailsAsync(quizResultId);
}

}
