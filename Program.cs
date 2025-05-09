using Microsoft.EntityFrameworkCore;
using ProductApi.Data;

using DotNetEnv;


var builder = WebApplication.CreateBuilder(args);
Env.Load();
string connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION")
                       ?? builder.Configuration.GetConnectionString("DefaultConnection") ?? "";
if (string.IsNullOrEmpty(connectionString))
{
    throw new Exception("Database connection string not configured.");
}
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    

    // Seed only if in Development environment
    if (app.Environment.IsDevelopment())
    {
        // DELETE EVERYTHING!
        await db.Database.EnsureDeletedAsync();
        
        // Then recreate it
        await db.Database.EnsureCreatedAsync();
        await DbSeeder.SeedAsync(db);
    }
    else
    {
        await db.Database.MigrateAsync(); // Ensure database is up to date
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();
app.Run();

