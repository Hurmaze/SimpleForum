using Microsoft.EntityFrameworkCore;
using DAL.DbAccess;
using NLog.Web;
using NLog;
using FluentValidation.AspNetCore;
using BLL.Validation.FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.Extensions.Configuration;
using ForumApi.Extensions;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(fv =>
{
    fv.RegisterValidatorsFromAssemblyContaining<RegistrationModelValidator>(lifetime: ServiceLifetime.Singleton);
    fv.RegisterValidatorsFromAssemblyContaining<AccountModelValidator>(lifetime: ServiceLifetime.Singleton);
    fv.RegisterValidatorsFromAssemblyContaining<ForumThreadModelValidator>(lifetime: ServiceLifetime.Singleton);
    fv.RegisterValidatorsFromAssemblyContaining<LoginModelValidator>(lifetime: ServiceLifetime.Singleton);
    fv.RegisterValidatorsFromAssemblyContaining<PostModelValidator>(lifetime: ServiceLifetime.Singleton);
    fv.RegisterValidatorsFromAssemblyContaining<RoleModelValidator>(lifetime: ServiceLifetime.Singleton);
    fv.RegisterValidatorsFromAssemblyContaining<ThemeModelValidator>(lifetime: ServiceLifetime.Singleton);
    fv.RegisterValidatorsFromAssemblyContaining<UserModelValidator>(lifetime: ServiceLifetime.Singleton);
});

var forumConnectionString = builder.Configuration.GetConnectionString("ForumDb");
builder.Services.AddDbContext<ForumDbContext>(x => x.UseSqlServer(forumConnectionString));
builder.Services.AddTransient<ForumDbContext>();

var authConnectionString = builder.Configuration.GetConnectionString("AuthenticationDb");
builder.Services.AddDbContext<AccountDbContext>(x => x.UseSqlServer(authConnectionString));
builder.Services.AddTransient<AccountDbContext>();

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionMiddleware>(logger);

app.Run();

