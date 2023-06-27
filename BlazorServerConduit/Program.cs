
using BlazorServerConduit.Services;
using BlazorServerConduit.Store.Articles;
using Fluxor;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add Fluxor
var currentAssembly = typeof(ArticleState).Assembly;
builder.Services.AddFluxor(options => options.ScanAssemblies(currentAssembly));

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Forbidden/"; //TODO
    });

builder.Services.AddTransient<AuthenticationHttpMessageHandler>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient<ArticlesService>().AddHttpMessageHandler<AuthenticationHttpMessageHandler>();
builder.Services.AddHttpClient<TagService>().AddHttpMessageHandler<AuthenticationHttpMessageHandler>();
builder.Services.AddHttpClient<UserService>().AddHttpMessageHandler<AuthenticationHttpMessageHandler>();
builder.Services.AddHttpClient<ProfileService>().AddHttpMessageHandler<AuthenticationHttpMessageHandler>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
