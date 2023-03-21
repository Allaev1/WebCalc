using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using WebCalc.Services;

namespace WebCalc.Blazor.AppState
{
    public class AppState : IBackNavigateable
    {
        private readonly NavigationManager navigationManager;
        private readonly IDisposable locationChangingHandler;

        public AppState(NavigationManager navigationManager)
        {
            this.navigationManager = navigationManager;

            locationChangingHandler = navigationManager.RegisterLocationChangingHandler(NavigationManagerLocationChangingHandler);
        }

        private Stack<string> locationsStack = new();

        private async ValueTask NavigationManagerLocationChangingHandler(LocationChangingContext arg)
        {
            locationsStack.Push(navigationManager.Uri);

            await Task.CompletedTask;
        }

        public string GetNaivgateBackLocation()
        {
            var location = locationsStack.Pop();
            return location;
        }

        public void Dispose()
        {
            locationChangingHandler.Dispose();
        }
    }
}
