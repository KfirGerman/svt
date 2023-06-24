namespace HPIndigoSysValTool.UI.Shared.Services;

public class PageTitleService
{
    public string PageTitle { get; private set; }

    public event Action OnPageTitleChanged;

    public void UpdatePageTitle(string newTitle)
    {
        PageTitle = newTitle;
        NotifyPageTitleChanged();
    }

    private void NotifyPageTitleChanged() => OnPageTitleChanged?.Invoke();
}
