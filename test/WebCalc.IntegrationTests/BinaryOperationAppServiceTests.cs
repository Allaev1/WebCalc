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
