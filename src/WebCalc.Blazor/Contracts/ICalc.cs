namespace WebCalc.Contracts
{
    public interface ICalc
    {
        public string GetDisplayValue();

        public string GetDisplayMemory();

        public string GetDisplayExpression();

        /// <summary>
        /// For test purpose only. Clear singelton operations
        /// </summary>
        public void ClearOperations();
    }
}
