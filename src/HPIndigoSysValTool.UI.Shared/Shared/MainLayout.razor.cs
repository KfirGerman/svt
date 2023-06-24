using HPIndigoSysValTool.UI.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Radzen;
using System.Diagnostics;

namespace HPIndigoSysValTool.UI.Shared.Shared;

public partial class MainLayout
{
    [Inject] protected IJSRuntime JSRuntime { get; set; }

    [Inject] protected NavigationManager NavigationManager { get; set; }

    [Inject] protected DialogService DialogService { get; set; }

    [Inject] protected TooltipService TooltipService { get; set; }

    [Inject] protected ContextMenuService ContextMenuService { get; set; }

    [Inject] protected NotificationService NotificationService { get; set; }

    [Inject] protected PageTitleService PageTitleService { get; set; }
    private string _pageTitle = "Default Title";
    public string PageTitle { get => _pageTitle; set => _pageTitle = value; }


    protected override void OnInitialized()
    {
        PageTitleService.OnPageTitleChanged += OnPageTitleChanged;
    }

    private void OnPageTitleChanged()
    {
        PageTitle = PageTitleService.PageTitle;
        InvokeAsync(StateHasChanged);
    }

    public void Dispose()
    {
        PageTitleService.OnPageTitleChanged -= OnPageTitleChanged;
    }


}