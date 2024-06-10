using ServicesProvider.Persistence;
using Microsoft.EntityFrameworkCore;
using ServicesProvider.Application.Services;
using Microsoft.OpenApi.Models;
using ServicesProvider.Application.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.CookiePolicy;
using ServicesProvider.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JwtOptions:SecretKey")))
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies["bearer"];
                return Task.CompletedTask;
            }

        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ProviderPolicy", policy =>
    {
        policy.AddRequirements(new UserRoleRequirement() { 
            UserRole = UserRole.Provider
        });
    });

    options.AddPolicy("ClientPolicy", policy =>
    {
        policy.AddRequirements(new UserRoleRequirement()
        {
            UserRole = UserRole.Client
        });
    });
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen();

// получаем строку подключения из файла конфигурации
string connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(
    options =>
    {
        options.UseMySql(connection, ServerVersion.AutoDetect(connection));
    });



builder.Services.AddScoped<IRequestsService, RequestsService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IServicesService, ServicesService>();

builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

builder.Services.AddSingleton<IAuthorizationHandler, UserAuthorizationHandler>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
