using HPIndigoSysValTool.UI.Shared.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System;
using HPIndigoSysValTool.Handlers.Logger;
using Serilog.Core;

namespace HPIndigoSysValTool.UI.Shared.Components
{
    public partial class TestDialog : ComponentBase
    {
        [Inject] protected IJSRuntime JSRuntime { get; set; }

        [Inject] protected NavigationManager NavigationManager { get; set; }

        [Inject] protected Radzen.DialogService DialogService { get; set; }

        [Inject] protected TooltipService TooltipService { get; set; }

        [Inject] protected ContextMenuService ContextMenuService { get; set; }

        [Inject] protected NotificationService NotificationService { get; set; }

        [Inject] protected ValidationViewModel ValidationViewModel { get; set; }

        protected MarkupString progress;

        protected List<string> ProgressMessages { get; set; } = new List<string>();

        [Parameter] public string selectedTest { get; set; }
        [Parameter] public string selectedHardware { get; set; }
        protected int ProgressBarPercentage { get; set; } = 0;

        protected string TestStatus = "Ready";


        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await RunSelectedTest();
        }

        #region Test Section

        public async Task RunSelectedTest()
        {
            Debug.WriteLine($"Running test {selectedTest} on {selectedHardware}");
            LoggerProvider.FileLogger.Information($"Running test {selectedTest} on {selectedHardware}");

            if (!string.IsNullOrEmpty(selectedTest) && !string.IsNullOrEmpty(selectedHardware))
            {
                var progress = new Progress<(int, string)>(UpdateUI);
                await ValidationViewModel.RunSpecificDiagnosticTestAsync(
                    selectedTest,
                    selectedHardware,
                    progress,
                    HandleDiagnosticTestError);
            }
            else
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Warning,
                    Summary = "Warning",
                    Detail = "Please select a test and hardware from the list.",
                });
            }
        }

        private void HandleDiagnosticTestError(string errorMessage)
        {
            NotificationService.Notify(new NotificationMessage
            {
                Severity = NotificationSeverity.Error,
                Summary = "Error",
                Detail = errorMessage,
            });
        }


        private void UpdateUI((int, string) progressData)
        {
            ProgressBarPercentage = progressData.Item1;
            string message = progressData.Item2;

            ProgressMessages.Add(message);
            InvokeAsync(StateHasChanged);
        }




        // Add a method to update the progress messages and trigger a UI update
        private void UpdateProgressMessage(string message)
        {
            progress = new MarkupString(message);
            InvokeAsync(StateHasChanged);
        }

        #endregion Test Section
    }
}
