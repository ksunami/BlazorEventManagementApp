
using System;                   
using System.Net.Http; 
using BlazorEventManagementApp;
using BlazorEventManagementApp.Data;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<UserSessionService>();
builder.Services.AddScoped<ToastService>();

var app = builder.Build();

var session = app.Services.GetRequiredService<UserSessionService>();
await session.InitializeAsync();

builder.Logging.SetMinimumLevel(LogLevel.Debug);
builder.Logging.AddFilter("Microsoft.AspNetCore.Components", LogLevel.Debug);
builder.Logging.AddFilter("Microsoft.AspNetCore.Components.Web", LogLevel.Debug);
builder.Logging.AddFilter("System.Net.Http", LogLevel.Warning);

using (var scope = app.Services.CreateScope())
{
    var eventsSvc = scope.ServiceProvider.GetRequiredService<EventService>();
    await eventsSvc.InitializeAsync();
}

await app.RunAsync();
