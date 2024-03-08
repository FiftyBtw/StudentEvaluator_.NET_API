using API_Dto;
using Asp.Versioning;
using EF_StubbedContextLib;
using Entities2Dto;
using JsonSubTypes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Shared;
using TP_ConsoDev.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();

builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseInMemoryDatabase("AppDb"));

builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

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
            
    swaggerGenOptions.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });
    swaggerGenOptions.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

builder.Services.AddScoped<DbDataManager>(provider => new DbDataManager(new StubbedContext()));
builder.Services.AddScoped<IStudentService<StudentDto>>(x => x.GetRequiredService<DbDataManager>());
builder.Services.AddScoped<IGroupService<GroupDto>>(x => x.GetRequiredService<DbDataManager>());
builder.Services.AddScoped<ICriteriaService<CriteriaDto,TextCriteriaDto,SliderCriteriaDto,RadioCriteriaDto>>(x => x.GetRequiredService<DbDataManager>());
builder.Services.AddScoped<ILessonService<LessonDto, LessonReponseDto>>(x => x.GetRequiredService<DbDataManager>());
builder.Services.AddScoped<IUserService<UserDto,LoginRequestDto,LoginResponseDto>>(x => x.GetRequiredService<DbDataManager>());
builder.Services.AddScoped<ITemplateService<TemplateDto, TemplateResponseDto>>(x => x.GetRequiredService<DbDataManager>());
builder.Services.AddScoped<IEvaluationService<EvaluationDto,EvaluationReponseDto>>(x => x.GetRequiredService<DbDataManager>());

builder.Services.AddDbContext<StubbedContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services
    .AddApiVersioning(options =>
    {
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
        options.DefaultApiVersion = new ApiVersion( 1.0 );
    })
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
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

app.UseAuthorization();
        
app.MapIdentityApi<IdentityUser>();
app.MapSwagger().RequireAuthorization();

app.MapPost("/logout", async (SignInManager<IdentityUser> signInManager,
        [FromBody]object empty) =>
    {
        if (empty != null)
        {
            await signInManager.SignOutAsync();
            return Results.Ok();
        }
        return Results.Unauthorized();
    })
    .WithOpenApi()
    .RequireAuthorization();


var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<StubbedContext>();
context.Database.EnsureCreated();

app.Run();
