using WebCalc.Domain.BinaryOperation;

namespace WebCalc.Application.Contracts.BinaryOperation
{
    public interface IBinaryOperationAppService
    {
        public void SetOperand(string value);

        public void SetOperationType(OperationType operationType);

        public void SetResult();
    }
}