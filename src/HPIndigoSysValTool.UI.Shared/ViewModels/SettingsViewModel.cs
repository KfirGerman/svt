using HPIndigoSysValTool.Validation;
using HPIndigoSysValTool.UI.Shared.ViewModels;

namespace HPIndigoSysValTool.UI.Shared.ViewModels
{
    /// <summary>
    /// ViewModel for the Settings page, responsible for handling the user input
    /// to update the press series and model configuration.
    /// </summary>
    public class SettingsViewModel : BaseViewModel
    {
        private string? _pressSeries;
        private string? _pressModel;

        /// <summary>
        /// Property for the press series. When the user updates the press series,
        /// it will call the UpdateSeriesAndModel method in the PressConfiguration class.
        /// </summary>
        public string? PressSeries
        {
            get => _pressSeries;
            set
            {
                if (SetProperty(ref _pressSeries, value))
                {
                    _pressSeries = value;
                }
            }
        }

        /// <summary>
        /// Property for the press model. When the user updates the press model,
        /// it will call the UpdateSeriesAndModel method in the PressConfiguration class.
        /// </summary>
        public string? PressModel
        {
            get => _pressModel;
            set
            {
                if (SetProperty(ref _pressModel, value))
                {
                    _pressModel = value;
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the SettingsViewModel class.
        /// </summary>
        public SettingsViewModel() : base()
        {
            _pressSeries = PressConfiguration.Instance.Series;
            _pressModel = PressConfiguration.Instance.Model;
        }

        public async Task SaveSettings()
        {
            // You can add more logic or validation here if needed.
            PressConfiguration.UpdateSeriesAndModel(PressSeries, PressModel);
            // Trigger an event or use a callback to notify the UI about the successful save.
        }

    }
}