using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCalc.Domain.Constant.Exceptions
{
    public class ConstantNameDuplicatedException : Exception
    {
        public ConstantNameDuplicatedException(string message) :
            base(message)
        { }
    }
}
