using Microsoft.AspNetCore.Components;
using WebCalc.Services;

namespace WebCalc.Blazor.AppState
{
    public class AppState : IBackNavigateable
    {
        private Stack<string> locationsStack = new();

        public string GetNaivgateBackLocation()
        {
            var location = locationsStack.Pop();
            return location;
        }

        public void AddCurrentLocation(string location)
        {
            locationsStack.Push(location);
        }
    }
}
