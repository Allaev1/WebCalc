using System.ComponentModel;
using WebCalc.Domain.Shared;

namespace WebCalc.Blazor.ViewModels.Components.CalcDisplay
{
    public interface ICalcDisplayViewModel : INotifyPropertyChanged
    {
        public void SetMemory(string memory);

        public void ClearMemory();

        public void ReadMemory();

        public void Append(char @char);

        public void Append(char[] chars);

        public void Clear();

        public string Value { get; }

        public string Memory { get; }

        public string Expression { get; }

        public bool PercentageOff { get; set; }

        public event EventHandler<float>? OnValidOperandGenerated;
        public event EventHandler<OperationType>? OnOperationTypeChanged;
        public event EventHandler? MemorySetted;
        public event EventHandler? MemoryCleared;
    }
}
