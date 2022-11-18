using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCalc.Domain.BinaryOperation;
using WebCalc.Domain.Shared;

namespace WebCalc.Domain.UnaryOperation
{
    public class UnaryOperation
    {
        private static UnaryOperation? percentageOperation;
        private static UnaryOperation? negationOperation;

        private float operand1;
        private readonly OperationType operationType;

        public static UnaryOperation GetPercentageOperation()
        {
            if (percentageOperation is null)
                percentageOperation = new(Shared.OperationType.Multiplication, 0.01f);
            return percentageOperation;
        }

        public static UnaryOperation GetNegationOperation()
        {
            if (negationOperation is null)
                negationOperation = new(Shared.OperationType.Multiplication, -1);
            return negationOperation;
        }

        private UnaryOperation(OperationType operationType, float operand1)
        {
            this.operand1 = operand1;
            this.operationType = operationType;
        }

        public float? Operand2 { get; private set; }

        public UnaryOperationState OperationState { get; private set; }

        public float? Result { get; private set; }

        public void SetResult()
        {
            SetState(null!);
        }

        public void SetOperand(float value)
        {
            SetState(value);
        }

        public void Clear()
        {
            Operand2 = null;
            OperationState = Domain.UnaryOperation.UnaryOperationState.SettingOperand2;
            Result = null;
        }

        private void SetState(object value)
        {
            if (value is null)
                OperationState = UnaryOperationState.Operand2Setted;
            else if (value is float && OperationState is UnaryOperationState.ResultSetted)
                OperationState = UnaryOperationState.SettingOperand2;

            switch (OperationState)
            {
                case UnaryOperationState.SettingOperand2:
                    Operand2 = (float?)value;
                    break;
                case UnaryOperationState.Operand2Setted:
                    Result = GetResult();
                    OperationState = UnaryOperationState.ResultSetted;
                    break;
                case UnaryOperationState.ResultSetted:
                    operand1 = Result!.Value;
                    Operand2 = (float?)value;
                    Result = null;
                    OperationState = UnaryOperationState.SettingOperand2;
                    break;
            }
        }

        private float GetResult() => operationType switch
        {
            OperationType.Addition => operand1 + Operand2!.Value,
            OperationType.Multiplication => operand1 * Operand2!.Value,
            _ => throw new ArgumentException("Incorrect operation type for unary operation")
        };
    }
}
