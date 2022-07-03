using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCalc.Domain.BinaryOperation.Exceptions
{
    public class OperationTypeNotSetException : Exception
    {
        public OperationTypeNotSetException(string message)
            : base(message) { }
    }
}
