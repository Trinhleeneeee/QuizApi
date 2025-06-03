namespace NewQuizApi.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public double PassingRate { get; set; }
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; } = null!;
        //  Navigation tới các câu trả lời của câu hỏi
        public ICollection<Answer> Answers { get; set; }
        //  Navigation tới các câu trả lời của người dùng
        public ICollection<UserAnswer> UserAnswers { get; set; }
    }

}