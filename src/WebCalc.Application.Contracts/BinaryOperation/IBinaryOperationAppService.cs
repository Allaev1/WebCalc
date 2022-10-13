using WebCalc.Domain.BinaryOperation;
using WebCalc.Domain.Shared;

namespace WebCalc.Application.Contracts.BinaryOperation
{
    public interface IBinaryOperationAppService
    {
        public void SetOperand(float operand);

        public void SetOperationType(OperationType operationType);

        public void SetResult();

        public float GetUpdatedMemory(float increase, float current);

        public float GetNumberWithoutPercentage(int percentageOff);

        public float? GetOperand1();

        public OperationType? GetOperationType();

        public float? GetOperand2();

        public float? GetResult();

        public BinaryOperationState GetState();

        public void ClearOperation();

        public void NegateOperand();
    }
}