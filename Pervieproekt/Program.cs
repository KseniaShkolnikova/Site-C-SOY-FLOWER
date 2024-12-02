using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Pervieproekt.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<Pervieproekt.Models.SushiStoreContext>(option => 
option.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));



builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Autorization/Enter";
        options.AccessDeniedPath = "/Home/Index";
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CustomerPolicy", policy =>
        policy.RequireRole("Customer"));
});



var app = builder.Build();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Autorization}/{action=Enter}/{id?}");


app.Run();
