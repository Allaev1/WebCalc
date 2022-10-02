using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCalc.Application.Contracts.BinaryOperation;
using WebCalc.Domain.BinaryOperation;
using WebCalc.Domain.Shared;

namespace WebCalc.Application.BinaryOperation
{
    public class BinaryOperationAppService : IBinaryOperationAppService
    {
        private readonly IBinaryOperationManager binaryOperationManager;

        public BinaryOperationAppService(IBinaryOperationManager binaryOperationManager)
        {
            this.binaryOperationManager = binaryOperationManager;
        }

        public void SetOperand(float operand)
        {
            binaryOperationManager.MainOperation.SetOperand(operand);
        }

        public void SetOperationType(OperationType operationType)
        {
            binaryOperationManager.MainOperation.SetOperationType(operationType);
        }

        public float GetResult()
        {
            binaryOperationManager.MainOperation.SetResult();
            return binaryOperationManager.MainOperation.Result!.Value;
        }
    }
}
