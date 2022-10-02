using WebCalc.Blazor;
using Microsoft.AspNetCore.Components.Web;
using System.Buffers;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebCalc.Domain.BinaryOperation;
using WebCalc.Application.Contracts.BinaryOperation;
using WebCalc.Application.BinaryOperation;
using WebCalc.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<IBinaryOperationManager, BinaryOperationManager>();
builder.Services.AddSingleton<IBinaryOperationAppService, BinaryOperationAppService>();
builder.Services.AddSingleton<IBackNavigateable, NavigationHistoryStorage>();
builder.Services.AddSingleton<IBinaryOperationAppService, BinaryOperationAppService>();

await builder.Build().RunAsync(); 
