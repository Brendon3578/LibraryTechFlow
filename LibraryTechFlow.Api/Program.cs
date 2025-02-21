using FluentValidation;
using LibraryTechFlow.Api.Filters;
using LibraryTechFlow.Api.Infrastructure.DataAccess;
using LibraryTechFlow.Api.Infrastructure.Security.Cryptography;
using LibraryTechFlow.Api.Infrastructure.Security.Tokens.Access;
using LibraryTechFlow.Api.UseCases.Login.DoLogin;
using LibraryTechFlow.Api.UseCases.Users.Register;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ExceptionFilter));
});

builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();


builder.Services.AddScoped<RegisterUserUseCase>();
builder.Services.AddScoped<DoLoginUseCase>();
builder.Services.AddScoped<BCryptAlgorithm>();
builder.Services.AddScoped<JwtTokenGenerator>();
builder.Services.AddValidatorsFromAssemblyContaining<RegisterUserValidator>();

var sqlLiteConnection = builder.Configuration["ConnectionStrings:SqlLiteDatabase"];

if (string.IsNullOrWhiteSpace(sqlLiteConnection))
    throw new InvalidOperationException("Database path is not configured properly.");

builder.Services.AddDbContext<LibraryTechFlowDbContext>(options =>
{
    options.UseSqlite(sqlLiteConnection);
});

var app = builder.Build();
;

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
