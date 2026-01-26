using BlogApp.API.Features.Blog;
using BlogApp.API.Features.Category;
using BlogApp.API.Options;
using BlogApp.API.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();
builder.Services.AddOptionExt();
builder.Services.AddRepositoryExt();

var app = builder.Build();

app.AddBlogGroupEndpointExt();
app.AddCategoryGroupEndpointExt();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.Run();
