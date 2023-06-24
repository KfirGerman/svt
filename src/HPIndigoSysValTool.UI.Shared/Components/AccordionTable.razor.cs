using Microsoft.AspNetCore.Components;

namespace HPIndigoSysValTool.UI.Shared.Components
{
    public partial class AccordionTable<T> : BaseTable<T>
    {
        private int _expandedAccordionIndex = -1;
        private bool IsAccordionExpanded(int index)
        {
            return _expandedAccordionIndex == index;
        }

        private void SetAccordionExpanded(int index)
        {
            _expandedAccordionIndex = index;
        }
        [Parameter]
        public List<T> Data { get; set; }
        private List<T> WrapInList(T item)
        {
            return new List<T> { item };
        }

        private List<object> WrapObjectInList(object item)
        {
            return new List<object> { item };
        }

    }
}