using WebCalc.Application.Contracts.BinaryOperation;
using WebCalc.Domain.BinaryOperation;
using WebCalc.Domain.Shared;
using WebCalc.Domain.UnaryOperation;

namespace WebCalc.Application.BinaryOperation
{
    public class BinaryOperationAppService : IBinaryOperationAppService
    {
        private readonly IBinaryOperationManager binaryOperationManager;
        private readonly IUnaryOperationManager unaryOperationManager;

        public BinaryOperationAppService(IBinaryOperationManager binaryOperationManager, IUnaryOperationManager unaryOperationManager)
        {
            this.binaryOperationManager = binaryOperationManager;
            this.unaryOperationManager = unaryOperationManager;
        }

        public void SetOperand(float operand)
        {
            binaryOperationManager.Operation.SetOperand(operand);
        }

        public void SetOperationType(OperationType operationType)
        {
            binaryOperationManager.Operation.SetOperationType(operationType);
        }

        public void SetResult()
        {
            binaryOperationManager.Operation.SetResult();
        }

        public float? GetOperand1() => binaryOperationManager.Operation.Operand1;

        public OperationType? GetOperationType() => binaryOperationManager.Operation.OperationType;

        public float? GetOperand2() => binaryOperationManager.Operation.Operand2;

        public float? GetResult() => binaryOperationManager.Operation.Result;

        public BinaryOperationState GetState() => binaryOperationManager.Operation.OperationState;

        public void ClearOperation()
        {
            binaryOperationManager.Operation.Clear();
        }

        public float GetUpdatedMemory(float increase, float current) => increase + current;

        public float GetNumberWithoutPercentage(int percentageOff)
        {
            unaryOperationManager.Percentage.SetOperand(percentageOff);
            unaryOperationManager.Percentage.SetResult();
            var percentage = unaryOperationManager.Percentage.Result;

            var temp = binaryOperationManager.Operation.Operand1;
            binaryOperationManager.Operation.Clear();

            binaryOperationManager.Operation.SetOperand(temp);
            binaryOperationManager.Operation.SetOperationType(OperationType.Multiplication);
            binaryOperationManager.Operation.SetOperand(percentage);
            binaryOperationManager.Operation.SetResult();
            var percentageOf = binaryOperationManager.Operation.Result;
            binaryOperationManager.Operation.Clear();

            binaryOperationManager.Operation.SetOperand(temp);
            binaryOperationManager.Operation.SetOperationType(OperationType.Subtraction);
            binaryOperationManager.Operation.SetOperand(percentageOf);
            binaryOperationManager.Operation.SetResult();

            return binaryOperationManager.Operation.Result!.Value;
        }

        public void NegateOperand()
        {
            if (binaryOperationManager.Operation.OperationState is BinaryOperationState.SettingOperand1)
            {
                unaryOperationManager.Negate.SetOperand(binaryOperationManager.Operation.Operand1!.Value);
            }
            else
            {
                unaryOperationManager.Negate.SetOperand(binaryOperationManager.Operation.Operand2!.Value);
            }

            unaryOperationManager.Negate.SetResult();
            var negatedOperand = unaryOperationManager.Negate.Result;

            binaryOperationManager.Operation.SetOperand(negatedOperand);
        }
    }
}
