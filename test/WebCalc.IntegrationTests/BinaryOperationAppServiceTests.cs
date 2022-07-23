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
            displayValue = e;
        }

        private void BinaryOperationAppService_DisplayValueChanged(object? sender, string e)
        {
            expressionValue = e;
        }

        [Theory]
        [InlineData(new char[] { '1', '2', '3' }, "123")]
        [InlineData(new char[] { '0', '0', '1', '2', '3' }, "123")]
        [InlineData(new char[] { '1', ',', ',', '0' }, "120")]
        [InlineData(new char[] { ',', ',', '0' }, "0,0")]
        public void TestDisplayValue(char[] values, string expected)
        {
            foreach (var value in values)
            {
                if (char.IsDigit(value))
                    binaryOperationAppService.EditDisplayValue(value);
                else
                    binaryOperationAppService.EditExpressionValue(value);
            }

            displayValue.Should().Be(expected);
        }
    }
}
