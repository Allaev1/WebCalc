using WebCalc.Domain.Shared;

namespace WebCalc.Domain.BinaryOperation
{
    public class BinaryOperation
    {
        private static BinaryOperation instance;

        public static BinaryOperation Instance()
        {
            if (instance is null)
                instance = new ();
            return instance;
        }

        public float? Operand1 { get; private set; }

        public float? Operand2 { get; private set; }

        public OperationType? OperationType { get; private set; }

        public float? Result { get; private set; }

        public BinaryOperationState OperationState { get; private set; }

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

        public void Clear()
        {
            Operand1 = null;
            Operand2 = null;
            OperationType = null;
            Result = null;
            OperationState = BinaryOperationState.SettingOperand1;
        }

        private void SetState(object value)
        {
            if (value is OperationType)
                OperationState = BinaryOperationState.Operand1Setted;
            else if (value is float && OperationState is BinaryOperationState.Operand1Setted)
                OperationState = BinaryOperationState.OperationTypeSetted;
            else if (value is null)
                OperationState = BinaryOperationState.Operand2Setted;
            else if (OperationState is BinaryOperationState.Start && value is float)
                OperationState = BinaryOperationState.SettingOperand1;
            else if (OperationState is BinaryOperationState.ResultSetted && value is float)
                OperationState = BinaryOperationState.SettingOperand1;

            switch (OperationState)
            {
                case BinaryOperationState.SettingOperand1 when Result is not null:
                    Operand1 = Result;
                    Result = null;
                    break;
                case BinaryOperationState.SettingOperand1:
                    Operand1 = (float?)value;
                    break;
                case BinaryOperationState.Operand1Setted:
                    OperationType = (OperationType?)value;
                    break;
                case BinaryOperationState.OperationTypeSetted:
                    Operand2 = (float?)value;
                    break;
                case BinaryOperationState.Operand2Setted:
                    Result = GetResult();
                    Operand1 = null;
                    Operand2 = null;
                    OperationType = null;
                    OperationState = BinaryOperationState.ResultSetted;
                    break;
            }
        }

        private float GetResult() => OperationType!.Value switch
        {
            Domain.Shared.OperationType.Addition => Operand1!.Value + Operand2!.Value,
            Domain.Shared.OperationType.Subtraction => Operand1!.Value - Operand2!.Value,
            Domain.Shared.OperationType.Division => Operand1!.Value / Operand2!.Value,
            Domain.Shared.OperationType.Multiplication => Operand1!.Value * Operand2!.Value,
            _ => throw new NotImplementedException()
        };
    }
}