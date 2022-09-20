using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCalc.Domain.UnaryOperation;

namespace WebCalc.Domain.BinaryOperation
{
    public interface IBinaryOperationManager
    {
        public BinaryOperation MainOperation { get; }

        public float GetNegateOperand();

        public float GetMemoryAddResult(float operand);

        public float GetWithoutPercentage(int percentageOff, float number);

        public void ClearMemory();

        public float? ReadMemory();
    }
}
