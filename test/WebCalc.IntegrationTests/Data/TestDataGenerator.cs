using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCalc.Application.BinaryOperation;

namespace WebCalc.IntegrationTests.Data
{
    public class TestDataGenerator
    {
        public static IEnumerable<object[]> GetTestDataForValue()
        {
            yield return new object[] { new char[] { '1', '+', '2', '=' }, "3" };
            yield return new object[] { new char[] { '2', '+', '1', '=' }, "3" };
            yield return new object[] { new char[] { '1', '2', '3' }, "123" };
            yield return new object[] { new char[] { '0', '0', '1', '2', '3' }, "123" };
            yield return new object[] { new char[] { '1', Constants.FLOATING_POINT, Constants.FLOATING_POINT, '0' }, $"1{Constants.FLOATING_POINT}0" };
            yield return new object[] { new char[] { Constants.FLOATING_POINT, Constants.FLOATING_POINT, '0' }, $"0{Constants.FLOATING_POINT}0" };
            yield return new object[] { new char[] { '0', Constants.FLOATING_POINT, Constants.FLOATING_POINT, '0', '1', '2', Constants.FLOATING_POINT, '0' }, $"0{Constants.FLOATING_POINT}0120" };
            yield return new object[] { new char[] { '0', '0', '1', '2', Constants.FLOATING_POINT, Constants.FLOATING_POINT, '3', '0', Constants.FLOATING_POINT, '4', '0' }, $"12{Constants.FLOATING_POINT}3040" };
            yield return new object[] { new char[] { '1', '2', '3', '+' }, "123" };
            yield return new object[] { new char[] { '1', '2', '3', '+', '-', '/' }, "123" };
            yield return new object[] { new char[] { '1', '2', '3', '+', '1', '2' }, "12" };
            yield return new object[] { new char[] { '1', '2', '3', '+', '-', '/', '0', Constants.FLOATING_POINT, '1', '2' }, $"0{Constants.FLOATING_POINT}12" };
            yield return new object[] { new char[] { '1', '2', '3', '+', '-', '/', '0', '0', '1', '2', Constants.FLOATING_POINT, Constants.FLOATING_POINT, '3', '0', Constants.FLOATING_POINT, '4', '0' }, $"12{Constants.FLOATING_POINT}3040" };
            yield return new object[] { new char[] { '1', '+', '2', '=', '+', }, "3" };
            yield return new object[] { new char[] { '1', '+', '2', '=', '+', '2' }, "2" };
            yield return new object[] { new char[] { '1', '+', '2', '=', '+', '2', '=' }, "5" };
            yield return new object[] { new char[] { '1', '+', '3', '-' }, "4" };
            yield return new object[] { new char[] { '1', '+', '3', '-', '5' }, "5" };
            yield return new object[] { new char[] { '1', '+', '3', '-', '5', '=' }, "-1" };
            yield return new object[] { new char[] { '1', '2', '3', Constants.BACKSPACE }, "12" };
            yield return new object[] { new char[] { '1', '2', '3', Constants.BACKSPACE, Constants.BACKSPACE }, "1" };
            yield return new object[] { new char[] { '1', Constants.BACKSPACE, Constants.BACKSPACE }, "0" };
            yield return new object[] { new char[] { '1', '+', Constants.BACKSPACE }, "1" };
            yield return new object[] { new char[] { '1', Constants.FLOATING_POINT, Constants.FLOATING_POINT, '4', Constants.FLOATING_POINT, '0', Constants.BACKSPACE, Constants.BACKSPACE }, $"1{Constants.FLOATING_POINT}" };
            yield return new object[] { new char[] { '1', '+', '1', Constants.BACKSPACE }, "0" };
            yield return new object[] { new char[] { '1', '+', '1', Constants.BACKSPACE, '=' }, "1" };
            yield return new object[] { new char[] { '1', '+', '2', Constants.CLEAR }, "0" };
            yield return new object[] { new char[] { '1', '+', '2', Constants.CLEAR, '1', '+', '3', '-', '5', '=' }, "-1" };
            yield return new object[] { new char[] { Constants.BACKSPACE, Constants.BACKSPACE, Constants.BACKSPACE }, "0" };
            yield return new object[] { new char[] { '1', '+', '1', '2', '3', '4', Constants.BACKSPACE, Constants.BACKSPACE, Constants.BACKSPACE }, "1" };
            yield return new object[] { new char[] { '1', '+', '2', '=', '4' }, "4" };
            yield return new object[] { new char[] { '1', '+', '2', '=', Constants.BACKSPACE, Constants.BACKSPACE, Constants.BACKSPACE }, "3" };
            yield return new object[] { new char[] { '1', '+', '2', '=', Constants.FLOATING_POINT, '1' }, $"0{Constants.FLOATING_POINT}1" };
            yield return new object[] { new char[] { '1', '2', Constants.NEGATION_OPERATION_SIGN }, "-12" };
            yield return new object[] { new char[] { '1', '2', Constants.NEGATION_OPERATION_SIGN, Constants.NEGATION_OPERATION_SIGN }, "12" };
            yield return new object[] { new char[] { Constants.NEGATION_OPERATION_SIGN }, "0" };
            yield return new object[] { new char[] { '1', '2', '+', '1', '2', '3', Constants.NEGATION_OPERATION_SIGN }, "-123" };
            yield return new object[] { new char[] { '1', '2', '+', '1', '2', '3', Constants.NEGATION_OPERATION_SIGN, Constants.NEGATION_OPERATION_SIGN }, "123" };
            yield return new object[] { new char[] { '7', '2', Constants.MEMORY_ADD, '1', Constants.MEMORY_READ, Constants.BACKSPACE }, "72" };
            yield return new object[] { new char[] { '7', '2', Constants.MEMORY_ADD, '2', Constants.MEMORY_READ, Constants.BACKSPACE, '1', '3' }, "13" };
            yield return new object[] { new char[] { '1', '0', Constants.PERCENTAGE_OFF, '2', '5', '=' }, $"7{Constants.FLOATING_POINT}5" };
        }

