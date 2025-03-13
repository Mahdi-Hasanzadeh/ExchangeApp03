using Authentication.Client.ClientAuthentication;
using Authentication.Client.Repository.Interface;
using Authentication.Client.Repository.Services;
using Authentication.Components;
using Authentication.ServerAuthentication;
using Blazored.Toast;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Models;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
                .AddInteractiveWebAssemblyComponents()
                .AddInteractiveServerComponents();

builder.Services.AddBlazoredToast();

builder.Services.AddScoped(sp =>
new HttpClient { BaseAddress = new Uri(Utility.API_URI) });

builder.Services.AddScoped<HttpClientManager>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<ClientAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<ClientAuthenticationStateProvider>());



// We should add These Three Important services


builder.Services.AddLocalization();

builder.Services.AddControllers();


//builder.Services.AddDefaultIdentity<IdentityUser>()
//    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddAuthentication(AuthConstant.Scheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = AuthConstant.CookieName;
        options.LoginPath = "/login";
        options.LogoutPath = "/logout";
        options.Cookie.MaxAge = TimeSpan.FromHours(AuthConstant.Time);
        options.AccessDeniedPath = "/access-denied";
    });



builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, CustomServerAuthenticationProvider>();

//builder.Services.AddDbContext<AppDbContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalCon"));
//});

// End of Services


var app = builder.Build();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Lax, // Allows cookies for OAuth2
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always // Ensures cookies are only sent over HTTPS
});

app.UseRequestLocalization(new RequestLocalizationOptions()
    .SetDefaultCulture("fa")
    .AddSupportedCultures(new[] { "en-US", "fa" })
    .AddSupportedUICultures(new[] { "en-US", "fa" }));


//We should Map these middleware
app.UseAuthentication();
app.UseAuthorization();
//End of middleware


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapControllers();


app.MapRazorComponents<App>()
               .AddInteractiveWebAssemblyRenderMode()
               .AddInteractiveServerRenderMode()
               .AddAdditionalAssemblies(typeof(Authentication.Client._Imports).Assembly);

app.Run();
