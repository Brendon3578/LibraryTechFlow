using FluentValidation;
using LibraryTechFlow.Api.Filters;
using LibraryTechFlow.Api.Infrastructure.DataAccess;
using LibraryTechFlow.Api.Infrastructure.Security.Cryptography;
using LibraryTechFlow.Api.Infrastructure.Security.Tokens.Access;
using LibraryTechFlow.Api.Services.LoggedUser;
using LibraryTechFlow.Api.UseCases.Books.Filter;
using LibraryTechFlow.Api.UseCases.Checkouts;
using LibraryTechFlow.Api.UseCases.Login.DoLogin;
using LibraryTechFlow.Api.UseCases.Users.Register;
using LibraryTechFlow.Security.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

const string AUTHENTICATION_TYPE = "Bearer";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ExceptionFilter>();
});

builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(AUTHENTICATION_TYPE, new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme.
                      Enter 'Bearer' [space] and then your token in the text input below;
                      Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = AUTHENTICATION_TYPE
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = AUTHENTICATION_TYPE
                },
                Scheme = "oauth2",
                Name = AUTHENTICATION_TYPE,
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});


var jwtKey = builder.Configuration["JwtSettings:Secret"]
        ?? throw new ArgumentNullException("Jwt Secret not found in appsettigns.json");

var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

builder.Services.AddScoped(x => new JwtTokenGenerator(symmetricKey));

builder.Services.AddScoped<RegisterUserUseCase>();
builder.Services.AddScoped<DoLoginUseCase>();
builder.Services.AddScoped<RegisterBookCheckoutUseCase>();
builder.Services.AddScoped<FilterBookUseCase>();

builder.Services.AddSingleton<ILogger<ExceptionFilter>, Logger<ExceptionFilter>>();


builder.Services.AddScoped<LoggedUserService>();

builder.Services.AddScoped<ICryptographyAlgorithm, BCryptAlgorithm>();
builder.Services.AddValidatorsFromAssemblyContaining<RegisterUserValidator>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = symmetricKey,
        };
    });

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
