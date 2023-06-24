using HPIndigoSysValTool.SystemInfo.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using HPIndigoSysValTool.UI.Shared.Components;
using HPIndigoSysValTool.UI.Shared.Pages;
using HPIndigoSysValTool.UI.Shared.ViewModels;
using Radzen.Blazor;
using HPIndigoSysValTool.Validation;

namespace HPIndigoSysValTool.UI.Pages
{
    public partial class ComponentValidation : BasePage
    {

        protected MarkupString description;
        protected ElementReference linkRef;
        protected Dictionary<string, List<string>> availableTests;
        public event EventHandler<string> TestDescriptionChanged;
        protected MarkupString progress;

        protected List<string> ProgressMessages { get; set; } = new List<string>();
        [Inject] protected ValidationViewModel ValidationViewModel { get; set; }

        protected override async Task OnInitializedAsync()
        {
            PageTitleService.UpdatePageTitle("Validation");
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            availableTests = new Dictionary<string, List<string>>();
            availableTests = ValidationViewModel.GetAllAvailableTests();
            TestDescriptionChanged += UpdateTestDescription;

        }

        protected void ShowTooltip(ElementReference elementReference, string tooltipMessage, TooltipOptions options = null)
        {
            RenderFragment<Radzen.TooltipService> tooltipContent = (Radzen.TooltipService TooltipService) => builder =>
            {
                builder.AddMarkupContent(0, tooltipMessage);
            };
            TooltipService.Open(elementReference, tooltipContent, options);
        }

        protected void HideTooltip()
        {
            TooltipService.Close();
        }

        protected virtual void OnTestDescriptionChanged(string testDescription)
        {
            TestDescriptionChanged?.Invoke(this, testDescription);
        }
        public async Task ShowTestDescription(string selectedTest, string hardwareComponentName)
        {
            if (!string.IsNullOrEmpty(selectedTest))
            {
                var testDescription = ValidationViewModel.GetTestDescription(selectedTest, hardwareComponentName);
                OnTestDescriptionChanged(testDescription);
            }
        }
        private void UpdateTestDescription(object sender, string testDescription)
        {
            description = new MarkupString(testDescription);
            StateHasChanged();
        }
        public void Dispose()
        {
            //JSRuntime.InvokeAsync<object>("setTitle", PageTitle);
        }

        public async Task OpenTestDialog(string selectedTest, string selectedHardware)
        {
            await DialogService.OpenAsync<TestDialog>($"{selectedTest}",
                new Dictionary<string, object>() { { "selectedTest", selectedTest }, { "selectedHardware", selectedHardware } },
                new DialogOptions() { Width = "1024px", Height = "512px" });
        }


        //public async Task RunSelectedTest(string selectedTest, string selectedHardware)
        //{

        //    Debug.WriteLine($"Running test {selectedTest} on {selectedHardware}");
        //    if (!string.IsNullOrEmpty(selectedTest) && !string.IsNullOrEmpty(selectedHardware))
        //    {
        //        await ValidationViewModel.RunSpecificDiagnosticTestAsync(selectedTest, selectedHardware);
        //        NotificationService.Notify(new NotificationMessage
        //        {
        //            Severity = NotificationSeverity.Success,
        //            Summary = "Test Result",
        //            Detail = $"Test {selectedTest} on {selectedHardware} completed successfully.",
        //            Duration = 5000
        //        });
        //    }
        //    else
        //    {
        //        NotificationService.Notify(new NotificationMessage
        //        {
        //            Severity = NotificationSeverity.Warning,
        //            Summary = "Warning",
        //            Detail = "Please select a test and hardware from the list.",
        //        });
        //    }
        //}
    }
}
