using API_Dto;
using EF_DbContextLib;
using EF_StubbedContextLib;
using Entities2Dto;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<DbDataManager>(provider => new DbDataManager(new StubbedContext()));
builder.Services.AddScoped<IStudentService>(x => x.GetRequiredService<DbDataManager>());
builder.Services.AddScoped<IGroupService>(x => x.GetRequiredService<DbDataManager>());
builder.Services.AddScoped<ICriteriaService>(x => x.GetRequiredService<DbDataManager>());
builder.Services.AddScoped<ILessonService>(x => x.GetRequiredService<DbDataManager>());
builder.Services.AddScoped<IUserService>(x => x.GetRequiredService<DbDataManager>());
builder.Services.AddScoped<ITemplateService>(x => x.GetRequiredService<DbDataManager>());

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

var scope = app.Services.CreateScope();


app.Run();