        public static IEnumerable<object[]> GetTestDataForExpression()
        {
            yield return new object[] { new char[] { '1', '2', '3' }, "123" };
            yield return new object[] { new char[] { '0', '0', '1', '2', '3' }, "123" };
            yield return new object[] { new char[] { '1', Constants.FLOATING_POINT, Constants.FLOATING_POINT, '0' }, $"1{Constants.FLOATING_POINT}0" };
            yield return new object[] { new char[] { Constants.FLOATING_POINT, Constants.FLOATING_POINT, '0' }, $"0{Constants.FLOATING_POINT}0" };
            yield return new object[] { new char[] { '0', Constants.FLOATING_POINT, Constants.FLOATING_POINT, '0', '1', '2', Constants.FLOATING_POINT, '0' }, $"0{Constants.FLOATING_POINT}0120" };
            yield return new object[] { new char[] { '0', '0', '1', '2', Constants.FLOATING_POINT, Constants.FLOATING_POINT, '3', '0', Constants.FLOATING_POINT, '4', '0' }, $"12{Constants.FLOATING_POINT}3040" };
            yield return new object[] { new char[] { '1', '2', '3', '+' }, "123+" };
            yield return new object[] { new char[] { '1', '2', '3', '+', '-', '/' }, "123/" };
            yield return new object[] { new char[] { '1', '2', '3', '+', '-', '-' }, "123-" };
            yield return new object[] { new char[] { '1', '2', '3', '+', '1', '2' }, "123+12" };
            yield return new object[] { new char[] { '1', '2', '3', '+', '-', '/', '0', Constants.FLOATING_POINT, '1', '2' }, $"123/0{Constants.FLOATING_POINT}12" };
            yield return new object[] { new char[] { '1', '2', '3', '+', '-', '/', '0', '0', '1', '2', Constants.FLOATING_POINT, Constants.FLOATING_POINT, '3', '0', Constants.FLOATING_POINT, '4', '0' }, $"123/12{Constants.FLOATING_POINT}3040" };
            yield return new object[] { new char[] { '1', '+', '2', '=' }, "1+2=" };
            yield return new object[] { new char[] { '1', '+', '2', '=', '+', }, "3+" };
            yield return new object[] { new char[] { '1', '+', '2', '=', '+', '2' }, "3+2" };
            yield return new object[] { new char[] { '1', '+', '2', '=', '+', '2', '=' }, "3+2=" };
            yield return new object[] { new char[] { '1', '+', '3', '-' }, "4-" };
            yield return new object[] { new char[] { '1', '+', '3', '-', '5' }, "4-5" };
            yield return new object[] { new char[] { '1', '+', '3', '-', '5', '=' }, "4-5=" };
            yield return new object[] { new char[] { '1', '2', '3', Constants.BACKSPACE }, "12" };
            yield return new object[] { new char[] { '1', '2', '3', Constants.BACKSPACE, Constants.BACKSPACE }, "1" };
            yield return new object[] { new char[] { '1', Constants.BACKSPACE, Constants.BACKSPACE }, "0" };
            yield return new object[] { new char[] { '1', '+', Constants.BACKSPACE }, "1+" };
            yield return new object[] { new char[] { '1', '+', Constants.BACKSPACE, '2', '+' }, "3+" };
            yield return new object[] { new char[] { '1', '+', Constants.BACKSPACE, '2', '+', '4' }, "3+4" };
            yield return new object[] { new char[] { '1', '+', '1', Constants.BACKSPACE }, "1+0" };
            yield return new object[] { new char[] { '1', '+', '1', Constants.BACKSPACE, '=' }, "1+0=" };
            yield return new object[] { new char[] { '1', '+', '2', Constants.CLEAR }, "" };
            yield return new object[] { new char[] { '1', '+', '2', Constants.CLEAR, '1', '+', '3', '-', '5', '=' }, "4-5=" };
            yield return new object[] { new char[] { Constants.BACKSPACE, Constants.BACKSPACE, Constants.BACKSPACE }, "0" };
            yield return new object[] { new char[] { '1', '+', '1', '2', '3', '4', Constants.BACKSPACE, Constants.BACKSPACE, Constants.BACKSPACE }, "1+1" };
            yield return new object[] { new char[] { '1', '+', '2', '=', '4' }, "4" };
            yield return new object[] { new char[] { '1', '+', '2', '=', Constants.BACKSPACE, Constants.BACKSPACE, Constants.BACKSPACE }, "1+2=" };
            yield return new object[] { new char[] { '1', '+', '2', '=', Constants.FLOATING_POINT, '1' }, $"0{Constants.FLOATING_POINT}1" };
            yield return new object[] { new char[] { '1', '2', Constants.NEGATION_OPERATION_SIGN }, "(-12)" };
            yield return new object[] { new char[] { '1', '2', Constants.NEGATION_OPERATION_SIGN, Constants.NEGATION_OPERATION_SIGN }, "12" };
            yield return new object[] { new char[] { Constants.NEGATION_OPERATION_SIGN }, "" };
            yield return new object[] { new char[] { '1', '2', '+', '1', '2', '3', Constants.NEGATION_OPERATION_SIGN }, "12+(-123)" };
            yield return new object[] { new char[] { '1', '2', '+', '1', '2', '3', Constants.NEGATION_OPERATION_SIGN, Constants.NEGATION_OPERATION_SIGN }, "12+123" };
            yield return new object[] { new char[] { '1', '2', Constants.NEGATION_OPERATION_SIGN, '+', '1', '2', '3' }, "(-12)+123" };
            yield return new object[] { new char[] { '1', '0', Constants.PERCENTAGE_OFF }, "10-" };
            yield return new object[] { new char[] { '1', '0', Constants.PERCENTAGE_OFF, '2', '5', '=' }, $"10-10*0{Constants.FLOATING_POINT}25=" };
            yield return new object[] { new char[] { '1', '+', '2', Constants.NEGATION_OPERATION_SIGN, '3' }, "1+(-23)" };
            yield return new object[] { new char[] { '-' }, string.Empty };
        }
    }
}
