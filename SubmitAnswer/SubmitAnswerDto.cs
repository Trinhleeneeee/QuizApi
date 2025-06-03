using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Collections.Generic;

namespace NewQuizApi.SubmitAnswer
{
    public class QuizDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int PassingRate { get; set; }
        public List<QuestionDto> Questions { get; set; }
    }

    public class QuestionDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<AnswerDto> Answers { get; set; }
    }

    public class AnswerDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }

    public class SubmitAnswerDto
    {
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
    }

    public class QuizResultDto
    {
        public int Score { get; set; }
        public bool Passed { get; set; }
        public int Duration { get; set; } // thời gian làm bài (giây)
        public List<AnswerResultDto> Answers { get; set; }
    }

    public class AnswerResultDto
    {
        public string Text { get; set; }         // Nội dung câu hỏi
        public string YourAnswer { get; set; }   // Đáp án người dùng chọn
        public bool IsCorrect { get; set; }      // Đúng / Sai
    }
}
