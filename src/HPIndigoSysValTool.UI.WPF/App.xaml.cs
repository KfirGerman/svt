using Microsoft.Extensions.DependencyInjection;
using Radzen;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using HPIndigoSysValTool.SystemInfo;
using HPIndigoSysValTool.UI.Shared.Shared;
using HPIndigoSysValTool.UI.Shared.ViewModels;
using Microsoft.AspNetCore.Components.WebView.Wpf;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebView;
using HPILogger = HPIndigoSysValTool.Handlers.Logger;
using Serilog.Events;
using HPIndigoSysValTool.Validation;
using HPIndigoSysValTool.Handlers.Logger;
using Serilog;
using Microsoft.Extensions.Logging;
using System.Reflection;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Builder;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using HPIndigoSysValTool.UI.Shared.Services;

namespace HPIndigoSysValTool.UI.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider Services { get; private set; } = default!;
        public readonly IHost _host;
        public App()
        {
            try
            {
                _host = Host.CreateDefaultBuilder()
                    .ConfigureServices((context, services) =>
                    {
                        ConfigureServices(services);
                        ConfigurePressMachine(services);
                    })
                    .Build();

                Services = _host.Services;
            }
            catch (Exception ex)
            {
                LoggerProvider.FileLogger.Error(ex, "ExecutionFailed in App constructor");
                throw;
            }
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            try
            {
                base.OnStartup(e);
                await _host.StartAsync();
            }
            catch (Exception ex)
            {
                LoggerProvider.FileLogger.Error(ex, "ExecutionFailed in OnStartup");
                MessageBox.Show("An error occurred during startup. Please check the log for more details.", "Startup ExecutionFailed", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown();
            }
        }


        private AbstractPressMachine CreatePressMachine(string series)
        {
            try
            {
                PressMachineSeriesFactory pressMachineSeriesFactory;
                if (string.IsNullOrEmpty(series))
                {
                    series = "Series3";
                }

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
            catch (Exception ex)
            {
                LoggerProvider.FileLogger.Error(ex, "ExecutionFailed in CreatePressMachine");
                throw;
            }
        }

        private void ConfigurePressMachine(IServiceCollection services)
        {
            try
            {
                PressConfiguration pressConfiguration = PressConfiguration.Instance;
                services.AddSingleton<PressConfiguration>(pressConfiguration);

                AbstractPressMachine pressMachine = CreatePressMachine(pressConfiguration.Series);
                //pressMachine.InitializePressValidation();
                services.AddSingleton<AbstractPressMachine>(pressMachine);

                // Send the press machine to the validation view model
                services.AddSingleton<ValidationViewModel>(sp =>
                    new ValidationViewModel(pressMachine, pressConfiguration));

                LoggerProvider.FileLogger.Information("Press Configuration Services Added Successfully");

            }
            catch (Exception ex)
            {
                LoggerProvider.FileLogger.Error(ex, "ExecutionFailed in ConfigurePressMachine");
                throw;
            }
        }

private void ConfigureServices(IServiceCollection services)

        {
            try
            {
                services.AddWpfBlazorWebView();

                services.AddRazorPages();
                services.AddScoped<DialogService>();
                services.AddScoped<NotificationService>();
                services.AddScoped<TooltipService>();
                services.AddScoped<Radzen.ContextMenuService>();

                #region HPIndigoSysValTool.SystemInfo services

                services.AddSingleton<ISysInfo, SysInfo>();

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

#if DEBUG
                services.AddBlazorWebViewDeveloperTools();
#endif

                services.AddLogging(loggingBuilder =>
                {
                    loggingBuilder.ClearProviders(); // Clear existing providers
                    loggingBuilder.AddSerilog(HPILogger.BlazorLogger.Instance, dispose: true);
                });

                services.Configure<StaticFileOptions>(options =>
                {
                    options.FileProvider = new PhysicalFileProvider(Path.Combine(AppContext.BaseDirectory, "wwwroot"));
                });

                services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("/") });
                services.AddSingleton<PageTitleService>();


                LoggerProvider.FileLogger.Information("Basic Services Added Successfully");

            }
            catch (Exception ex)
            {
                LoggerProvider.FileLogger.Error(ex, "ExecutionFailed in ConfigureServices");
                throw;
            }
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            using (_host)
            {
                await _host.StopAsync(TimeSpan.FromSeconds(5));
            }
        }
    }
}