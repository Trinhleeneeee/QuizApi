using NewQuizApi.Models;

namespace NewQuizApi.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; } = null!;
    }

}