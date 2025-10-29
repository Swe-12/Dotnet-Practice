using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// ------------------------------
// 1️⃣ Add required services
// ------------------------------

// Enables Swagger / OpenAPI for API testing (optional but useful)
builder.Services.AddOpenApi();

// Registers controller-based API endpoints (like TasksController)
builder.Services.AddControllers();

// Enables CORS (Cross-Origin Resource Sharing) so React frontend (localhost:3000)
// can talk to this .NET backend (localhost:5001)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()   // Allow all origins (React app, etc.)
              .AllowAnyMethod()   // Allow all HTTP methods (GET, POST, PUT, DELETE)
              .AllowAnyHeader();  // Allow all request headers
    });
});

var app = builder.Build();

// ------------------------------
// 2️⃣ Configure middleware pipeline
// ------------------------------

// Enable CORS policy (must come before MapControllers)
app.UseCors("AllowAll");

// Only enable Swagger/OpenAPI in development mode
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Redirect HTTP to HTTPS for security
app.UseHttpsRedirection();

// Map controller routes (connects /api/tasks → TasksController)
app.MapControllers();

// ------------------------------
// 3️⃣ Run the application
// ------------------------------
app.Run();
