using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCalc.Domain.Entities;
using WebCalc.Domain.Interfaces;

namespace WebCalc.Domain.DomainServices
{
    public class BinaryOperationManager : IBinaryOperationManager
    {
        public BinaryOperation GetBinaryOperation() => new BinaryOperation();

        public BinaryOperation GetChainedBinaryOperation(BinaryOperation operation)
        {
            var result = new BinaryOperation();

            operation.CalculateResult();
            result.FirstOperand.SetValue(operation.Result);

            return operation;
        }
    }
}
