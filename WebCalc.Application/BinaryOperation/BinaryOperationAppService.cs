using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCalc.Application.Contracts.BinaryOperation;
using WebCalc.Domain.BinaryOperation;

namespace WebCalc.Application.BinaryOperation
{
    public class BinaryOperationAppService : IBinaryOperationAppService
    {
        private readonly IBinaryOperationManager binaryOperationManager;
        private string displayValue = "0";

        public BinaryOperationAppService(IBinaryOperationManager binaryOperationManager)
        {
            this.binaryOperationManager = binaryOperationManager;
        }

        public event EventHandler<string> DisplayValueChanged = null!;

        public void SetDisplayValue(char value)
        {
            if (displayValue[0] == '0')
                displayValue = String.Empty;

            displayValue += value;

            if (DisplayValueChanged is not null)
                DisplayValueChanged.Invoke(this, displayValue);
        }

        public void SetOperand(string value)
        {
            binaryOperationManager.BinaryOperation.SetOperand(float.Parse(value));
        }

        public void SetOperationType(OperationType operationType)
        {
            binaryOperationManager.BinaryOperation.SetOperationType(operationType);
        }

        public string GetResult()
        {
            binaryOperationManager.BinaryOperation.SetResult();
            return binaryOperationManager.BinaryOperation.Result.ToString()!;
        }
    }
}
