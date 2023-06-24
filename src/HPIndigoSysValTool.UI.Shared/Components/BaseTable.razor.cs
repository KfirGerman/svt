using HPIndigoSysValTool.UI.Shared.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Linq;
using Serilog.Core;
using HPIndigoSysValTool.Handlers.Logger;

namespace HPIndigoSysValTool.UI.Shared.Components
{
    public partial class BaseTable<T> : ComponentBase
    {
        [Inject] protected IJSRuntime JSRuntime { get; set; }

        [Inject] protected NavigationManager NavigationManager { get; set; }

        [Inject] protected DialogService DialogService { get; set; }

        [Inject] protected TooltipService TooltipService { get; set; }

        [Inject] protected ContextMenuService ContextMenuService { get; set; }

        [Inject] protected NotificationService NotificationService { get; set; }

        [Inject] protected SystemInformationViewModel SystemInformationViewModel { get; set; }

        [Parameter]
        public string TableName { get; set; }

        [Parameter]
        public string Name { get; set; }

        protected List<T> TableData { get; set; } = new();

        protected RenderFragment RenderComponent() => builder =>
        {
            if (TableData.Count == 1)
            {
                builder.OpenComponent(0, typeof(Table<>).MakeGenericType(typeof(T)));
            }
            else
            {
                builder.OpenComponent(0, typeof(AccordionTable<>).MakeGenericType(typeof(T)));
            }

            builder.AddAttribute(1, "TableName", TableName);
            builder.AddAttribute(2, "Data", TableData);
            builder.CloseComponent();
        };

        protected override void OnInitialized()
        {
            try
            {
                LoggerProvider.FileLogger.Information("BaseTable.OnInitialized");
                SetTableData();
            }
            catch (Exception ex)
            {
                LoggerProvider.FileLogger.Error(ex, "ExecutionFailed in BaseTable.OnInitialized");
            }
        }

        protected override void OnParametersSet()
        {
            try
            {
                LoggerProvider.FileLogger.Information("BaseTable.OnParametersSet");
                SetTableData();
            }
            catch (Exception ex)
            {
                LoggerProvider.FileLogger.Error(ex, "ExecutionFailed in BaseTable.OnParametersSet");
            }
        }

        private void SetTableData()
        {
            try
            {
                string tableName = TableName.ToLower();

                switch (tableName)
                {
                    case "memory":
                        TableData = SystemInformationViewModel.MemoryList.Cast<T>().ToList();
                        break;
                    case "storage":
                        TableData = SystemInformationViewModel.StorageList.Cast<T>().ToList();
                        break;
                    case "processor":
                        TableData = SystemInformationViewModel.ProcessorList.Cast<T>().ToList();
                        break;
                    case "system":
                    case "system information":
                        TableData = SystemInformationViewModel.SystemInformationList.Cast<T>().ToList();
                        break;
                    case "video":
                        TableData = SystemInformationViewModel.VideoList.Cast<T>().ToList();
                        break;
                    case "network card":
                        TableData = SystemInformationViewModel.NetworkCardList.Cast<T>().ToList();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                LoggerProvider.FileLogger.Error(ex, "ExecutionFailed in BaseTable.SetTableData");
            }
        }

        private string GetDisplayName(PropertyInfo prop)
        {
            try
            {
                return prop.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? prop.Name;
            }
            catch (Exception ex)
            {
                LoggerProvider.FileLogger.Error(ex, "ExecutionFailed in BaseTable.GetDisplayName");
                return prop.Name;
            }
        }
    }
}
