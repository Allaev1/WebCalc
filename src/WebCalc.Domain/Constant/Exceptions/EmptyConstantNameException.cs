using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCalc.Domain.Constant.Exceptions
{
    public class EmptyConstantNameException : Exception
    {
        public EmptyConstantNameException(string message)
            : base(message) { }
    }
}
