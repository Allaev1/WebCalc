using System.Collections.ObjectModel;

namespace WebCalc.Services.CustomNavigationManager
{
    public interface INavigationHistory
    {
        public ReadOnlyCollection<string> History { get; }
    }
}
