using FootballBuddy.Auth.Api;
using FootballBuddy.Auth.Application;
using FootballBuddy.Auth.Infrastructure;
using FootballBuddy.Auth.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


var rootPath = Path.GetFullPath(
    Path.Combine(builder.Environment.ContentRootPath, "..", "..", ".."));

builder.Configuration
    .SetBasePath(rootPath)
    .AddJsonFile("configuration/appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile(
        $"configuration/appsettings.{builder.Environment.EnvironmentName}.json",
        optional: true,
        reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddControllers().AddAuthApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("AuthDb")));
        
builder.Services.AddAuthApplication();
builder.Services.AddAuthInfrastructure(builder.Configuration);

var app = builder.Build();

await ApplyMigrationsAsync(app);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();


static async Task ApplyMigrationsAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
    
    await dbContext.Database.MigrateAsync();
}