using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCalc.Application.BinaryOperation
{
    public static class Constants
    {
        public const char BACKSPACE = 'x';

        public const char CLEAR = 'C';

        public static char FLOATING_POINT { get; } = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator.Single();

        public const char NEGATION_OPERATION_SIGN = '±';

        public const char MEMORY_ADD = '!';

        public const char MEMORY_READ = '&';

        public const char MEMORY_CLEAR = '$';

        public const char PERCENTAGE_OFF = '%';
    }
}
