using WebCalc.Domain.BinaryOperation;

namespace WebCalc.Application.Contracts.BinaryOperation
{
    public interface IBinaryOperationAppService
    {
        public event EventHandler<string> DisplayValueChanged;

        public event EventHandler<string> ExpressionValueChanged;

        public void EditDisplayValue(char value);

        public void EditExpressionValue(char value);   
    }
}