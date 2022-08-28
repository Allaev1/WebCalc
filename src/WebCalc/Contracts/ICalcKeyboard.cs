using Microsoft.AspNetCore.Components;

namespace WebCalc.Contracts
{
    public interface ICalcKeyboard
    {
        [Parameter]
        public EventCallback<char> OnButtonClick { get; set; }
    }
}
