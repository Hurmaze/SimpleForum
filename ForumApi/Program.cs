using Microsoft.EntityFrameworkCore;
using DAL.DbAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var forumConnectionString = builder.Configuration.GetConnectionString("ForumDb");
builder.Services.AddDbContext<ForumDbContext>(x => x.UseSqlServer(forumConnectionString));
builder.Services.AddTransient<ForumDbContext>();

var authConnectionString = builder.Configuration.GetConnectionString("AuthenticationDb");
builder.Services.AddDbContext<AuthenticationDbContext>(x => x.UseSqlServer(authConnectionString));
builder.Services.AddTransient<AuthenticationDbContext>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

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
