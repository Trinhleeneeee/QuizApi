# QuizApi
**Steps to run the code:
First, when running the code, paste the link: http://localhost:5132/start.html
Then you can run the app.**

**Note:
1.This is a static views, so the new path is start.html.
2.Only seed data at User ID:1, so other UserIDs will not be able to load the quiz
3.Because using UseInMemoryDatabase means using the device memory to run, there is no need to install or run any database application.**

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
