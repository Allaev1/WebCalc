using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCalc.Domain.BinaryOperation
{
    public class OperandWrongFormatException : Exception
    {
        public OperandWrongFormatException(string message)
            : base(message) { }
    }
}
