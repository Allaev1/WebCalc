using System.Collections.ObjectModel;
using System.ComponentModel;
using WebCalc.Application.Contracts.Constants.DTO;

namespace WebCalc.Blazor.ViewModels.Pages.Consts
{
    public interface IConstsViewModel : INotifyPropertyChanged
    {
        ObservableCollection<ConstantDto> Constants { get; }

        ConstantDto? SelectedConstant { get; set; }

        bool DeleteDisabled { get; set; }

        bool EditDisabled { get; set; }

        Task DeleteConstantAsync();

        Task SetConstantsAsync();

        void NavigateToAddEditConst();

        void NavigateBack();
    }
}
