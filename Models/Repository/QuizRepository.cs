using Microsoft.EntityFrameworkCore;
using NewQuizApi.Models;
using NewQuizApi.Models.Data;

namespace NewQuizApi.Repository;

public class QuizRepository : IQuizRepository
{
    private readonly QuizDbContext _context;
    public QuizRepository(QuizDbContext context) => _context = context;

    public async Task<Quiz?> GetQuizWithQuestionsAsync(int quizId) =>
        await _context.Quiz
            .Include(q => q.Questions)
                .ThenInclude(q => q.Answers)
            .FirstOrDefaultAsync(q => q.Id == quizId);

    public async Task<Question?> GetQuestionByIdAsync(int questionId) =>
        await _context.Questions.Include(q => q.Answers).FirstOrDefaultAsync(q => q.Id == questionId);

    public async Task<Answer?> GetAnswerByIdAsync(int answerId) =>
        await _context.Answers.FirstOrDefaultAsync(a => a.Id == answerId);

    public async Task<QuizResult> StartQuizAsync(int quizId)
    {
        var result = new QuizResult
        {
            QuizId = quizId,
            StartTime = DateTime.UtcNow,
            AnswerUsers = new List<UserAnswer>()
        };
        _context.QuizResults.Add(result);
        await _context.SaveChangesAsync();
        return result;
    }

    public async Task<UserAnswer> SubmitAnswerAsync(int quizResultId, int questionId, int answerId)
    {
        var answer = await GetAnswerByIdAsync(answerId);
        if (answer == null || answer.QuestionId != questionId)
            throw new Exception("Invalid answer.");

        var userAnswer = new UserAnswer
        {
            QuizResultId = quizResultId,
            QuestionId = questionId,
            AnswerSelectedId = answerId,
            IsCorrect = answer.IsCorrect
        };

        _context.UserAnswers.Add(userAnswer);
        await _context.SaveChangesAsync();
        return userAnswer;
    }

    public async Task<QuizResult> CompleteQuizAsync(int quizResultId)
    {
        var result = await _context.QuizResults
            .Include(r => r.AnswerUsers)
            .ThenInclude(ua => ua.AnswerSelected)
            .Include(r => r.Quiz)
            .FirstOrDefaultAsync(r => r.Id == quizResultId);

        if (result == null) throw new Exception("QuizResult not found");

        result.EndTime = DateTime.UtcNow;
        var correctAnswers = result.AnswerUsers.Count(a => a.IsCorrect);
        var totalQuestions = result.Quiz.Questions.Count;
        result.Score = (int)((double)correctAnswers / totalQuestions * 100);
        result.Passed = result.Score >= result.Quiz.PassingRate;

        await _context.SaveChangesAsync();
        return result;
    }

    public async Task<QuizResult?> GetQuizResultWithDetailsAsync(int quizResultId)
    {
        return await _context.QuizResults
            .Include(r => r.Quiz)
            .Include(r => r.AnswerUsers)
                .ThenInclude(a => a.Question)
            .Include(r => r.AnswerUsers)
                .ThenInclude(a => a.AnswerSelected)
            .FirstOrDefaultAsync(r => r.Id == quizResultId);
    }
}
