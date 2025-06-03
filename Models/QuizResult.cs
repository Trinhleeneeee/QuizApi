namespace NewQuizApi.Models
{
    public class QuizResult
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public bool Passed { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; } = null!;
        public ICollection<UserAnswer> AnswerUsers { get; set; } = new List<UserAnswer>();
    }
}