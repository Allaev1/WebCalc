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

        internal Operand() { }

        public void SetValue(string value)
        {
            if (value.Length > 1 && value.First() == '0')
                throw new OperandWrongFormatException(ExceptionMessageConstants.FIRST_ZERO_MESSAGE);
            else if (value.Length > 1 && value.Substring(0, 2) == "--")
                throw new OperandWrongFormatException(ExceptionMessageConstants.TWO_MINUS_SIGN_MESSAGE);
            else if (value.Where(x => x == SignConstants.FLOATING_POINT).Count() > 1)
                throw new OperandWrongFormatException(ExceptionMessageConstants.TWO_FLOATING_POINT_MESSAGE);

            Value = double.Parse(value);
        }

        public void SetValue(double? value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));
            Value = value.Value;
        }

        public void SetPercision(int precision)
        {
            Precision = precision;

            Value = Math.Round(Value, precision);
        }
    }
}
