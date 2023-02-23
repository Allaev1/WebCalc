using System.ComponentModel;

namespace WebCalc.Blazor.ViewModels.Pages.Calculator
{
    public interface ICalculatorViewModel : INotifyPropertyChanged
    {
        bool IsRounding { get; }

        Task SetRoundingFlagAsync();

        void NavigateToConstants();

        void NavigateToSettings();

        void NavigateToAddingNewConstant(string constantValue);

        bool GetIsPossibleToAddNewConstant();
    }
}
