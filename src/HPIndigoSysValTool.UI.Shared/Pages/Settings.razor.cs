using HPIndigoSysValTool.UI.Shared.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using HPIndigoSysValTool.UI.Shared.Shared;
using HPIndigoSysValTool.UI.Shared.Pages;

namespace HPIndigoSysValTool.UI.Pages
{
    public partial class Settings : BasePage
    {

        [Inject] public SettingsViewModel? settingsViewModel { get; set; }

        protected override async Task OnInitializedAsync()
        {
            PageTitleService.UpdatePageTitle("Settings");
        }
        public void Dispose()
        {
            //JSRuntime.InvokeAsync<object>("setTitle", PageTitle);
        }

        // set PressSeries and PressModel from the ViewModel
        protected string? PressSeries
        {
            get => settingsViewModel!.PressSeries;
            set => settingsViewModel!.PressSeries = value;
        }

        protected string? PressModel
        {
            get => settingsViewModel!.PressModel;
            set => settingsViewModel!.PressModel = value;
        }


        protected async Task SaveSettings()
        {
            PressSeries = selectedSeries;
            PressModel = selectedPress;
            await settingsViewModel!.SaveSettings();
            NotificationService.Notify(new NotificationMessage { 
                Severity = NotificationSeverity.Success, 
                Summary = "Settings saved!", 
                Detail = $"{PressSeries}, {PressModel}", 
                Duration = 8000 });
        }

        protected string selectedSeries = "Series 3";
        protected string selectedPress = "HP Indigo Digital Press 6K";

        protected IEnumerable<string> series = new List<string> { "Series 3", "Series 4", "Series 5", "Series 6" };
        protected IEnumerable<string> press = new List<string>
        {
            "HP Indigo Digital Press 6K", "HP Indigo Digital Press 7K", "HP Indigo Digital Press 8K", "HP Indigo Digital Press 7ECO", // Series 3
        };

        protected void OnSeriesChanged(string selectedOption)
        {
            switch (selectedOption)
            {
                case "Series 3":
                    press = new List<string> { "HP Indigo Digital Press 6K", "HP Indigo Digital Press 7K", "HP Indigo Digital Press 8K", "HP Indigo Digital Press 7ECO" };
                    break;
                case "Series 4":
                    press = new List<string> { "HP Indigo Digital Press 15K", "HP Indigo Digital Press 15K HD", "HP Indigo Digital Press 25K", "HP Indigo Digital Press 35K" };
                    break;
                case "Series 5":
                    press = new List<string> { "HP Indigo Digital Press 100K", "HP Indigo Digital Press 100K HD", "HP Indigo Digital Press 200K" };
                    break;
                case "Series 6":
                    press = new List<string> { "HP Indigo Digital Press V12" };
                    break;
            }
            selectedSeries = selectedOption;
            selectedPress = press.FirstOrDefault();

            Debug.WriteLine($"selectedSeries - {selectedSeries}, selectedPress - {selectedPress}");
        }

    }
}