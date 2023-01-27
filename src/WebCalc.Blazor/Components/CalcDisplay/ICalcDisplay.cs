using Microsoft.AspNetCore.Components;
using System.Drawing;
using WebCalc.Blazor.ViewModels.CalcDisplay;
using WebCalc.Domain.Shared;

namespace WebCalc.Blazor.Components.CalcDisplay
{
    public interface ICalcDisplay : IDisposable
    {
        [Parameter]
        public int MaxDisplayCharsCount { get; set; }

        [Parameter]
        public EventCallback<float> OnValidOperandGenerated { get; set; }

        [Parameter]
        public EventCallback<OperationType> OnOperationTypeChanged { get; set; }

        [Parameter]
        public Color MemoryColor { get; set; }

        [Parameter]
        public Color ExpressionColor { get; set; }

        [Parameter]
        public Color ValueColor { get; set; }

        [CascadingParameter]
        public ICalcDisplayViewModel? ViewModel { get; set; }
    }
}
