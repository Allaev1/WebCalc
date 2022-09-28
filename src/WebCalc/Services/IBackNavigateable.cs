namespace WebCalc.Services
{
    public interface IBackNavigateable : INavigationHistory
    {
        void NavigateBack();
    }
}
