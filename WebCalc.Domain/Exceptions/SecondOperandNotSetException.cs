using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCalc.Domain.Exceptions
{
    public class SecondOperandNotSetException : Exception
    {
        public SecondOperandNotSetException(string message)
            : base(message) { }
    }
}
