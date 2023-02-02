using WebCalc.Blazor.ViewModels.Components.Calc;

namespace WebCalc.Blazor.Components.Calc
{
    public interface ICalc
    {
        public string GetDisplayValue();

        public string GetDisplayMemory();

        public string GetDisplayExpression();

        public ICalcViewModel? ViewModel { get; }
    }
}
