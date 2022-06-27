using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCalc.Domain.BinaryOperation
{
    public class OperatorNotSetException : Exception
    {
        public OperatorNotSetException(string message)
            : base(message) { }
    }
}
