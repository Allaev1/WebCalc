using System.ComponentModel;

namespace WebCalc.Blazor.ViewModels.Pages.Settings
{
    public interface ISettingsViewModel : INotifyPropertyChanged
    {
        string Sample { get; }

        bool VibrationOn { get; set; }

        bool RoundUpOn { get; set; }

        bool DelimiterOn { get; set; }

        int Accuracy { get; set; }

        Task SetDefaultSettingAsync();

        Task DecrementAccuracyAsync();

        Task IncrementAccuracyAsync();

        Task DelimeterOnChangedAsync();

        Task VibrationOnChangedAsync();

        Task RoundUpChangedAsync();

        Task SetupSettingsAsync();

        void NavigateBack();
    }
}
