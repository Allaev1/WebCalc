using WebCalc.Blazor;
using Microsoft.AspNetCore.Components.Web;
using System.Buffers;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebCalc.Domain.BinaryOperation;
using WebCalc.Application.Contracts.BinaryOperation;
using WebCalc.Application.BinaryOperation;
using WebCalc.Services;
using WebCalc.Application.Contracts.Services.Settings;
using WebCalc.Application.Services.Settings;
using WebCalc.Application.Contracts.Services.InputValidationService;
using WebCalc.Application.Services.InputValidationService;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<IBinaryOperationAppService, BinaryOperationAppService>();
builder.Services.AddSingleton<IBackNavigateable, NavigationHistoryStorage>();
builder.Services.AddSingleton<IBinaryOperationAppService, BinaryOperationAppService>();
builder.Services.AddSingleton<ISettings, FakeSettings>();
builder.Services.AddSingleton<IInputValidationService, InputValidationService>();

await builder.Build().RunAsync(); 
