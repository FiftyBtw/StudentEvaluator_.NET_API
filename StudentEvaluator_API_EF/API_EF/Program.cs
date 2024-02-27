using API_Dto;
using EF_DbContextLib;
using EF_Entities;
using EF_StubbedContextLib;
using Entities2Dto;
using JsonSubTypes;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.Converters.Add(
        JsonSubtypesConverterBuilder
            .Of(typeof(CriteriaDto), "criteriaType")
            .RegisterSubtype(typeof(TextCriteriaDto), "text")
            .RegisterSubtype(typeof(SliderCriteriaDto), "slider")
            .RegisterSubtype(typeof(RadioCriteriaDto), "radio")
            .SerializeDiscriminatorProperty()
            .Build()
        );
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.UseAllOfToExtendReferenceSchemas();
    c.UseAllOfForInheritance();
    c.UseOneOfForPolymorphism();
    c.SelectDiscriminatorNameUsing(baseType =>
    {
        if (baseType == typeof(CriteriaDto))
        {
            return "criteriaType";
        }
        return null;
    });
});
builder.Services.AddScoped<DbDataManager>(provider => new DbDataManager(new StubbedContext()));
builder.Services.AddScoped<IStudentService>(x => x.GetRequiredService<DbDataManager>());
builder.Services.AddScoped<IGroupService>(x => x.GetRequiredService<DbDataManager>());
builder.Services.AddScoped<ICriteriaService>(x => x.GetRequiredService<DbDataManager>());
builder.Services.AddScoped<ILessonService>(x => x.GetRequiredService<DbDataManager>());
builder.Services.AddScoped<IUserService>(x => x.GetRequiredService<DbDataManager>());
builder.Services.AddScoped<ITemplateService>(x => x.GetRequiredService<DbDataManager>());
builder.Services.AddScoped<IEvaluationService>(x => x.GetRequiredService<DbDataManager>());

builder.Services.AddDbContext<StubbedContext>(options =>
{
    options.UseSqlite("Data Source=StudentEvaluator_API_EF.db");
});

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
var context = scope.ServiceProvider.GetRequiredService<StubbedContext>();
context.Database.EnsureCreated();

app.Run();
