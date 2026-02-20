using AutoMapper;
using BlogApp.API.Features.Auth;
using BlogApp.API.Features.Blog;
using BlogApp.API.Features.Category;
using BlogApp.API.Options;
using BlogApp.API.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var jwtSection = builder.Configuration.GetSection("JwtSettings");

builder.Services.Configure<JwtSettings>(jwtSection);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtSection["Issuer"],
            ValidAudience = jwtSection["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSection["SecretKey"]!))
        };
    });

builder.Services.AddAuthorization();

var loggerFactory = LoggerFactory.Create(builder => { });

var mapperConfig = new MapperConfiguration(
    cfg =>
    {
        cfg.AddMaps(typeof(Program).Assembly);
    },
    loggerFactory
);

builder.Services.AddSingleton(mapperConfig.CreateMapper());

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.AddOptionExt();
builder.Services.AddRepositoryExt();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.AddBlogGroupEndpointExt();
app.AddCategoryGroupEndpointExt();
app.AddAuthGroupEnpoint();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
