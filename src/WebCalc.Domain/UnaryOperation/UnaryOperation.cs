using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCalc.Domain.BinaryOperation;

namespace WebCalc.Domain.UnaryOperation
{
    public class UnaryOperation
    {
        private readonly float operand1;

        internal UnaryOperation(float operand1)
        {
            this.operand1 = operand1;
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
                    Result = operand1 * Operand2;
                    OperationState = UnaryOperationState.ResultSetted;
                    break;
            }
        }
    }
}
