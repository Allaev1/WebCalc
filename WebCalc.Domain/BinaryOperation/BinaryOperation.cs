using WebCalc.Domain.BinaryOperation;

namespace WebCalc.Domain.BinaryOperation
{
    public class BinaryOperation
    {
        private OperationState operationState;
        private readonly string operand1NotSetExceptionMessage;
        private readonly string operationTypeNotSetExceptionMessage;
        private readonly string operand2NotSetExceptionMessage;

        internal BinaryOperation(string operand1NotSetExceptionMessage, string operationTypeNotSetExceptionMessage, string operand2NotSetExceptionMessage)
        {
            this.operand1NotSetExceptionMessage = operand1NotSetExceptionMessage;
            this.operationTypeNotSetExceptionMessage = operationTypeNotSetExceptionMessage;
            this.operand2NotSetExceptionMessage = operand2NotSetExceptionMessage;
        }

        public float Operand1 { get; private set; }

        public float Operand2 { get; private set; }

        public OperationType OperationType { get; private set; }

        public float Result { get; private set; }

        public void SetOperand1(float value)
        {
            Operand1 = value;
            operationState = OperationState.Operand1Set;
            Result = 0;
        }

        public void SetOperand2(float value)
        {
            if (operationState != OperationState.OperationTypeSet)
                throw new OperationTypeNotSetException(operationTypeNotSetExceptionMessage);

            Operand2 = value;
            operationState = OperationState.Operand2Set;
        }

        public void SetOperationType(OperationType operationType)
        {
            if (operationState != OperationState.Operand1Set)
                throw new Operand1NotSetException(operand1NotSetExceptionMessage);

            OperationType = operationType;
            operationState = OperationState.OperationTypeSet;
        }

        public void CalculateResult()
        {
            if (operationState != OperationState.Operand2Set)
                throw new Operand2NotSetException(operand2NotSetExceptionMessage);
            Result = GetResult();
            StartNewOperation();
        }

        private void StartNewOperation()
        {
            Operand1 = 0;
            Operand2 = 0;
            OperationType = OperationType.NotSet;
            operationState = OperationState.Operand1NotSet;
        }

        private float GetResult() => OperationType switch
        {
            OperationType.Addition => Operand1 + Operand2,
            OperationType.Subtraction => Operand1 - Operand2,
            OperationType.Division => Operand1 / Operand2,
            OperationType.Multiplication => Operand1 * Operand2,
            _ => throw new NotImplementedException()
        };
    }
}