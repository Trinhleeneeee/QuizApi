using NewQuizApi.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using NewQuizApi.Service;
using NewQuizApi.Repository;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// DB
builder.Services.AddDbContext<QuizDbContext>(options =>
    options.UseInMemoryDatabase("QuizDb"));

// DI
builder.Services.AddScoped<IQuizRepository, QuizRepository>();
builder.Services.AddScoped<IQuizService, QuizService>();




// API controllers
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.WriteIndented = true; // Tùy chọn
    });

// Swagger / OpenAPI
builder.Services.AddOpenApi();

var app = builder.Build();

// Seed DB
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<QuizDbContext>();
    QuizSeeder.Seed(db);
}

// Development tools
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.MapOpenApi();
}

// Serve static HTML in wwwroot
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // API endpoints
});

app.Run();
