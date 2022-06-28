namespace WebCalc.Domain.BinaryOperation
{
    public class BinaryOperation
    {
        private readonly string operand1NotSetExceptionMessage;
        private readonly string operationTypeNotSetExceptionMessage;
        private readonly string operand2NotSetExceptionMessage;
        private readonly string operationTypeSettedExceptionMessage;
        private readonly string operand1SettedExceptionMessage;

        internal BinaryOperation(
            string operand1NotSetExceptionMessage, 
            string operationTypeNotSetExceptionMessage,
            string operand2NotSetExceptionMessage,
            string operationTypeSettedExceptionMessage,
            string operand1SettedExceptionMessage)
        {
            this.operand1NotSetExceptionMessage = operand1NotSetExceptionMessage;
            this.operationTypeNotSetExceptionMessage = operationTypeNotSetExceptionMessage;
            this.operand2NotSetExceptionMessage = operand2NotSetExceptionMessage;
            this.operationTypeSettedExceptionMessage = operationTypeSettedExceptionMessage;
            this.operand1SettedExceptionMessage = operand1SettedExceptionMessage;
        }

        public float? Operand1 { get; private set; }

        public float? Operand2 { get; private set; }

        public OperationType? OperationType { get; private set; }

        public float? Result { get; private set; }

        public void SetOperand1(float value)
        {
            if(Operand1.HasValue)
                throw new Operand1SettedException(operand1SettedExceptionMessage);
            Operand1 = value;
        }

        public void SetOperand2(float value)
        {
            if(!OperationType.HasValue)
                throw new OperationTypeNotSetException(operationTypeNotSetExceptionMessage);
            Operand2 = value;
        }

        public void SetOperationType(OperationType operationType)
        {
            if (!Operand1.HasValue)
                throw new Operand1NotSetException(operand1NotSetExceptionMessage);
            if (Operand2.HasValue)
                throw new OperationTypeSettedException(operationTypeSettedExceptionMessage);
            OperationType = operationType;
        }

        public void CalculateResult()
        {
            if (!Operand2.HasValue)
                throw new Operand2NotSetException(operand2NotSetExceptionMessage);
            Result = GetResult();
            StartNewOperation();
        }

        private void StartNewOperation()
        {
            Operand1 = null;
            Operand2 = null;
            OperationType = null;
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