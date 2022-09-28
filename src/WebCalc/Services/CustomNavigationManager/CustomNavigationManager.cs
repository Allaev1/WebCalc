using Microsoft.AspNetCore.Components;
using System.Collections.ObjectModel;

namespace WebCalc.Services.CustomNavigationManager
{
    public class CustomNavigationManager : NavigationManager, IBackNavigateable, IDisposable
    {
        private List<string> history = new();

        public CustomNavigationManager()
        {
            LocationChanged += NavigationManagerWithHistory_LocationChanged;
            History = new(history);
        }

        public ReadOnlyCollection<string> History { get; }

        private void NavigationManagerWithHistory_LocationChanged(object? sender, Microsoft.AspNetCore.Components.Routing.LocationChangedEventArgs e)
        {
            history.Add(e.Location);
        }

        public void NavigateBack()
        {
            var location = history.Last();
            history.Remove(location);
            NavigateTo(location);
        }

        public void Dispose()
        {
            LocationChanged -= NavigationManagerWithHistory_LocationChanged;
        }
    }
}
