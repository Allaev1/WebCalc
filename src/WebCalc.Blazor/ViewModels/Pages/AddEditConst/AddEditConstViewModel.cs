using Microsoft.AspNetCore.Components;
using System;
using WebCalc.Application.Constant;
using WebCalc.Application.Contracts.Constants;
using WebCalc.Blazor.Validation;
using WebCalc.Blazor.ViewModels.Base;
using WebCalc.Services;

namespace WebCalc.Blazor.ViewModels.Pages.AddEditConst
{
    public class AddEditConstViewModel : ViewModelBase, IAddEditConstViewModel
    {
        private readonly IConstantAppService constantAppService;
        private readonly NavigationManager navigationManager;
        private readonly IBackNavigateable backNavigateable;

        public AddEditConstViewModel(
            IConstantAppService constantAppService,
            NavigationManager navigationManager,
            IBackNavigateable backNavigateable)
        {
            this.constantAppService = constantAppService;
            this.navigationManager = navigationManager;
            this.backNavigateable = backNavigateable;
        }

        public event EventHandler<string>? ConstantAddedUpdated;

        public AddEditConstValidationModel ValidationModel { get; } = new();

        public Guid? ConstId
        {
            get { return constId; }
            set
            {
                constId = value;
                if (value is null)
                {
                    SaveButtonText = "Add";
                    Title = "Adding constant";
                }
                else
                {
                    SaveButtonText = "Update";
                    Title = "Update constant";
                }
            }
        }
        private Guid? constId;

        public string? ConstName
        {
            get
            {
                return ValidationModel.Name;
            }
            set
            {
                ValidationModel.Name = value;
                OnPropertyChanged();
            }
        }

        public float? ConstValue
        {
            get
            {
                return ValidationModel.Value;
            }
            set
            {
                ValidationModel.Value = value;
                OnPropertyChanged();
            }
        }

        public string? ConstDescription
        {
            get
            {
                return ValidationModel.Description;
            }
            set
            {
                ValidationModel.Description = value;
                OnPropertyChanged();
            }
        }

        public string? SaveButtonText { get; private set; }

        public string? Title { get; private set; }

        public async Task SaveAsync()
        {
            if (ConstId is null)
            {
                await constantAppService.CreateAsync(new()
                {
                    Name = ConstName!,
                    Description = ConstDescription,
                    Value = ConstValue!.Value
                });

                ConstantAddedUpdated?.Invoke(this, "Constant added successfully");
            }
            else
            {
                await constantAppService.UpdateAsync(ConstId!.Value, new()
                {
                    Name = ConstName!,
                    Description = ConstDescription,
                    Value = ConstValue!.Value
                });

                ConstantAddedUpdated?.Invoke(this, "Constant updated successfully");
            }

            Clear();
            NavigateBack();
        }

        private void Clear()
        {
            ConstName = null;
            ConstValue = null;
            ConstDescription = null;
        }

        public void NavigateBack()
        {
            var location = backNavigateable.GetNaivgateBackLocation();
            navigationManager.NavigateTo(location);
        }
    }
}
