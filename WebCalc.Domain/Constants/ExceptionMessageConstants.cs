using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCalc.Domain.Constants
{
    public static class ExceptionMessageConstants
    {
        public const string SET_FIRST_OPERAND_MESSAGE = "Please set first operand";
        public const string SET_SECOND_OPERAND_MESSAGE = "Please set second operand";
        public const string SET_OPERATOR_MESSAGE = "Please set operator first";

        public const string FIRST_ZERO_MESSAGE = "First digit cannot be zero";
        public const string TWO_FLOATING_POINT_MESSAGE = "Two floating point sign";
        public const string TWO_MINUS_SIGN_MESSAGE = "Two minus sign";
    }
}
