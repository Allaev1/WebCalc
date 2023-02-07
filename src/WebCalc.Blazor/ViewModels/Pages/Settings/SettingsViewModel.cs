using Microsoft.AspNetCore.Components;
using System;
using WebCalc.Application.Contracts.Services.Formater;
using WebCalc.Application.Contracts.Services.Settings;
using WebCalc.Blazor.ViewModels.Base;
using WebCalc.Services;

namespace WebCalc.Blazor.ViewModels.Pages.Settings
{
    public class SettingsViewModel : ViewModelBase, ISettingsViewModel
    {
        private const double SAMPLE = 12345.6789;

        private readonly NavigationManager navigationManager;
        private readonly IBackNavigateable backNavigateable;
        private readonly ISettings settings;
        private readonly IFormater formater;

        public SettingsViewModel(
            NavigationManager navigationManager,
            IBackNavigateable backNavigateable,
            ISettings settings,
            IFormater formater)
        {
            this.navigationManager = navigationManager;
            this.backNavigateable = backNavigateable;
            this.settings = settings;
            this.formater = formater;
        }

        public string Sample
        {
            get { return sample; }
            private set
            {
                sample = value;
                OnPropertyChanged();
            }
        }
        private string sample = string.Empty;

        public bool VibrationOn
        {
            get; set;
        }

        public bool RoundUpOn
        {
            get; set;
        }

        public bool DelimiterOn { get; set; }

        public int Accuracy { get; set; }

        public async Task SetDefaultSettingAsync()
        {
            VibrationOn = true;
            Accuracy = 2;
            DelimiterOn = true;
            RoundUpOn = false;

            await settings.UpdateAsync(SettingsNames.ButtonVibrationOn, VibrationOn);
            await settings.UpdateAsync(SettingsNames.Accuracy, Accuracy);
            await settings.UpdateAsync(SettingsNames.DelimiterOn, DelimiterOn);
            await settings.UpdateAsync(SettingsNames.RoundUpOn, RoundUpOn);

            Sample = await formater.GetFormatedStringFromAsync(SAMPLE);
        }

        public async Task DecrementAccuracyAsync()
        {
            if (Accuracy - 1 >= 0)
            {
                Accuracy--;
                await settings.UpdateAsync(SettingsNames.Accuracy, Accuracy);
                Sample = await formater.GetFormatedStringFromAsync(SAMPLE);
            }
        }

        public async Task IncrementAccuracyAsync()
        {
            if (Accuracy + 1 <= 4)
            {
                Accuracy++;
                await settings.UpdateAsync(SettingsNames.Accuracy, Accuracy);
                Sample = await formater.GetFormatedStringFromAsync(SAMPLE);
            }
        }

        public async Task DelimeterOnChangedAsync()
        {
            await settings.UpdateAsync(SettingsNames.DelimiterOn, DelimiterOn);
            Sample = await formater.GetFormatedStringFromAsync(SAMPLE);
        }

        public async Task VibrationOnChangedAsync()
        {
            await settings.UpdateAsync(SettingsNames.ButtonVibrationOn, VibrationOn);
        }

        public async Task RoundUpChangedAsync()
        {
            await settings.UpdateAsync(SettingsNames.RoundUpOn, RoundUpOn);
            Sample = await formater.GetFormatedStringFromAsync(SAMPLE);
        }

        public async Task SetupSettingsAsync()
        {
            if (!(await settings.IsSettingExistAsync(SettingsNames.ButtonVibrationOn)))
            {
                await settings.CreateAsync(SettingsNames.ButtonVibrationOn, VibrationOn);
            }
            else
            {
                VibrationOn = await settings.GetAsync<bool>(SettingsNames.ButtonVibrationOn);
            }

            if (!(await settings.IsSettingExistAsync(SettingsNames.RoundUpOn)))
            {
                await settings.CreateAsync(SettingsNames.RoundUpOn, RoundUpOn);
            }
            else
            {
                RoundUpOn = await settings.GetAsync<bool>(SettingsNames.RoundUpOn);
            }

            if (!(await settings.IsSettingExistAsync(SettingsNames.DelimiterOn)))
            {
                await settings.CreateAsync(SettingsNames.DelimiterOn, DelimiterOn);
            }
            else
            {
                DelimiterOn = await settings.GetAsync<bool>(SettingsNames.DelimiterOn);
            }

            if (!(await settings.IsSettingExistAsync(SettingsNames.Accuracy)))
            {
                await settings.CreateAsync(SettingsNames.Accuracy, Accuracy);
            }
            else
            {
                Accuracy = await settings.GetAsync<int>(SettingsNames.Accuracy);
            }

            Sample = await formater.GetFormatedStringFromAsync(SAMPLE);
        }

        public void NavigateBack()
        {
            var location = backNavigateable.GetNaivgateBackLocation();
            navigationManager.NavigateTo(location);
        }
    }
}
