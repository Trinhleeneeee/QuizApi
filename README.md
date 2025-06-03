# QuizApi
**Files in project:**

**1.QuizApiController.cs**
This is the main API for the client (JS on the HTML side) to call.

**2.Models**
Quiz.cs, Question.cs, Answer.cs, UserAnswer.cs, QuizResult.cs
These are entity classes (data models) mapped to the database via Entity Framework.

**3.QuizDbContext.cs**
Declare DbSet<Quiz>, DbSet<Question>
Connect to the database and manage migration.

**4.QuizSeeder.cs**
Used to create initial fake data (seed) such as sample questions, test answers.

**5.Repository**
Pattern Repository helps separate data retrieval logic from the controller and service.

**6.Service**
Encapsulated business processing logic

**7.SubmitAnswerDto.cs**
DTO (Data Transfer Object) to receive data from the client when submitting answers.

**8.wwwroot/(start,DoQuiz,Rrssult)**
This directory contains static files that are served directly to the browser (HTML, JS, CSS...).

**9.Program.cs**
Entry point of the application.
Configuring middleware, routing, service DI container, CORS
