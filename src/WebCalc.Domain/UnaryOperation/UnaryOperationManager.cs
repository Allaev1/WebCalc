using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCalc.Domain.UnaryOperation
{
    public class UnaryOperationManager : IUnaryOperationManager
    {
        public UnaryOperation Percentage { get; } = new(Shared.OperationType.Multiplication, 0.01f);

        public UnaryOperation Negate { get; } = new(Shared.OperationType.Multiplication, -1);
    }
}
