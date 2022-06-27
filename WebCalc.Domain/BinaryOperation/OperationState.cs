using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCalc.Domain.BinaryOperation
{
    public enum OperationState
    {
        Operand1NotSet,
        Operand1Set,
        Operand2Set,
        OperationTypeSet
    }
}
