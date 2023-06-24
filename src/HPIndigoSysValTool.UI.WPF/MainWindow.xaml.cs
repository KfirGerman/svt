using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Radzen;
using HPIndigoSysValTool.SystemInfo;
using Microsoft.AspNetCore.Components.WebView.Wpf;
using Microsoft.Extensions.DependencyInjection;
using HPIndigoSysValTool.UI.Shared.ViewModels;
using Microsoft.Extensions.Hosting;
using HPIndigoSysValTool.Handlers.Logger;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.Web.WebView2.Core;
using Path = System.IO.Path;

namespace HPIndigoSysValTool.UI.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            try
            {
                LoggerProvider.FileLogger.Information("Application start");

                if (!IsWebView2RuntimeInstalled())
                {
                    MessageBox.Show("WebView2 runtime is not installed. Please install it to continue.", "WebView2 Runtime Missing", MessageBoxButton.OK, MessageBoxImage.Error);
                    Application.Current.Shutdown();
                    return;
                }

                InitializeComponent();


                this.MinWidth = SystemParameters.PrimaryScreenWidth * 0.8;
                this.MinHeight = SystemParameters.PrimaryScreenHeight * 0.8;

                var serviceProvider = ((App)Application.Current)._host.Services;
                BlazorWebViewControl.Services = serviceProvider;
            }
            catch (Exception ex)
            {
                LoggerProvider.FileLogger.Error(ex, "ExecutionFailed in MainWindow constructor");
                throw;
            }
        }

        private bool IsWebView2RuntimeInstalled()
        {
            try
            {
                CoreWebView2Environment.GetAvailableBrowserVersionString();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private async void webView_Loaded(object sender, RoutedEventArgs e)
        {
            await BlazorWebViewControl.WebView.EnsureCoreWebView2Async();

            var cwv = BlazorWebViewControl.WebView.CoreWebView2;

            cwv.WebResourceResponseReceived += (s, e) =>
            {
                if (e.Request.Uri?.StartsWith("data:") == false)
                {
                    Debug.WriteLine($"{e.Request.Method} {e.Request.Uri}: {e.Response.StatusCode}");
                    // log the request and response
                    LoggerProvider.FileLogger.Error($"{e.Request.Method} {e.Request.Uri}: {e.Response.StatusCode}");
                }
            };
        }
    }
}
