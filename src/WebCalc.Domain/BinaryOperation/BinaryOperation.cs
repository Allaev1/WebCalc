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

        public OperationState OperationState { get; private set; }

        public void SetOperand(float? value)
        {
            SetState(value!);
        }

        public void SetOperationType(OperationType? operationType)
        {
            SetState(operationType!);
        }

        public void SetResult()
        {
            SetState(null!);
        }

        public void ClearOperation()
        {
            Operand1 = null;
            Operand2 = null;
            OperationType = null;
            Result = null;
            OperationState = OperationState.SettingOperand1;
        }

        private void SetState(object value)
        {
            if (value is OperationType)
                OperationState = OperationState.Operand1Setted;
            else if (value is float && OperationState is OperationState.Operand1Setted)
                OperationState = OperationState.OperationTypeSetted;
            else if (value is null)
                OperationState = OperationState.Operand2Setted;
            else if (OperationState is OperationState.Start && value is float)
                OperationState = OperationState.SettingOperand1;

            switch (OperationState)
            {
                case OperationState.SettingOperand1:
                    Operand1 = (float?)value;
                    break;
                case OperationState.Operand1Setted:
                    OperationType = (OperationType?)value;
                    break;
                case OperationState.OperationTypeSetted:
                    Operand2 = (float?)value;
                    break;
                case OperationState.Operand2Setted:
                    Result = GetResult();
                    Operand1 = null;
                    Operand2 = null;
                    OperationType = null;
                    OperationState = OperationState.ResultSetted;
                    break;
                case OperationState.ResultSetted:
                    Operand1 = (float?)value;
                    Result = null;
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