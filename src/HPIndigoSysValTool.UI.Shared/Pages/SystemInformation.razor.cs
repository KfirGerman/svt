using HPIndigoSysValTool.SystemInfo.Components;
using HPIndigoSysValTool.UI.Shared.Services;
using HPIndigoSysValTool.UI.Shared.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using System;
using HPIndigoSysValTool.UI.Shared.Pages;

namespace HPIndigoSysValTool.UI.Pages
{
    public partial class SystemInformation : BasePage
    {
        protected override async Task OnInitializedAsync()
        {
            PageTitleService.UpdatePageTitle("System Information");
        }
        public void Dispose()
        {
            //JSRuntime.InvokeAsync<object>("setTitle", PageTitle);
        }

        [Parameter] public string HardwareName { get; set; }

    }
}