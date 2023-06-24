using HPIndigoSysValTool.UI.Shared.Services;
using HPIndigoSysValTool.UI.Shared.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPIndigoSysValTool.UI.Shared.Pages
{
    public abstract class BasePage : ComponentBase, IDisposable
    {
        [Inject] protected IJSRuntime JSRuntime { get; set; }

        [Inject] protected NavigationManager NavigationManager { get; set; }

        [Inject] protected DialogService DialogService { get; set; }

        [Inject] protected TooltipService TooltipService { get; set; }

        [Inject] protected ContextMenuService ContextMenuService { get; set; }

        [Inject] protected NotificationService NotificationService { get; set; }

        [Inject] protected PageTitleService PageTitleService { get; set; }


        public event EventHandler<string> TitleChanged;

        public async Task SetPageTitle(string title)
        {
            Debug.WriteLine($"Setting page title to: {title}");
            if (PageTitleService != null)
            {
                PageTitleService.UpdatePageTitle(title);
                await InvokeAsync(StateHasChanged);
            }
        }

        public virtual void Dispose()
        {
            // You can add common disposing logic here if needed.
        }
    }
}