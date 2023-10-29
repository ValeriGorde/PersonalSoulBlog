using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using PersonalSoulBlog.BLL.Services.Contracts.Interfaces;
using PersonalSoulBlog.BLL.Services.Contracts;
using PersonalSoulBlog.BLL.Services.ControllersServices;
using PersonalSoulBlog.DAL.Data;
using PersonalSoulBlog.DAL.Models.Entities;
using PersonalSoulBlog.DAL.Models.Repositories.Interfaces;
using PersonalSoulBlog.DAL.Models.Repositories;
using PersonalSoulBlog.BLL.Services.Mapping;
using Microsoft.OpenApi.Models;
using System.Reflection;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init PesonalSoulBlog.API");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Добавление контекста для связи с БД
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));

    // Регистрация зависимостей репозиториев
    builder.Services
        .AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>))
        .AddScoped<ITagRepository, TagRepository>()
        .AddScoped<IArticleRepository, ArticleRepository>()
        .AddScoped<ICommentRepository, CommentRepository>();


    // Подключаем сервисы
    builder.Services
        .AddScoped<IAccountService, AccountService>()
        .AddScoped<IRoleService, RoleService>()
        .AddScoped<IUserService, UserService>()
        .AddScoped<ITagService, TagService>()
        .AddScoped<IArticleService, ArticleService>()
        .AddScoped<ICommentService, CommentService>();

    // Подключаем маппинг
    var mappingConfig = new MapperConfiguration(mc =>
    {
        mc.AddProfile(new MappingProfile());
    });
    IMapper mapper = mappingConfig.CreateMapper();
    builder.Services.AddSingleton(mapper);

    // Добавление сервисов Identity
    builder.Services.AddIdentity<User, Role>(opts =>
    {
        opts.Password.RequiredLength = 5;
        opts.Password.RequireNonAlphanumeric = false;
        opts.Password.RequireLowercase = false;
        opts.Password.RequireUppercase = false;
        opts.Password.RequireDigit = false;
    }).AddRoles<Role>()
    .AddRoleManager<RoleManager<Role>>()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>();

    // Документация Swagger
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "PersonalSoulBlog API",
            Description = "ASP.NET Core WEB.API для написания личных блогов"
        });

        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
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

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex);
}
finally
{
    LogManager.Shutdown();
}

