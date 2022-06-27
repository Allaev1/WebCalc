using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCalc.Domain.BinaryOperation
{
    public class Operand1NotSetException : Exception
    {
        public Operand1NotSetException(string message)
            : base(message) { }
    }
}
