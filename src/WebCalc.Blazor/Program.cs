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
using WebCalc.Domain.Repositories;
using WebCalc.Domain.Constant.DomainManager;
using WebCalc.Application.Contracts.Constants;
using WebCalc.Application.Constant;
using Syncfusion.Blazor;
using WebCalc.IndexedDbStorage.Constant.Repostiory;
using DnetIndexedDb;
using WebCalc.IndexedDbStorage.Data;
using DnetIndexedDb.Models;
using DnetIndexedDb.Fluent;
using WebCalc.Domain.Constant.Proxy;
using Blazored.LocalStorage;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<IBinaryOperationAppService, BinaryOperationAppService>();
builder.Services.AddSingleton<IBackNavigateable, NavigationHistoryStorage>();
builder.Services.AddSingleton<IBinaryOperationAppService, BinaryOperationAppService>();
builder.Services.AddSingleton<IInputValidationService, InputValidationService>();
builder.Services.AddScoped<IRepository<ConstantProxy>, IndexedDbRepository<ConstantProxy>>();
builder.Services.AddScoped<IConstantManager, ConstantManager>();
builder.Services.AddScoped<IConstantAppService, ConstantAppService>();
builder.Services.AddSingleton<ISettings, Settings>();
builder.Services.AddIndexedDbDatabase<WebCalcDb>(options =>
{
    var model = new IndexedDbDatabaseModel()
        .WithName("WebCalc")
        .WithVersion(1)
        .WithModelId(0);

    model.AddStore("constants")
        .WithKey("id")
        .AddUniqueIndex("id")
        .AddIndex("name")
        .AddIndex("value")
        .AddIndex("description");

    options.UseDatabase(model);
});
builder.Services.AddSyncfusionBlazor();
builder.Services.AddBlazoredLocalStorageAsSingleton();

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NzU4MjY3QDMyMzAyZTMzMmUzMFdjMHZpbTNYUHRRTXlUU1RZVjRiZStUQThnVEl4MWZYbm1DeDJzRGs1MmM9");

await builder.Build().RunAsync();
