using Microsoft.EntityFrameworkCore;
using DAL.DbAccess;
using NLog.Web;
using FluentValidation.AspNetCore;
using Services.Validation.FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.Extensions.Configuration;
using ForumApi.Extensions;
using AutoMapper;
using Services;
using Services.Interfaces;
using Services.Services;
using DAL.Interfaces;
using DAL.Repositories;
using DAL;
using Microsoft.Extensions.Logging;
using Serilog;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddTransient<ExceptionMiddleware>();

var authOptions = builder.Configuration.GetSection("Auth");
builder.Services.Configure<JwtOptions>(authOptions);
var auth = authOptions.Get<JwtOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {

        ValidateIssuer = true,
        ValidIssuer = auth.Issuer,

        ValidateAudience = true,
        ValidAudience = auth.Audience,

        ValidateLifetime = true,

        IssuerSigningKey = auth.GetSymmetricSecurityKey(),
        ValidateIssuerSigningKey = true,

    };
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(build =>
    {
        build.AllowAnyOrigin().
        AllowAnyMethod().
        AllowAnyHeader();
    });
});

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
    fv.RegisterValidatorsFromAssemblyContaining<NicknameModelValidator>(lifetime: ServiceLifetime.Singleton);
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
builder.Services.AddTransient<IStatisticService, StatisticService>();

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

app.UseCors();

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

