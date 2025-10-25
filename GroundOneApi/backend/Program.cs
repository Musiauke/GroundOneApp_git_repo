using Microsoft.EntityFrameworkCore;
using backend.Models;
using backend.Data;
using backend.Services;
using backend.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Database Context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

// Repositories - Repository Pattern
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<ICompartmentRepository, CompartmentRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();

// Services - Business Logic Layer
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<ICompartmentService, CompartmentService>();
builder.Services.AddScoped<IItemService, ItemService>();

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS dla React
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        builder =>
        {
            builder.WithOrigins("http://localhost:5173") // port Vite frontend
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

// Database Seeding
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    
    // Upewnij się że baza istnieje
    context.Database.EnsureCreated();
    
    // Dodaj dane seed
    DatabaseSeeder.SeedData(context);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontend");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();