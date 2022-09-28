namespace WebCalc.Services.CustomNavigationManager
{
    public interface IBackNavigateable : INavigationHistory
    {
        void NavigateBack();
    }
}
