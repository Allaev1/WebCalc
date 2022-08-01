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
            if (IsChainingOperation(value))
            {
                binaryOperationManager.BinaryOperation.SetResult();
                binaryOperationManager.BinaryOperation.SetOperand(binaryOperationManager.BinaryOperation.Result);
                SetOperationType(value);
                displayValue = binaryOperationManager.BinaryOperation.Operand1!.ToString()!;
                expressionValue = binaryOperationManager.BinaryOperation.Operand1!.ToString()! + value;

                if (DisplayValueChanged is not null && ExpressionValueChanged is not null)
                {
                    DisplayValueChanged.Invoke(this, displayValue);
                    ExpressionValueChanged.Invoke(this, expressionValue);
                }
            }
            else
            {
                if (char.IsDigit(value) || value == FLOATING_POINT || value == '=')
                    EditDisplayValue(value);

                EditExpressionValue(value);

                if (char.IsDigit(value) || value == FLOATING_POINT || value == '=')
                    binaryOperationManager.BinaryOperation.SetOperand(float.Parse(displayValue.Last() == FLOATING_POINT ? displayValue + '0' : displayValue));
            }
        }

        private void EditDisplayValue(char value)
        {
            if (value == '=')
            {
                binaryOperationManager.BinaryOperation.SetResult();
                displayValue = binaryOperationManager.BinaryOperation.Result!.ToString()!;
            }
            else if (binaryOperationManager.BinaryOperation.OperationType is not null && binaryOperationManager.BinaryOperation.Operand2 is null)
                displayValue = GetValidOperandString(string.Empty, value);
            else
                displayValue = GetValidOperandString(displayValue, value);

            if (DisplayValueChanged is not null)
                DisplayValueChanged.Invoke(this, displayValue);
        }

        private void EditExpressionValue(char value)
        {
            SetValidExpressionString(value);

            if (ExpressionValueChanged is not null)
                ExpressionValueChanged.Invoke(this, expressionValue);
        }

        private void SetValidExpressionString(char value)
        {
            if (value == '+' ||
                value == '-' ||
                value == '*' ||
                value == '/')
            {
                if (binaryOperationManager.BinaryOperation.OperationType is not null)
                    expressionValue = expressionValue.Replace(expressionValue.Last(), value);
                else if (expressionValue.Last() == '=')
                    expressionValue = binaryOperationManager.BinaryOperation.Operand1!.ToString()! + value;
                else
                    expressionValue += value;

                SetOperationType(value);
            }
            else if (binaryOperationManager.BinaryOperation.OperationType is not null &&
                value != '+' &&
                value != '*' &&
                value != '-' &&
                value != '/')
            {
                int? operationTypeIndex = GetOperationTypeIndex(binaryOperationManager.BinaryOperation.OperationType);

                if (operationTypeIndex is null)
                    throw new ArgumentNullException();

                var secondOperandString = expressionValue.Substring(operationTypeIndex.Value + 1, expressionValue.LastIndexOf(expressionValue.Last()) - operationTypeIndex.Value);

                var validSecondOperandString = GetValidOperandString(secondOperandString, value);

                expressionValue = expressionValue.Replace(
                    $"{expressionValue[operationTypeIndex.Value]}{secondOperandString}",
                    $"{expressionValue[operationTypeIndex.Value]}{validSecondOperandString}");
            }
            else if (value == '=') expressionValue += value;
            else expressionValue = GetValidOperandString(expressionValue, value);
        }

        private int? GetOperationTypeIndex(OperationType? operationType)
        {
            switch (operationType)
            {
                case OperationType.Addition:
                    return expressionValue.IndexOf('+');
                case OperationType.Division:
                    return expressionValue.IndexOf('/');
                case OperationType.Multiplication:
                    return expressionValue.IndexOf('*');
                case OperationType.Subtraction:
                    return expressionValue.IndexOf('-');
                default:
                    return null;
            }
        }

        private void SetOperationType(char value)
        {
            switch (value)
            {
                case '+':
                    binaryOperationManager.BinaryOperation.SetOperationType(OperationType.Addition);
                    break;
                case '-':
                    binaryOperationManager.BinaryOperation.SetOperationType(OperationType.Subtraction);
                    break;
                case '*':
                    binaryOperationManager.BinaryOperation.SetOperationType(OperationType.Multiplication);
                    break;
                case '/':
                    binaryOperationManager.BinaryOperation.SetOperationType(OperationType.Division);
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        private string GetValidOperandString(string source, char value)
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

        private bool IsChainingOperation(char value) =>
            (value == '+' ||
            value == '-' ||
            value == '*' ||
            value == '/') &&
            binaryOperationManager.BinaryOperation.Operand2 is not null;

    }
}
