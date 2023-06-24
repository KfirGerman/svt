using Microsoft.AspNetCore.Components;

namespace HPIndigoSysValTool.UI.Shared.Components
{
    public partial class Table<T> : BaseTable<T>
    {
        [Parameter]
        public List<T> Data { get; set; }
    }
}