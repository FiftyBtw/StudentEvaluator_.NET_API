using API_Dto;
using EF_StubbedContextLib;
using Entities2Dto;

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
