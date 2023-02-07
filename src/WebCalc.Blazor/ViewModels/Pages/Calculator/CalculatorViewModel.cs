using Microsoft.AspNetCore.Components;
using System;
using WebCalc.Application.Contracts.Services.Settings;
using WebCalc.Blazor.ViewModels.Base;
using WebCalc.Services;

namespace WebCalc.Blazor.ViewModels.Pages.Calculator
{
    public class CalculatorViewModel : ViewModelBase, ICalculatorViewModel
    {
        private readonly NavigationManager navigationManager;
        private readonly IBackNavigateable backNavigateable;
        private readonly ISettings settings;

        public CalculatorViewModel(
            NavigationManager navigationManager,
            IBackNavigateable backNavigateable,
            ISettings settings)
        {
            this.navigationManager = navigationManager;
            this.backNavigateable = backNavigateable;
            this.settings = settings;
        }

        public async Task SetRoundingFlagAsync()
        {
            IsRounding = await settings.GetAsync<bool>(SettingsNames.RoundUpOn);
        }

        public bool IsRounding { get; private set; }

        public void NavigateToAddingNewConstant(string constantValue)
        {
            backNavigateable.AddCurrentLocation(navigationManager.Uri);
            navigationManager.NavigateTo($"/addEditConst/{constantValue}");
        }

        public void NavigateToConstants()
        {
            backNavigateable.AddCurrentLocation(navigationManager.Uri);
            navigationManager.NavigateTo("/consts");
        }

        public void NavigateToSettings()
        {
            backNavigateable.AddCurrentLocation(navigationManager.Uri);
            navigationManager.NavigateTo("/settings");
        }
    }
}
