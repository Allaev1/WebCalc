using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCalc.Domain.UnaryOperation
{
    public class UnaryOperation
    {
        internal UnaryOperation(float operand)
        {
            Operand1 = operand;
        }

        public float? Operand1 { get; }

        public float? Operand2 { get; private set; }

        public float? Result { get; private set; }

        public UnaryOperationState OperationState { get; private set; }

        public void SetResult()
        {
            SetState(null!);
        }

        public void SetOperand(float value)
        {
            SetState(value);
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
                    Result = Operand1 * Operand2;
                    OperationState = UnaryOperationState.ResultSetted;
                    break;
            }
        }
    }
}
