using Microsoft.EntityFrameworkCore;
using DAL.DbAccess;
using NLog.Web;
using FluentValidation.AspNetCore;
using BLL.Validation.FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.Extensions.Configuration;
using ForumApi.Extensions;
using AutoMapper;
using BLL;
using BLL.Interfaces;
using BLL.Services;
using DAL.Interfaces;
using DAL.Repositories;
using DAL;
using Microsoft.Extensions.Logging;
using Serilog;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddTransient<ExceptionMiddleware>();
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
ValidatorOptions.Global.LanguageManager.Enabled = false;

builder.Services.AddTransient<IAccountRepository, AccountRepository>();
builder.Services.AddTransient<IRoleRepository, RoleRepository>();
builder.Services.AddTransient<IThemeRepository, ThemeRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IForumThreadRepository, ForumThreadRepository>();
builder.Services.AddTransient<IPostRepository, PostRepository>();

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddTransient<IUserAccountService, UserAccountService>();
builder.Services.AddTransient<IPostService, PostService>();
builder.Services.AddTransient<IForumThreadService, ForumThreadService>();

var forumConnectionString = builder.Configuration.GetConnectionString("ForumDb");
builder.Services.AddDbContext<ForumDbContext>(x => x.UseSqlServer(forumConnectionString));
builder.Services.AddTransient<ForumDbContext>();

var authConnectionString = builder.Configuration.GetConnectionString("AuthenticationDb");
builder.Services.AddDbContext<AccountDbContext>(x => x.UseSqlServer(authConnectionString));
builder.Services.AddTransient<AccountDbContext>();

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new AutomapperProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

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

app.UseMiddleware<ExceptionMiddleware>();

app.Run();

