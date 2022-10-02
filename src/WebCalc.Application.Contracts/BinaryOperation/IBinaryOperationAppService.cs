using WebCalc.Domain.BinaryOperation;
using WebCalc.Domain.Shared;

namespace WebCalc.Application.Contracts.BinaryOperation
{
    public interface IBinaryOperationAppService
    {
        public void SetOperand(float operand);

        public void SetOperationType(OperationType operationType);

        public float GetResult();
    }
}