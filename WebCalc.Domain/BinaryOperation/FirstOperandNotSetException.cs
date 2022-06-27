using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCalc.Domain.BinaryOperation
{
    public class FirstOperandNotSetException : Exception
    {
        public FirstOperandNotSetException(string message)
            : base(message) { }
    }
}
