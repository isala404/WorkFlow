using System;
using System.Net.Http;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using WorkFlow.Client;
using WorkFlow.Client.Services;
using WorkFlow.Shared.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("WorkFlow.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("WorkFlow.ServerAPI"));

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<INavService, NavService>();
builder.Services.AddScoped<ITicket, TicketService>();
builder.Services.AddScoped<IUser, UserService>();
builder.Services.AddScoped<ICompany, CompanyService>();
builder.Services.AddScoped<IProject, ProjectService>();
builder.Services.AddApiAuthorization();

await builder.Build().RunAsync();
