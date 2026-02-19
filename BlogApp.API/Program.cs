using BlogApp.API.Features.Auth;
using BlogApp.API.Features.Blog;
using BlogApp.API.Features.Category;
using BlogApp.API.Options;
using BlogApp.API.Repository;
using BlogApp.API.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var jwtSettings = builder.Configuration.GetSection("JwtSettings");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings["Key"]))
    };
});

builder.Services.AddAuthorization();
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddSingleton<ITokenRevocationService, InMemoryTokenRevocationService>();

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
