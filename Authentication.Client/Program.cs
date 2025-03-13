using Authentication.Client.ClientAuthentication;
using Authentication.Client.Repository.Interface;
using Authentication.Client.Repository.Services;
using Blazored.Toast;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Shared;

var builder = WebAssemblyHostBuilder.CreateDefault(args);


builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddLocalization();
builder.Services.AddBlazoredToast();



//builder.Services.AddScoped<AuthenticationStateProvider, ClientAuthenticationStateProvider>();
builder.Services.AddScoped<ClientAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<ClientAuthenticationStateProvider>());

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(Utility.API_URI) });

builder.Services.AddScoped<HttpClientManager>();

builder.Services.AddScoped<IUserRepository, UserRepository>();


var host = builder.Build();

//await Services.SetCulture(host);

// Run the application
await host.RunAsync();




