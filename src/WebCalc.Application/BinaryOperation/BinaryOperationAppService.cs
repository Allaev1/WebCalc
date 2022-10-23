using WebCalc.Application.Contracts.BinaryOperation;
using WebCalc.Domain.BinaryOperation;
using WebCalc.Domain.Shared;
using WebCalc.Domain.UnaryOperation;

namespace WebCalc.Application.BinaryOperation
{
    public class BinaryOperationAppService : IBinaryOperationAppService
    {
        public void SetOperand(float operand)
        {
            Domain.BinaryOperation.BinaryOperation.Instance().SetOperand(operand);
        }

        public void SetOperationType(OperationType operationType)
        {
            Domain.BinaryOperation.BinaryOperation.Instance().SetOperationType(operationType);
        }

        public void SetResult()
        {
            Domain.BinaryOperation.BinaryOperation.Instance().SetResult();
        }

        public float? GetOperand1() => Domain.BinaryOperation.BinaryOperation.Instance().Operand1;

        public OperationType? GetOperationType() => Domain.BinaryOperation.BinaryOperation.Instance().OperationType;

        public float? GetOperand2() => Domain.BinaryOperation.BinaryOperation.Instance().Operand2;

        public float? GetResult() => Domain.BinaryOperation.BinaryOperation.Instance().Result;

        public BinaryOperationState GetState() => Domain.BinaryOperation.BinaryOperation.Instance().OperationState;

        public void ClearOperations()
        {
            Domain.BinaryOperation.BinaryOperation.Instance().Clear();
            Domain.UnaryOperation.UnaryOperation.GetNegationOperation().Clear();
            Domain.UnaryOperation.UnaryOperation.GetPercentageOperation().Clear();
        }

        public float GetUpdatedMemory(float increase, float current) => increase + current;

        public float GetNumberWithoutPercentage(int percentageOff)
        {
            Domain.UnaryOperation.UnaryOperation.GetPercentageOperation().SetOperand(percentageOff);
            Domain.UnaryOperation.UnaryOperation.GetPercentageOperation().SetResult();
            var percentage = Domain.UnaryOperation.UnaryOperation.GetPercentageOperation().Result;

            var temp = Domain.BinaryOperation.BinaryOperation.Instance().Operand1;
            Domain.BinaryOperation.BinaryOperation.Instance().Clear();

            Domain.BinaryOperation.BinaryOperation.Instance().SetOperand(temp);
            Domain.BinaryOperation.BinaryOperation.Instance().SetOperationType(OperationType.Multiplication);
            Domain.BinaryOperation.BinaryOperation.Instance().SetOperand(percentage);
            Domain.BinaryOperation.BinaryOperation.Instance().SetResult();
            var percentageOf = Domain.BinaryOperation.BinaryOperation.Instance().Result;
            Domain.BinaryOperation.BinaryOperation.Instance().Clear();

            Domain.BinaryOperation.BinaryOperation.Instance().SetOperand(temp);
            Domain.BinaryOperation.BinaryOperation.Instance().SetOperationType(OperationType.Subtraction);
            Domain.BinaryOperation.BinaryOperation.Instance().SetOperand(percentageOf);
            Domain.BinaryOperation.BinaryOperation.Instance().SetResult();

            return Domain.BinaryOperation.BinaryOperation.Instance().Result!.Value;
        }

        public void NegateOperand()
        {
            if (Domain.BinaryOperation.BinaryOperation.Instance().OperationState is BinaryOperationState.SettingOperand1)
            {
                Domain.UnaryOperation.UnaryOperation.GetPercentageOperation().SetOperand(Domain.BinaryOperation.BinaryOperation.Instance().Operand1!.Value);
            }
            else
            {
                Domain.UnaryOperation.UnaryOperation.GetPercentageOperation().SetOperand(Domain.BinaryOperation.BinaryOperation.Instance().Operand2!.Value);
            }

            Domain.UnaryOperation.UnaryOperation.GetPercentageOperation().SetResult();
            var negatedOperand = Domain.UnaryOperation.UnaryOperation.GetPercentageOperation().Result;

            Domain.BinaryOperation.BinaryOperation.Instance().SetOperand(negatedOperand);
        }
    }
}
