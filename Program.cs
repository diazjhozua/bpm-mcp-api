using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "BPM MCP API - Hackathon Demo",
        Version = "v1",
        Description = @"🚧 **HACKATHON PROJECT - FOR DEMONSTRATION PURPOSES ONLY** 🚧

This API is developed for hackathon group use and educational purposes. It demonstrates Business Process Management concepts including employees, travel, assets, and purchases management.

⚠️ **Important Notice:** This application does not reflect any specific business flow or processes of any actual company. All data, workflows, and business logic are fictional and created solely for demonstration and learning purposes.

**Features:**
- Employee expense management
- Travel request and expense tracking
- Asset assignment and inventory
- Purchase request processing

**Note:** All endpoints return mock/dummy data for demonstration purposes.",
    });

    // Enable annotations
    c.EnableAnnotations();

    // Set the comments path for the Swagger JSON and UI
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
