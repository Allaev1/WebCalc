namespace WebCalc.Services
{
    public interface IBackNavigateable 
    {
        string GetNaivgateBackLocation();

        void AddCurrentLocation(string location);
    }
}
