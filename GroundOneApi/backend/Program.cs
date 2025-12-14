using Microsoft.EntityFrameworkCore;
using backend.Models;
using backend.Data;
using backend.Services;
using backend.Repository;
using backend.Middleware;
using Microsoft.OpenApi.Models;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// ==================================================
// 1. Database confinguration
// ==================================================
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Data Source=app.db"; // Fallback dla development

// determining database provider based on environment
if (builder.Environment.IsProduction())
{
    // PostgreSQL for Railway (*)
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(connectionString));
}
else
{
    // SQLite for development
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlite(connectionString));
}

// ==================================================
// 2. DEPENDENCY INJECTION
// ==================================================

// Repositories
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<ICompartmentRepository, CompartmentRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();

// Services
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<ICompartmentService, CompartmentService>();
builder.Services.AddScoped<IItemService, ItemService>();

// FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddFluentValidationAutoValidation();

// ==================================================
// 3. CONTROLLERS
// ==================================================
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // enum as string in JSON
        options.JsonSerializerOptions.Converters.Add(
            new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

// ==================================================
// 4. CORS - connect adn allow frontend
// ==================================================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        // Pobierz allowed origins z appsettings.json
        var allowedOrigins = builder.Configuration
            .GetSection("AllowedOrigins")
            .Get<string[]>() ?? new[] 
            { 
                "http://localhost:5173",  // Vite dev
                "http://localhost:5174"   // Backup port
            };

        policy.WithOrigins(allowedOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// ==================================================
// 5. SWAGGER / OpenAPI
// ==================================================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "GroundOne API - Fire Department Vehicle Management",
        Version = "v1",
        Description = "API do zarządzania flotą pojazdów straży pożarnej, " +
                      "przedziałami i wyposażeniem",
        Contact = new OpenApiContact
        {
            Name = "Andrzej Musiałek",
            Email = "a.musialke@example.com",
            Url = new Uri("https://github.com/yourusername/groundone")
        },
        License = new OpenApiLicense
        {
            Name = "MIT License",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });

    // XML comments
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

// ==================================================
// 6. LOGGING
// ==================================================
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

if (builder.Environment.IsProduction())
{
    builder.Logging.AddFilter("Microsoft.EntityFrameworkCore", LogLevel.Warning);
}

var app = builder.Build();

// ==================================================
// 7. MIDDLEWARE PIPELINE (order counts! check the order one by one <depends what you want>)
// ==================================================

// Swagger only in deveplopment and production (or if portfolio demo)
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "GroundOne API v1");
        c.RoutePrefix = "swagger"; // access at /swagger
    });
}

// Global Exception Handler -  it must be before other middlewares
app.UseMiddleware<ExceptionHandlingMiddleware>();

// HTTPS Redirection
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
    app.UseHsts();
}

// CORS - before Authorization
app.UseCors("AllowFrontend");

// Authorization
app.UseAuthorization();

// Controllers
app.MapControllers();

// Health check endpoint
app.MapGet("/health", () => Results.Ok(new 
{ 
    status = "healthy", 
    timestamp = DateTime.UtcNow,
    environment = app.Environment.EnvironmentName
}));

// ==================================================
// 8. DATABASE INITIALIZATION
// ==================================================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        
        // Development: EnsureCreated
        // Production: use migrations
        if (app.Environment.IsDevelopment())
        {
            context.Database.EnsureCreated();
            logger.LogInformation("Database ensured created (Development)");
        }
        else
        {
            // Production: Apply pending migrations
            var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
            {
                logger.LogInformation("Applying {Count} pending migrations", 
                    pendingMigrations.Count());
                await context.Database.MigrateAsync();
                logger.LogInformation("Migrations applied successfully");
            }
        }
        
        // Seed data only in development (*)
        if (app.Environment.IsDevelopment())
        {
            DatabaseSeeder.SeedData(context);
            logger.LogInformation("Database seeded successfully");
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while initializing the database");
        
        // in production rethrow to stop the app
        if (app.Environment.IsDevelopment())
        {
            throw;
        }
    }
}

// ==================================================
// 9. Start the app
// ==================================================
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Logger.LogInformation("Starting GroundOne API on port {Port}", port);
app.Logger.LogInformation("Environment: {Environment}", app.Environment.EnvironmentName);

app.Run();

// partial public class for integration tests
public partial class Program { }