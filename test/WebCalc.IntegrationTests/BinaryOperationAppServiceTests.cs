using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCalc.Application.BinaryOperation;
using WebCalc.Application.Contracts.BinaryOperation;
using WebCalc.Domain.BinaryOperation;
using FluentAssertions;
using WebCalc.Components;
using Bunit;
using Microsoft.Extensions.DependencyInjection;

namespace WebCalc.IntegrationTests
{
    public class BinaryOperationAppServiceTests
    {
        [Theory]
        [InlineData(new char[] { '1', '2', '3' }, "123")]
        [InlineData(new char[] { '0', '0', '1', '2', '3' }, "123")]
        [InlineData(new char[] { '1', Constants.FLOATING_POINT, Constants.FLOATING_POINT, '0' }, "1,0")]
        [InlineData(new char[] { Constants.FLOATING_POINT, Constants.FLOATING_POINT, '0' }, "0,0")]
        [InlineData(new char[] { '0', Constants.FLOATING_POINT, Constants.FLOATING_POINT, '0', '1', '2', Constants.FLOATING_POINT, '0' }, "0,0120")]
        [InlineData(new char[] { '0', '0', '1', '2', Constants.FLOATING_POINT, Constants.FLOATING_POINT, '3', '0', Constants.FLOATING_POINT, '4', '0' }, "12,3040")]
        [InlineData(new char[] { '1', '2', '3', '+' }, "123")]
        [InlineData(new char[] { '1', '2', '3', '+', '-', '/' }, "123")]
        [InlineData(new char[] { '1', '2', '3', '+', '1', '2' }, "12")]
        [InlineData(new char[] { '1', '2', '3', '+', '-', '/', '0', Constants.FLOATING_POINT, '1', '2' }, "0,12")]
        [InlineData(new char[] { '1', '2', '3', '+', '-', '/', '0', '0', '1', '2', Constants.FLOATING_POINT, Constants.FLOATING_POINT, '3', '0', Constants.FLOATING_POINT, '4', '0' }, "12,3040")]
        [InlineData(new char[] { '1', '+', '2', '=' }, "3")]
        [InlineData(new char[] { '1', '+', '2', '=', '+', }, "3")]
        [InlineData(new char[] { '1', '+', '2', '=', '+', '2' }, "2")]
        [InlineData(new char[] { '1', '+', '2', '=', '+', '2', '=' }, "5")]
        [InlineData(new char[] { '1', '+', '3', '-' }, "4")]
        [InlineData(new char[] { '1', '+', '3', '-', '5' }, "5")]
        [InlineData(new char[] { '1', '+', '3', '-', '5', '=' }, "-1")]
        [InlineData(new char[] { '1', '2', '3', Constants.BACKSPACE }, "12")]
        [InlineData(new char[] { '1', '2', '3', Constants.BACKSPACE, Constants.BACKSPACE }, "1")]
        [InlineData(new char[] { '1', Constants.BACKSPACE, Constants.BACKSPACE }, "0")]
        [InlineData(new char[] { '1', '+', Constants.BACKSPACE }, "1")]
        [InlineData(new char[] { '1', Constants.FLOATING_POINT, Constants.FLOATING_POINT, '4', Constants.FLOATING_POINT, '0', Constants.BACKSPACE, Constants.BACKSPACE }, "1,")]
        [InlineData(new char[] { '1', '+', '1', Constants.BACKSPACE }, "0")]
        [InlineData(new char[] { '1', '+', '1', Constants.BACKSPACE, '=' }, "1")]
        [InlineData(new char[] { '1', '+', '2', Constants.CLEAR }, "0")]
        [InlineData(new char[] { '1', '+', '2', Constants.CLEAR, '1', '+', '3', '-', '5', '=' }, "-1")]
        [InlineData(new char[] { Constants.BACKSPACE, Constants.BACKSPACE, Constants.BACKSPACE }, "0")]
        [InlineData(new char[] { '1', '+', '1', '2', '3', '4', Constants.BACKSPACE, Constants.BACKSPACE, Constants.BACKSPACE }, "1")]
        [InlineData(new char[] { '1', '+', '2', '=', '4' }, "4")]
        [InlineData(new char[] { '1', '+', '2', '=', Constants.BACKSPACE, Constants.BACKSPACE, Constants.BACKSPACE }, "3")]
        [InlineData(new char[] { '1', '+', '2', '=', Constants.FLOATING_POINT, '1' }, "0,1")]
        [InlineData(new char[] { '1', '2', Constants.NEGATION_OPERATION_SIGN }, "-12")]
        [InlineData(new char[] { '1', '2', Constants.NEGATION_OPERATION_SIGN, Constants.NEGATION_OPERATION_SIGN }, "12")]
        [InlineData(new char[] { Constants.NEGATION_OPERATION_SIGN }, "0")]
        [InlineData(new char[] { '1', '2', '+', '1', '2', '3', Constants.NEGATION_OPERATION_SIGN }, "-123")]
        [InlineData(new char[] { '1', '2', '+', '1', '2', '3', Constants.NEGATION_OPERATION_SIGN, Constants.NEGATION_OPERATION_SIGN }, "123")]
        public void TestValue(char[] values, string expected)
        {
            using var context = new TestContext();
            context.Services.AddSingleton<IBinaryOperationManager>(new BinaryOperationManager());
            var calc = context.RenderComponent<Calc>();
            var buttons = calc.FindAll("button");
            var display = calc.Instance.display!;

            foreach (var value in values)
            {
                buttons.Single(x => x.Id == value.ToString()).Click(new());
            }

            display.Value.Should().Be(expected);
        }

