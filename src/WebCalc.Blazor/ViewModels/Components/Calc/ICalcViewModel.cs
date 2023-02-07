using WebCalc.Blazor.ViewModels.Components.CalcDisplay;
using WebCalc.Domain.Shared;

namespace WebCalc.Blazor.ViewModels.Components.Calc
{
    public interface ICalcViewModel
    {
        Task UpdateDisplayAsync(char value);

        void OnValidOperandGenerated(float operand);

        void OnOperationTypeChanged(OperationType operationType);

        /// <summary>
        /// For test purpose only. Clear singelton operations
        /// </summary>
        public void ClearOperations();

        public ICalcDisplayViewModel? DisplayViewModel { get; }
    }
}
