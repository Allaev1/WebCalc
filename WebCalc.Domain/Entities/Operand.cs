using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCalc.Domain.Entities
{
    public class Operand
    {
        public double Value { get; private set; }

        public int Precision { get; private set; }

        internal Operand() { }

        public void SetValue(string stringValue)
        {
            if ((stringValue.Length > 1 && stringValue.First() == '0') || stringValue.Where(x => x == ',').Count() > 1)
                throw new Exception();
            Value = double.Parse(stringValue);
        }

        public void SetPercision(int precision)
        {
            Precision = precision;

            Value = Math.Round(Value, precision);
        }
    }
}
