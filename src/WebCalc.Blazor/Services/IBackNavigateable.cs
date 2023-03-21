namespace WebCalc.Services
{
    public interface IBackNavigateable : IDisposable 
    {
        string GetNaivgateBackLocation();
    }
}