        [Theory]
        [InlineData(new char[] { '1', '2', '3' }, "123")]
        [InlineData(new char[] { '0', '0', '1', '2', '3' }, "123")]
        [InlineData(new char[] { '1', Constants.FLOATING_POINT, Constants.FLOATING_POINT, '0' }, "1,0")]
        [InlineData(new char[] { Constants.FLOATING_POINT, Constants.FLOATING_POINT, '0' }, "0,0")]
        [InlineData(new char[] { '0', Constants.FLOATING_POINT, Constants.FLOATING_POINT, '0', '1', '2', Constants.FLOATING_POINT, '0' }, "0,0120")]
        [InlineData(new char[] { '0', '0', '1', '2', Constants.FLOATING_POINT, Constants.FLOATING_POINT, '3', '0', Constants.FLOATING_POINT, '4', '0' }, "12,3040")]
        [InlineData(new char[] { '1', '2', '3', '+' }, "123+")]
        [InlineData(new char[] { '1', '2', '3', '+', '-', '/' }, "123/")]
        [InlineData(new char[] { '1', '2', '3', '+', '-', '-' }, "123-")]
        [InlineData(new char[] { '1', '2', '3', '+', '1', '2' }, "123+12")]
        [InlineData(new char[] { '1', '2', '3', '+', '-', '/', '0', Constants.FLOATING_POINT, '1', '2' }, "123/0,12")]
        [InlineData(new char[] { '1', '2', '3', '+', '-', '/', '0', '0', '1', '2', Constants.FLOATING_POINT, Constants.FLOATING_POINT, '3', '0', Constants.FLOATING_POINT, '4', '0' }, "123/12,3040")]
        [InlineData(new char[] { '1', '+', '2', '=' }, "1+2=")]
        [InlineData(new char[] { '1', '+', '2', '=', '+', }, "3+")]
        [InlineData(new char[] { '1', '+', '2', '=', '+', '2' }, "3+2")]
        [InlineData(new char[] { '1', '+', '2', '=', '+', '2', '=' }, "3+2=")]
        [InlineData(new char[] { '1', '+', '3', '-' }, "4-")]
        [InlineData(new char[] { '1', '+', '3', '-', '5' }, "4-5")]
        [InlineData(new char[] { '1', '+', '3', '-', '5', '=' }, "4-5=")]
        [InlineData(new char[] { '1', '2', '3', Constants.BACKSPACE }, "12")]
        [InlineData(new char[] { '1', '2', '3', Constants.BACKSPACE, Constants.BACKSPACE }, "1")]
        [InlineData(new char[] { '1', Constants.BACKSPACE, Constants.BACKSPACE }, "0")]
        [InlineData(new char[] { '1', '+', Constants.BACKSPACE }, "1+")]
        [InlineData(new char[] { '1', '+', Constants.BACKSPACE, '2', '+' }, "3+")]
        [InlineData(new char[] { '1', '+', Constants.BACKSPACE, '2', '+', '4' }, "3+4")]
        [InlineData(new char[] { '1', '+', '1', Constants.BACKSPACE }, "1+0")]
        [InlineData(new char[] { '1', '+', '1', Constants.BACKSPACE, '=' }, "1+0=")]
        [InlineData(new char[] { '1', '+', '2', Constants.CLEAR }, "0")]
        [InlineData(new char[] { '1', '+', '2', Constants.CLEAR, '1', '+', '3', '-', '5', '=' }, "4-5=")]
        [InlineData(new char[] { Constants.BACKSPACE, Constants.BACKSPACE, Constants.BACKSPACE }, "0")]
        [InlineData(new char[] { '1', '+', '1', '2', '3', '4', Constants.BACKSPACE, Constants.BACKSPACE, Constants.BACKSPACE }, "1+1")]
        [InlineData(new char[] { '1', '+', '2', '=', '4' }, "4")]
        [InlineData(new char[] { '1', '+', '2', '=', Constants.BACKSPACE, Constants.BACKSPACE, Constants.BACKSPACE }, "1+2=")]
        [InlineData(new char[] { '1', '+', '2', '=', Constants.FLOATING_POINT, '1' }, "0,1")]
        [InlineData(new char[] { '1', '2', Constants.NEGATION_OPERATION_SIGN }, "(-12)")]
        [InlineData(new char[] { '1', '2', Constants.NEGATION_OPERATION_SIGN, Constants.NEGATION_OPERATION_SIGN }, "12")]
        [InlineData(new char[] { Constants.NEGATION_OPERATION_SIGN }, "")]
        [InlineData(new char[] { '1', '2', '+', '1', '2', '3', Constants.NEGATION_OPERATION_SIGN }, "12+(-123)")]
        [InlineData(new char[] { '1', '2', '+', '1', '2', '3', Constants.NEGATION_OPERATION_SIGN, Constants.NEGATION_OPERATION_SIGN }, "12+123")]
        [InlineData(new char[] { '1', '2', Constants.NEGATION_OPERATION_SIGN, '+', '1', '2', '3' }, "(-12)+123")]
        public void TestExpression(char[] values, string expected)
        {
            using var context = new TestContext();
            context.Services.AddSingleton<IBinaryOperationManager>(new BinaryOperationManager());
            var calc = context.RenderComponent<Calc>();
            var buttons = calc.FindAll("button");
            var display = calc.Instance.display!;

            foreach (var value in values)
            {
                buttons.Single(x => x.Id == value.ToString()).Click(new());
            }

            display.Expression.Should().Be(expected);
        }

        [Theory]
        [InlineData(new char[] { Constants.MEMORY_ADD }, "")]
        [InlineData(new char[] { '1', Constants.MEMORY_ADD }, "1")]
        [InlineData(new char[] { '1', Constants.MEMORY_ADD, '1', Constants.MEMORY_ADD }, "12")]
        public void TestMemory(char[] values, string expected)
        {
            using var context = new TestContext();
            context.Services.AddSingleton<IBinaryOperationManager>(new BinaryOperationManager());
            var calc = context.RenderComponent<Calc>();
            var buttons = calc.FindAll("button");
            var display = calc.Instance.display!;

            foreach (var value in values)
            {
                buttons.Single(x => x.Id == value.ToString()).Click(new());
            }

            display.Memory.Should().Be(expected);
        }
    }
}
