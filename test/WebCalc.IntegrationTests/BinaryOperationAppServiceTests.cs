using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCalc.Application.BinaryOperation;
using WebCalc.Application.Contracts.BinaryOperation;
using WebCalc.Domain.BinaryOperation;
using FluentAssertions;

namespace WebCalc.IntegrationTests
{
    public class BinaryOperationAppServiceTests
    {
        private readonly IBinaryOperationAppService binaryOperationAppService;
        private string displayValue = null!;
        private string expressionValue = null!;

        public BinaryOperationAppServiceTests()
        {
            var binaryOperationDomainManager = new BinaryOperationManager();

            binaryOperationAppService = new BinaryOperationAppService(binaryOperationDomainManager);
            binaryOperationAppService.DisplayValueChanged += BinaryOperationAppService_DisplayValueChanged;
            binaryOperationAppService.ExpressionValueChanged += BinaryOperationAppService_ExpressionValueChanged;
        }

        private void BinaryOperationAppService_ExpressionValueChanged(object? sender, string e)
        {
            expressionValue = e;
        }

        private void BinaryOperationAppService_DisplayValueChanged(object? sender, string e)
        {
            displayValue = e;
        }

        [Theory]
        [InlineData(new char[] { '1', '2', '3' }, "123")]
        [InlineData(new char[] { '0', '0', '1', '2', '3' }, "123")]
        [InlineData(new char[] { '1', ',', ',', '0' }, "1,0")]
        [InlineData(new char[] { ',', ',', '0' }, "0,0")]
        [InlineData(new char[] { '0', ',', ',', '0', '1', '2', ',', '0' }, "0,0120")]
        [InlineData(new char[] { '0', '0', '1', '2', ',', ',', '3', '0', ',', '4', '0' }, "12,3040")]
        [InlineData(new char[] { '1', '2', '3', '+' }, "123")]
        [InlineData(new char[] { '1', '2', '3', '+', '-', '/' }, "123")]
        [InlineData(new char[] { '1', '2', '3', '+', '1', '2' }, "12")]
        [InlineData(new char[] { '1', '2', '3', '+', '-', '/', '0', ',', '1', '2' }, "0,12")]
        [InlineData(new char[] { '1', '2', '3', '+', '-', '/', '0', '0', '1', '2', ',', ',', '3', '0', ',', '4', '0' }, "12,3040")]
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
        [InlineData(new char[] { '1', ',', ',', '4', ',', '0', Constants.BACKSPACE, Constants.BACKSPACE }, "1,")]
        [InlineData(new char[] { '1', '+', '1', Constants.BACKSPACE }, "0")]
        [InlineData(new char[] { '1', '+', '1', Constants.BACKSPACE, '=' }, "1")]
        [InlineData(new char[] { '1', '+', '2', Constants.CLEAR }, "0")]
        [InlineData(new char[] { '1', '+', '2', Constants.CLEAR, '1', '+', '3', '-', '5', '=' }, "-1")]
        [InlineData(new char[] { Constants.BACKSPACE, Constants.BACKSPACE, Constants.BACKSPACE }, "0")]
        [InlineData(new char[] { '1', '+', '1', '2', '3', '4', Constants.BACKSPACE, Constants.BACKSPACE, Constants.BACKSPACE }, "1")]
        [InlineData(new char[] { '1', '+', '2', '=', '4' }, "4")]
        [InlineData(new char[] { '1', '+', '2', '=', Constants.BACKSPACE, Constants.BACKSPACE, Constants.BACKSPACE }, "3")]
        [InlineData(new char[] { '1', '+', '2', '=', ',', '1' }, "0,1")]
        public void TestDisplayValue(char[] values, string expected)
        {
            foreach (var value in values)
            {
                binaryOperationAppService.EditValues(value);
            }

            displayValue.Should().Be(expected);
        }

        [Theory]
        [InlineData(new char[] { '1', '2', '3' }, "123")]
        [InlineData(new char[] { '0', '0', '1', '2', '3' }, "123")]
        [InlineData(new char[] { '1', ',', ',', '0' }, "1,0")]
        [InlineData(new char[] { ',', ',', '0' }, "0,0")]
        [InlineData(new char[] { '0', ',', ',', '0', '1', '2', ',', '0' }, "0,0120")]
        [InlineData(new char[] { '0', '0', '1', '2', ',', ',', '3', '0', ',', '4', '0' }, "12,3040")]
        [InlineData(new char[] { '1', '2', '3', '+' }, "123+")]
        [InlineData(new char[] { '1', '2', '3', '+', '-', '/' }, "123/")]
        [InlineData(new char[] { '1', '2', '3', '+', '-', '-' }, "123-")]
        [InlineData(new char[] { '1', '2', '3', '+', '1', '2' }, "123+12")]
        [InlineData(new char[] { '1', '2', '3', '+', '-', '/', '0', ',', '1', '2' }, "123/0,12")]
        [InlineData(new char[] { '1', '2', '3', '+', '-', '/', '0', '0', '1', '2', ',', ',', '3', '0', ',', '4', '0' }, "123/12,3040")]
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
        [InlineData(new char[] { '1', '+', '2', '=', ',', '1' }, "0,1")]
        public void TestExpressionValue(char[] values, string expected)
        {
            foreach (var value in values)
            {
                binaryOperationAppService.EditValues(value);
            }

            expressionValue.Should().Be(expected);
        }
    }
}
