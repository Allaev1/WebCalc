using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCalc.Domain.BinaryOperation
{
    //TODO: Rename
    public enum OperationState
    {
        Start,
        SettingOperand1,
        Operand1Setted,
        Operand2Setted,
        OperationTypeSetted,
        ResultSetted
    }
}
