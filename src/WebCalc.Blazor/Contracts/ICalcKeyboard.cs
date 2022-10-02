using Microsoft.AspNetCore.Components;
using System.Drawing;

namespace WebCalc.Contracts
{
    public interface ICalcKeyboard
    {
        [Parameter]
        public EventCallback<char> OnButtonClick { get; set; }

        [Parameter]
        public Color MemoryColor { get; set; }

        [Parameter]
        public Color FigureColor { get; set; }

        [Parameter]
        public Color ConstColor { get; set; }

        [Parameter] 
        public Color OperationTypeColor { get; set; }

        [Parameter]
        public Color BackspaceAndClearColor { get; set; }

        [Parameter]
        public Color PercentageOffColor { get; set; }

        [Parameter]
        public Color ButtonForgeGroundColor { get; set; }
    }
}
