namespace WebCalc.Contracts
{
    public interface ICalc
    {
        public string GetDisplayValue();

        public string GetDisplayMemory();

        public string GetDisplayExpression();
    }
}
