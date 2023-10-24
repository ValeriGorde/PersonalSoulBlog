using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PersonalSoulBlog.DAL.Data;
using PersonalSoulBlog.DAL.Data.DefaultData;
using PersonalSoulBlog.DAL.Models.Entities;
using PersonalSoulBlog.DAL.Models.Repositories.Interfaces;
using PersonalSoulBlog.DAL.Models.Repositories;
using PersonalSoulBlog.Services.Contracts;
using PersonalSoulBlog.Services.Contracts.Interfaces;
using PersonalSoulBlog.Services.ControllersServices;
using PersonalSoulBlog.Services.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

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
    .AddScoped<IArticleService, ArticleService>();

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

var app = builder.Build();

// Добавление пользователей и ролей по умолчанию в БД
var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
await SeedData.EnsureSeedData(scope.ServiceProvider);


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Добавление аутентификации
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Article}/{action=Index}/{id?}");

app.Run();
