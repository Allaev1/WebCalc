using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCalc.Domain.BinaryOperation;

namespace WebCalc.Domain.BinaryOperation
{
    public class BinaryOperationManager : IBinaryOperationManager
    {
        public BinaryOperation BinaryOperation { get; }
        public UnaryOperation.UnaryOperation NegationOperation { get; }
        public BinaryOperation MemoryOperation { get; }

        public BinaryOperationManager()
        {
            BinaryOperation = new();
            MemoryOperation = new();
            MemoryOperation.SetOperand(0);
            NegationOperation = new(OperationType.Multiplication, -1);
        }
    }
}
