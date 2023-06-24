using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HPIndigoSysValTool.Validation;
using HPIndigoSysValTool.Handlers.Logger;

namespace HPIndigoSysValTool.UI.Shared.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public PressConfiguration PressConfiguration { get; }

        // create a new instance of the PressConfiguration class
        public MainViewModel()
        {
            PressConfiguration = PressConfiguration.Instance;
        }
    }
}