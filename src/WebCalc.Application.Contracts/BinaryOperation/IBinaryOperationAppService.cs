using WebCalc.Domain.BinaryOperation;

namespace WebCalc.Application.Contracts.BinaryOperation
{
    public interface IBinaryOperationAppService
    {
        public event EventHandler<string> DisplayValueChanged;

        public event EventHandler<string> ExpressionValueChanged;

        /// <summary>
        /// Edit display and expression values
        /// </summary>
        /// <param name="value"></param>
        public void EditValues(char value);
    }
}