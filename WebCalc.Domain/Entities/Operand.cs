using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCalc.Domain.Constants;
using WebCalc.Domain.Exceptions;

namespace WebCalc.Domain.Entities
{
    public class Operand
    {
        public double Value { get; private set; }

        public int Precision { get; private set; }

        internal Operand(int precision)
        {
            Precision = precision;
        }

        public void SetValue(string stringValue)
        {
            if (stringValue.Length > 1 && stringValue.First() == '0')
                throw new OperandWrongFormatException(ExceptionMessageConstants.FIRST_ZERO_MESSAGE);
            else if (stringValue.Length > 1 && stringValue.Substring(0, 2) == "--")
                throw new OperandWrongFormatException(ExceptionMessageConstants.TWO_MINUS_SIGN_MESSAGE);
            else if (stringValue.Where(x => x == ',').Count() > 1)
                throw new OperandWrongFormatException(ExceptionMessageConstants.TWO_FLOATING_POINT_MESSAGE);

            Value = double.Parse(stringValue);
        }

        public void SetPercision(int precision)
        {
            Precision = precision;

            Value = Math.Round(Value, precision);
        }
    }
}
