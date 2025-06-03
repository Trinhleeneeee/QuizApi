using Microsoft.AspNetCore.Mvc;
using NewQuizApi.Service;
using NewQuizApi.Models;
using System.Linq;
using NewQuizApi.SubmitAnswer;


namespace NewQuizApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _service;

        public QuizController(IQuizService service)
        {
            _service = service;
        }

        [HttpGet("{quizId}")]
        public async Task<IActionResult> GetQuiz(int quizId)
        {
            var quiz = await _service.GetQuizAsync(quizId);
            return quiz == null ? NotFound() : Ok(quiz);
        }

        [HttpPost("{quizId}/start")]
        public async Task<IActionResult> StartQuiz(int quizId)
        {
            var result = await _service.StartQuizAsync(quizId);
            return Ok(result);
        }

        [HttpPost("{quizResultId}/submit")]
        public async Task<IActionResult> SubmitAnswer(int quizResultId, [FromBody] SubmitAnswerDto dto)
        {
            var answer = await _service.SubmitAnswerAsync(quizResultId, dto.QuestionId, dto.AnswerId);
            return Ok(new
            {
                answer.QuestionId,
                dto.AnswerId,
                answer.IsCorrect
            });
        }

        [HttpPost("{quizResultId}/complete")]
        public async Task<IActionResult> CompleteQuiz(int quizResultId)
        {
            var result = await _service.CompleteQuizAsync(quizResultId);
            return Ok(new
            {
                result.Score,
                result.Passed,
                Duration = (result.EndTime - result.StartTime).TotalSeconds
            });
        }

        [HttpGet("result/{quizResultId}")]
        public async Task<IActionResult> GetResult(int quizResultId)
        {
            var result = await _service.GetResultAsync(quizResultId);
            if (result == null)
                return NotFound();

            var dto = new QuizResultDto
            {
                Score = result.Score,
                Passed = result.Passed,
                Duration = (int)(result.EndTime - result.StartTime).TotalSeconds,
                Answers = result.AnswerUsers.Select(a => new AnswerResultDto
                {
                    Text = a.Question.Text,
                    YourAnswer = a.AnswerSelected.Text,
                    IsCorrect = a.IsCorrect
                }).ToList()
            };

            return Ok(dto);
        }

    }
}

