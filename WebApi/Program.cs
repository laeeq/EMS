using Microsoft.EntityFrameworkCore;
using WebApi.Database;
using WebApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("ConnectionString");
if (string.IsNullOrEmpty(connectionString))
{
    builder.Services.AddScoped<IEmployeeRepository, TestEmployeeRepository>();
}
else
{
    builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connectionString));
    builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
}

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (!string.IsNullOrEmpty(connectionString))
{
    var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
