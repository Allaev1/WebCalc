using WebCalc.Domain.BinaryOperation.Exceptions;

namespace WebCalc.Domain.BinaryOperation
{
    public class BinaryOperation
    {
        private OperationState operationState;

        internal BinaryOperation()
        {
        }

        public float? Operand1 { get; private set; }

        public float? Operand2 { get; private set; }

        public OperationType? OperationType { get; private set; }

        public float? Result { get; private set; }

        public void SetOperand(float value)
        {
            SetState(value);
        }

        public void SetOperationType(OperationType operationType)
        {
            SetState(operationType);
        }

        public void SetResult()
        {
            SetState(GetResult());
        }

        private void SetState(object value)
        {
            switch (operationState)
            {
                case OperationState.Start:
                    Operand1 = (float)value;
                    operationState = OperationState.Operand1Setted;
                    break;
                case OperationState.Operand1Setted:
                    OperationType = (OperationType)value;
                    operationState = OperationState.OperationTypeSetted;
                    break;
                case OperationState.OperationTypeSetted:
                    Operand2 = (float)value;
                    operationState = OperationState.Operand2Setted;
                    break;
                case OperationState.Operand2Setted:
                    Result = (float)value;
                    Operand1 = null;
                    Operand2 = null;
                    OperationType = null;
                    operationState = OperationState.ResultSetted;
                    break;
                case OperationState.ResultSetted:
                    Operand1 = (float)value;
                    Result = null;
                    operationState = OperationState.Operand1Setted;
                    break;
            }
        }

        private float GetResult() => OperationType!.Value switch
        {
            Domain.BinaryOperation.OperationType.Addition => Operand1!.Value + Operand2!.Value,
            Domain.BinaryOperation.OperationType.Subtraction => Operand1!.Value - Operand2!.Value,
            Domain.BinaryOperation.OperationType.Division => Operand1!.Value / Operand2!.Value,
            Domain.BinaryOperation.OperationType.Multiplication => Operand1!.Value * Operand2!.Value,
            _ => throw new NotImplementedException()
        };
    }
}