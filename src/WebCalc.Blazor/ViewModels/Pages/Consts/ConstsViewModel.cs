using Microsoft.AspNetCore.Components;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using WebCalc.Application.Contracts.Constants;
using WebCalc.Application.Contracts.Constants.DTO;
using WebCalc.Blazor.ViewModels.Base;
using WebCalc.Services;

namespace WebCalc.Blazor.ViewModels.Pages.Consts
{
    public class ConstsViewModel : ViewModelBase, IConstsViewModel
    {
        private readonly NavigationManager navigationManager;
        private readonly IBackNavigateable backNavigateable;
        private readonly IConstantAppService constantAppService;

        public ConstsViewModel(
            IConstantAppService constantAppService,
            IBackNavigateable backNavigateable,
            NavigationManager navigationManager)
        {
            this.constantAppService = constantAppService;
            this.backNavigateable = backNavigateable;
            this.navigationManager = navigationManager;
        }

        public async Task SetConstantsAsync()
        {
            var consts = await constantAppService.GetAllAsync();

            foreach (var @const in consts)
            {
                Constants.Add(@const);
            }
        }

        public ObservableCollection<ConstantDto> Constants { get; } = new();

        public ConstantDto? SelectedConstant
        {
            get { return selectedConstant; }
            set
            {
                selectedConstant = value;
                OnPropertyChanged();
            }
        }
        private ConstantDto? selectedConstant;

        public async Task DeleteConstantAsync()
        {
            if (SelectedConstant is null)
            {
                throw new ArgumentNullException();
            }

            Constants.Remove(SelectedConstant);
            await constantAppService.DeleteAsync(SelectedConstant.Id);

            SelectedConstant = null;
        }

        public void NavigateToAddEditConst()
        {
            if (SelectedConstant is null)
            {
                backNavigateable.AddCurrentLocation("/consts");
                navigationManager.NavigateTo("/addEditConst");
            }
            else
            {
                backNavigateable.AddCurrentLocation("/consts");
                navigationManager.NavigateTo($"/addEditConst/{SelectedConstant.Id}/{SelectedConstant.Name}/{SelectedConstant.Value}/{SelectedConstant.Description}");
            }
        }

        public void NavigateBack()
        {
            var location = backNavigateable.GetNaivgateBackLocation();
            navigationManager.NavigateTo(location);
        }
    }
}
