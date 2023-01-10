using Better.Injection;
using Better.Persistence;
using Better.Tools.Configuration;
using Better.Tools.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(config =>
{
    var policy = new AuthorizationPolicyBuilder()
                 .RequireAuthenticatedUser()
                 .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (Constants.UseLogger)
{
    builder.Host.ConfigureLogging(builder =>
    {
        builder.ClearProviders().AddColorConsoleLogger();
    });
}

builder.Services.AddDbContext<BetterContext>(options =>
{
    options.UseSqlServer(Constants.ConnectionString);
    options.LogTo(Console.WriteLine, Constants.DbLogLevel);
    options.EnableSensitiveDataLogging(Constants.EnableSensitiveLogging);
});
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    options.SignIn.RequireConfirmedAccount = Constants.RequireEmailConfirmation)
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<BetterContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(Constants.LogCacheOut);

    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();

builder.Services.RegisterServices();
builder.Services.RegisterIdentityConfiguration();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseForwardedHeaders();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.MapRazorPages();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
