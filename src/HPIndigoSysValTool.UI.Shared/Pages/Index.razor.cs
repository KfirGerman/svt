using HPIndigoSysValTool.UI.Shared.Shared;
using HPIndigoSysValTool.UI.Shared.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Radzen;

namespace HPIndigoSysValTool.UI.Shared.Pages
{
    public partial class Index : BasePage
    {
        [Inject] public MainViewModel? mainViewModel { get; set; }

        public override void Dispose()
        {
            //JSRuntime.InvokeAsync<object>("setTitle", PageTitle);
            // You can add additional disposing logic specific to the Index class here if needed.

        }

        protected override async Task OnInitializedAsync()
        {
            PageTitleService.UpdatePageTitle("Main");
        }


        // create a method to return mainViewModel.PressConfiguration.Model
        protected string? PressModel
        {
            get => mainViewModel!.PressConfiguration.Model;
        }

        // create a method to return mainViewModel.PressConfiguration.Series
        protected string? PressSeries
        {
            get => mainViewModel!.PressConfiguration.Series;
        }

    }
}