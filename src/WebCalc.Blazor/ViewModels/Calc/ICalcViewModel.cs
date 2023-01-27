using WebCalc.Blazor.ViewModels.CalcDisplay;
using WebCalc.Domain.Shared;

namespace WebCalc.Blazor.ViewModels.Calc
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
