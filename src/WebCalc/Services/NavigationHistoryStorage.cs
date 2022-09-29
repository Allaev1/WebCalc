using Microsoft.AspNetCore.Components;

namespace WebCalc.Services
{
    public class NavigationHistoryStorage : IBackNavigateable
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
