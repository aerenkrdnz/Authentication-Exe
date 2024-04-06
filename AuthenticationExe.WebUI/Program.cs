using AuthenticationExe.Business.Managers;
using AuthenticationExe.Business.Service;
using AuthenticationExe.Data.Context;
using AuthenticationExe.Data.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AuthenticationExeContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
builder.Services.AddScoped<IUserService,UserManager>();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = new PathString("/");
    options.LogoutPath = new PathString("/");
    options.AccessDeniedPath = new PathString("/");
});

var app = builder.Build();

app.UseAuthentication();

app.MapDefaultControllerRoute();

app.Run();
