using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace NewQuizApi.Models.Data
{
    public class QuizDbContext : DbContext
    {
        public QuizDbContext(DbContextOptions<QuizDbContext> options) : base(options) { }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Quiz> Quiz { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }
        public DbSet<QuizResult> QuizResults { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cấu hình mối quan hệ giữa các thực thể

            // Quiz - Question (Một Quiz có nhiều Question)
            modelBuilder.Entity<Question>()
                .HasOne(q => q.Quiz)
                .WithMany(quiz => quiz.Questions)
                .HasForeignKey(q => q.QuizId)
                .OnDelete(DeleteBehavior.Cascade); // Xóa cascade nếu muốn xóa Question khi Quiz bị xóa

            // Question - Answer (Một Question có nhiều Answer)
            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.Cascade); // Xóa cascade nếu muốn xóa Answer khi Question bị xóa

            // Question - AnswerUser (Một Question có nhiều AnswerUser)
            modelBuilder.Entity<UserAnswer>()
                .HasOne(au => au.Question)
                .WithMany(q => q.UserAnswers)
                .HasForeignKey(au => au.QuestionId)
                .OnDelete(DeleteBehavior.Restrict); // Giữ lại AnswerUser nếu Question bị xóa (hoặc chọn Cascade nếu muốn)

            // Answer - AnswerUser (AnswerSelectedId trong AnswerUser trỏ đến Answer)
            modelBuilder.Entity<UserAnswer>()
                .HasOne(au => au.AnswerSelected)
                .WithMany() // Answer không có navigation property về AnswerUser
                .HasForeignKey(au => au.AnswerSelectedId)
                .OnDelete(DeleteBehavior.Restrict); // Giữ lại AnswerUser nếu Answer bị xóa (hoặc chọn Cascade nếu muốn)

            // QuizResult - Quiz (Một QuizResult thuộc về một Quiz)
            modelBuilder.Entity<QuizResult>()
                .HasOne(qr => qr.Quiz)
                .WithMany(quiz => quiz.QuizResults)
                .HasForeignKey(qr => qr.QuizId)
                .OnDelete(DeleteBehavior.Cascade); // Xóa cascade nếu muốn xóa QuizResult khi Quiz bị xóa

            // QuizResult - AnswerUser (Một QuizResult có nhiều AnswerUser)
            modelBuilder.Entity<UserAnswer>()
                .HasOne(au => au.QuizResult)
                .WithMany(qr => qr.AnswerUsers)
                .HasForeignKey(au => au.QuizResultId)
                .OnDelete(DeleteBehavior.Cascade); // Xóa cascade nếu muốn xóa AnswerUser khi QuizResult bị xóa

            base.OnModelCreating(modelBuilder);
        }
    }
}