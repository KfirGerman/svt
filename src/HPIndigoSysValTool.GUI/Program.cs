using System;
using System.Threading.Tasks;
using HPIndigoSysValTool.SystemInfo;
using HPIndigoSysValTool.UI.Shared.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Radzen;
using HPILogger = HPIndigoSysValTool.Handlers.Logger;
using HPIndigoSysValTool.Validation;
using HPIndigoSysValTool.UI.Shared.Shared;
using Serilog;
using HPIndigoSysValTool.UI.Shared.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigureServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
ConfigureMiddleware(app);

app.Run();

void ConfigureServices(IServiceCollection services)
{
    services.AddRazorPages();
    services.AddServerSideBlazor();
    services.AddScoped<DialogService>();
    services.AddScoped<NotificationService>();
    services.AddScoped<TooltipService>();
    services.AddScoped<ContextMenuService>();
    services.AddSingleton<PageTitleService>();

    #region HPIndigoSysValTool.SystemInfo services
    services.AddSingleton<ISysInfo>(SysInfo.Instance);
    #endregion

    #region HPIndigoSysValTool.Handlers services
    services.AddSingleton<HPILogger.ILoggerFactory, HPILogger.LoggerFactory>();
    services.AddSingleton<HPILogger.FileLogger>(HPILogger.FileLogger.Instance);
    services.AddSingleton<HPILogger.HtmlLogger>(HPILogger.HtmlLogger.Instance);
    services.AddSingleton<HPILogger.JsonLogger>(HPILogger.JsonLogger.Instance);
    #endregion

    #region HPIndigoSysValTool.UI.Shared services
    services.AddScoped<SystemInformationViewModel>();
    services.AddScoped<SettingsViewModel>();
    services.AddScoped<MainViewModel>();
    services.AddScoped<MainLayout>();
    #endregion
    services.AddLogging(loggingBuilder =>
    {
        loggingBuilder.ClearProviders(); // Clear existing providers
        loggingBuilder.AddSerilog(HPILogger.FileLogger.Instance, dispose: true);
    });

    ConfigurePressMachine(services);

}


void ConfigurePressMachine(IServiceCollection services)
{
    PressConfiguration pressConfiguration = PressConfiguration.Instance;
    services.AddSingleton<PressConfiguration>(pressConfiguration);

    AbstractPressMachine pressMachine = CreatePressMachine(pressConfiguration.Series);
    services.AddSingleton<AbstractPressMachine>(pressMachine);

    // Send the press machine to the validation view model
    services.AddSingleton<ValidationViewModel>(sp => new ValidationViewModel(pressMachine, pressConfiguration));
}

// Method to create a PressMachine instance based on the provided series
AbstractPressMachine CreatePressMachine(string series)
{
    PressMachineSeriesFactory pressMachineSeriesFactory;
    if (string.IsNullOrEmpty(series)) { series = "Series3"; }

    switch (series)
    {
        case "Series3":
            pressMachineSeriesFactory = new PressMachineSeries3Factory();
            break;
        case "Series4":
            pressMachineSeriesFactory = new PressMachineSeries4Factory();
            break;
        case "Series5":
            pressMachineSeriesFactory = new PressMachineSeries5Factory();
            break;
        case "Series6":
            pressMachineSeriesFactory = new PressMachineSeries6Factory();
            break;
        default:
            throw new ArgumentException($"Invalid press series: {series}");
    }

    return pressMachineSeriesFactory.CreatePressInstance();
}

// Method to configure the middleware
void ConfigureMiddleware(WebApplication app)
{
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/ExecutionFailed");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();
    app.MapControllers();
    app.MapBlazorHub();
    app.MapFallbackToPage("/_Host");
}