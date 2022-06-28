using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCalc.Domain.BinaryOperation.Exceptions
{
    public class OperationTypeSettedException : Exception
    {
        public OperationTypeSettedException(string message)
            : base(message) { }
    }
}
