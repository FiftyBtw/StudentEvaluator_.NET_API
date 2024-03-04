using API_Dto;
using EF_DbContextLib;
using EF_Entities;
using EF_StubbedContextLib;
using Entities2Dto;
using JsonSubTypes;
using Microsoft.EntityFrameworkCore;
using Shared;

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
builder.Services.AddSwaggerGen(swaggerGenOptions =>
{
    swaggerGenOptions.UseAllOfToExtendReferenceSchemas();
    swaggerGenOptions.UseAllOfForInheritance();
    swaggerGenOptions.UseOneOfForPolymorphism();
    
    swaggerGenOptions.SelectSubTypesUsing(baseType =>
    {
        return typeof(CriteriaDto).Assembly.GetTypes().Where(type => type.IsSubclassOf(baseType));
    });
});
builder.Services.AddScoped<DbDataManager>(provider => new DbDataManager(new StubbedContext()));
builder.Services.AddScoped<IStudentService<StudentDto>>(x => x.GetRequiredService<DbDataManager>());
builder.Services.AddScoped<IGroupService<GroupDto>>(x => x.GetRequiredService<DbDataManager>());
builder.Services.AddScoped<ICriteriaService<CriteriaDto>>(x => x.GetRequiredService<DbDataManager>());
builder.Services.AddScoped<ILessonService<LessonDto>>(x => x.GetRequiredService<DbDataManager>());
builder.Services.AddScoped<IUserService<UserDto>>(x => x.GetRequiredService<DbDataManager>());
builder.Services.AddScoped<ITemplateService<TemplateDto>>(x => x.GetRequiredService<DbDataManager>());
builder.Services.AddScoped<IEvaluationService<EvaluationDto>>(x => x.GetRequiredService<DbDataManager>());

builder.Services.AddDbContext<StubbedContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
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
