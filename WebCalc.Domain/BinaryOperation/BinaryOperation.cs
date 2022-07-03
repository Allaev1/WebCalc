namespace WebCalc.Domain.BinaryOperation
{
    public class BinaryOperation
    {
        internal BinaryOperation()
        {
        }

        public float? Operand1 { get; private set; }

        public float? Operand2 { get; private set; }

        public OperationType? OperationType { get; private set; }

        public float? Result { get; private set; }

        public void SetOperand1(float value)
        {
        }

        public void SetOperand2(float value)
        {
        }

        public void SetOperationType(OperationType operationType)
        {
        }

        public void CalculateResult()
        {
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