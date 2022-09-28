using System.Collections.ObjectModel;

namespace WebCalc.Services
{
    public interface INavigationHistory
    {
        public ReadOnlyCollection<string> History { get; }
    }
}
