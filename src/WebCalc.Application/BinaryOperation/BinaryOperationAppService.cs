using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCalc.Application.Contracts.BinaryOperation;
using WebCalc.Domain.BinaryOperation;
using WebCalc.Domain.BinaryOperation.Exceptions;

namespace WebCalc.Application.BinaryOperation
{
    public class BinaryOperationAppService : IBinaryOperationAppService
    {
        private readonly IBinaryOperationManager binaryOperationManager;
        private string displayValue = "0";
        private string expressionValue = "0";
        private const char FLOATING_POINT = ',';

        public BinaryOperationAppService(IBinaryOperationManager binaryOperationManager)
        {
            this.binaryOperationManager = binaryOperationManager;
        }

        public event EventHandler<string> DisplayValueChanged = null!;

        public event EventHandler<string> ExpressionValueChanged = null!;

        public void EditValues(char value)
        {
            if (char.IsDigit(value) || value == FLOATING_POINT)
                EditDisplayValue(value);

            EditExpressionValue(value);

            if (char.IsDigit(value) || value == FLOATING_POINT)
                binaryOperationManager.BinaryOperation.SetOperand(float.Parse(displayValue.Last() == FLOATING_POINT ? displayValue + '0' : displayValue));
        }

        public void EditDisplayValue(char value)
        {
            displayValue = GetValidValue(displayValue, value);

            if (DisplayValueChanged is not null)
                DisplayValueChanged.Invoke(this, displayValue);
        }

        public void EditExpressionValue(char value)
        {
            expressionValue = GetValidValue(expressionValue, value);

            if (ExpressionValueChanged is not null)
                ExpressionValueChanged.Invoke(this, expressionValue);
        }

        private string GetValidValue(string source, char value)
        {
            string temp = string.Empty;

            if (source == "0" && value != FLOATING_POINT)
                temp += value;
            else
                temp = source + value;

            if (float.TryParse(temp, out float res))
                return temp;
            else
                return source;
        }
    }
}
