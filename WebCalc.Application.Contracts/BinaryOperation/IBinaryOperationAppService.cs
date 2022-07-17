using WebCalc.Domain.BinaryOperation;

namespace WebCalc.Application.Contracts.BinaryOperation
{
    public interface IBinaryOperationAppService
    {
        public event EventHandler<string> DisplayValueChanged;

        public void SetDisplayValue(char value);

        public void SetOperand(string value);

        public void SetOperationType(OperationType operationType);

        public string GetResult();
    }
}