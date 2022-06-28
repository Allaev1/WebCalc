using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCalc.Domain.BinaryOperation.Exceptions
{
    public class Operand1SettedException : Exception
    {
        public Operand1SettedException(string message)
            : base(message) { }
    }
}
