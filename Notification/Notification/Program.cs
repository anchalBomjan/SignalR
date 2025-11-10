using Microsoft.EntityFrameworkCore;
using Notification.Data;
using Notification.Hubs;
using Notification.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// SignalR
builder.Services.AddSignalR();

// Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IApplicationDbContext>(provider =>
    provider.GetRequiredService<ApplicationDbContext>());

// MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

// AutoMapper (MISSING - Add this)
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Services
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IDataSeeder, DataSeeder>(); // Add data seeder


// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Apply migrations automatically in development (OPTIONAL - Add this)
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
}

app.UseHttpsRedirection(); // Add this for production

app.UseCors("AngularApp");

app.UseRouting();
app.UseAuthorization();

// Map endpoints
app.MapControllers();
app.MapHub<NotificationHub>("/notificationHub");

app.Run();