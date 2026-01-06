using Microsoft.EntityFrameworkCore;
using backend.Models;
using backend.Data;
using backend.Services;
using backend.Repository;
using backend.Middleware;
using Microsoft.OpenApi.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore.Diagnostics; 

var builder = WebApplication.CreateBuilder(args);

var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

// ==================================================
// 1. Database confinguration
// ==================================================
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
string connectionString;

if (!string.IsNullOrEmpty(databaseUrl))
{
    var uri = new Uri(databaseUrl.Replace("postgres://", "postgresql://"));
    var userInfo = uri.UserInfo.Split(':');

    connectionString =
        $"Host={uri.Host};Port={uri.Port};Database={uri.AbsolutePath.TrimStart('/')};" +
        $"Username={userInfo[0]};Password={userInfo[1]};SSL Mode=Require;Trust Server Certificate=true";

    Console.WriteLine("✅ Using PostgreSQL (Railway)");
}
else
{
    connectionString =
        builder.Configuration.GetConnectionString("DefaultConnection")
        ?? "Data Source=app.db";

    Console.WriteLine("⚠️ Using SQLite (local)");
}


//// DEBUG - log connection string (remove in production later)
//Console.WriteLine($"🔍 Connection String (first 50 chars): {(connectionString?.Length > 50 ? connectionString.Substring(0, 50) + "..." : connectionString ?? "NULL")}");

// determining database provider based on environment
if (!string.IsNullOrEmpty(databaseUrl))
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(connectionString)
               .ConfigureWarnings(warnings =>
                   warnings.Ignore(RelationalEventId.PendingModelChangesWarning)));
}
else
{
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
            Name = "Musiauke",
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
//if (!app.Environment.IsDevelopment())
//{
//    app.UseHttpsRedirection();
//    app.UseHsts();
//}

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
// 8. DATABASE INITIALIZATION WITH RETRY
// ==================================================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();

    // Retry configuration
    const int maxRetries = 5;
    const int delayMs = 2000;

    for (int attempt = 1; attempt <= maxRetries; attempt++)
    {
        try
        {
            var context = services.GetRequiredService<AppDbContext>();

            // Test connection
            context.Database.CanConnect();
            logger.LogInformation("Database connection established");

            if (app.Environment.IsDevelopment())
            {
                // Development: EnsureCreated + Seed
                context.Database.EnsureCreated();
                logger.LogInformation("Database ensured created (Development)");

                DatabaseSeeder.SeedData(context);
                logger.LogInformation("Database seeded successfully");
            }
            else
            {
                // Production: Apply migrations
                var pendingMigrations = context.Database
                    .GetPendingMigrationsAsync()
                    .GetAwaiter()
                    .GetResult();

                if (pendingMigrations.Any())
                {
                    logger.LogInformation("Applying {Count} pending migrations",
                        pendingMigrations.Count());
                    context.Database.MigrateAsync().GetAwaiter().GetResult();
                    logger.LogInformation("✅ Migrations applied successfully");
                }
                else
                {
                    logger.LogInformation("No pending migrations");
                }
            }

            // Success - break the retry loop
            break;
        }
        catch (Exception ex)
        {
            if (attempt == maxRetries)
            {
                logger.LogError(ex,
                    "❌ Database initialization failed after {Attempts} attempts",
                    maxRetries);

                // In development: crash immediately
                if (app.Environment.IsDevelopment())
                {
                    throw;
                }

                // In production: log and continue (app will work without DB initially)
                logger.LogWarning("⚠️ Application starting without database connection");
            }
            else
            {
                logger.LogWarning(ex,
                    "Database initialization failed (attempt {Attempt}/{Max}). Retrying in {Delay}ms...",
                    attempt, maxRetries, delayMs);
                Thread.Sleep(delayMs);
            }
        }
    }
}

// ==================================================
// 9. Start the app
// ==================================================
app.Logger.LogInformation("Starting GroundOne API on port {Port}", port);
app.Logger.LogInformation("Environment: {Environment}", app.Environment.EnvironmentName);

app.Run();

