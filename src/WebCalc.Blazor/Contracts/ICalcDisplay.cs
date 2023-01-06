using Microsoft.AspNetCore.Components;
using System.Drawing;
using WebCalc.Domain.Shared;

namespace WebCalc.Contracts
{
    public interface ICalcDisplay
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

        public string Value { get; }

        public string Expression { get; }

        public string Memory { get; }

        public void SetMemory(string memory);

        public void ClearMemory();

        public void ReadMemory();

        public Task AppendAsync(char @char);

        public void Append(char[] chars);
    }
}
