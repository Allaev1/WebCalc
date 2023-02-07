using System.ComponentModel;
using WebCalc.Blazor.Validation;

namespace WebCalc.Blazor.ViewModels.Pages.AddEditConst
{
    public interface IAddEditConstViewModel : INotifyPropertyChanged
    {
        public event EventHandler<string> ConstantAddedUpdated;

        public AddEditConstValidationModel ValidationModel { get; }

        public Guid? ConstId { get; set; }

        public string? ConstName { get; set; }

        public float? ConstValue { get; set; }

        public string? ConstDescription { get; set; }

        public string? Title { get; }

        public string? SaveButtonText { get; }

        Task SaveAsync();

        void NavigateBack();
    }
}
