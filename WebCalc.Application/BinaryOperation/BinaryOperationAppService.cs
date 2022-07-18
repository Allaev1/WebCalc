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

        public BinaryOperationAppService(IBinaryOperationManager binaryOperationManager)
        {
            this.binaryOperationManager = binaryOperationManager;
        }

        public event EventHandler<string> DisplayValueChanged = null!;

        public event EventHandler<string> ExpressionValueChanged = null!;

        public void EditDisplayValue(char value)
        {
            if (displayValue[0] == '0' || binaryOperationManager.BinaryOperation.OperationType is not null && binaryOperationManager.BinaryOperation.Operand2 is null)
                displayValue = String.Empty;
            if (value == '=')
            {
                binaryOperationManager.BinaryOperation.SetResult();
                displayValue = binaryOperationManager.BinaryOperation.Result.ToString()!;
            }
            else
                displayValue += value;

            binaryOperationManager.BinaryOperation.SetOperand(float.Parse(displayValue));

            if (DisplayValueChanged is not null)
                DisplayValueChanged.Invoke(this, displayValue);
            EditExpressionValue(value);
        }

        public void EditExpressionValue(char value)
        {
            if (expressionValue[0] == '0')
                expressionValue = String.Empty;
            if (value != '=' && binaryOperationManager.BinaryOperation.OperationState is OperationState.ResultSetted)
                expressionValue = binaryOperationManager.BinaryOperation.Operand1.ToString()!;
            if (binaryOperationManager.BinaryOperation.OperationType is not null && binaryOperationManager.BinaryOperation.Operand2 is null)
                expressionValue = expressionValue.Substring(0, expressionValue.Length - 1);

            switch (value)
            {
                case '+':
                    binaryOperationManager.BinaryOperation.SetOperationType(OperationType.Addition);
                    break;
                case '/':
                    binaryOperationManager.BinaryOperation.SetOperationType(OperationType.Division);
                    break;
                case '*':
                    binaryOperationManager.BinaryOperation.SetOperationType(OperationType.Multiplication);
                    break;
                case '-':
                    binaryOperationManager.BinaryOperation.SetOperationType(OperationType.Subtraction);
                    break;
            }

            expressionValue += value;

            if (ExpressionValueChanged is not null)
                ExpressionValueChanged.Invoke(this, expressionValue);
        }
    }
}
