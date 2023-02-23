using Microsoft.AspNetCore.Components;
using System;
using WebCalc.Application.Contracts.BinaryOperation;
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
        private readonly IBinaryOperationAppService binaryOperationAppService;

        public CalculatorViewModel(
            NavigationManager navigationManager,
            IBackNavigateable backNavigateable,
            ISettings settings,
            IBinaryOperationAppService binaryOperationAppService)
        {
            this.navigationManager = navigationManager;
            this.backNavigateable = backNavigateable;
            this.settings = settings;
            this.binaryOperationAppService = binaryOperationAppService;
        }

        public async Task SetRoundingFlagAsync()
        {
            IsRounding = await settings.GetAsync<bool>(SettingsNames.RoundUpOn);
        }

        public bool IsRounding { get; private set; }

        public void NavigateToAddingNewConstant(string constantValue)
        {
            backNavigateable.AddCurrentLocation(navigationManager.Uri);
            navigationManager.NavigateTo($"addEditConst/{constantValue}");
        }

        public bool GetIsPossibleToAddNewConstant()
        {
            switch (binaryOperationAppService.GetState())
            {
                case Domain.BinaryOperation.BinaryOperationState.Start:
                case Domain.BinaryOperation.BinaryOperationState.SettingOperand1:
                case Domain.BinaryOperation.BinaryOperationState.Operand1Setted:
                    return binaryOperationAppService.GetOperand1() > 0;
                default:
                    return binaryOperationAppService.GetOperand2() > 0;
            }
        }

        public void NavigateToConstants()
        {
            backNavigateable.AddCurrentLocation(navigationManager.Uri);
            navigationManager.NavigateTo("consts");
        }

        public void NavigateToSettings()
        {
            backNavigateable.AddCurrentLocation(navigationManager.Uri);
            navigationManager.NavigateTo("settings");
        }
    }
}
