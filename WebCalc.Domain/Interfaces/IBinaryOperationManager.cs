using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCalc.Domain.Entities;

namespace WebCalc.Domain.Interfaces
{
    public interface IBinaryOperationManager
    {
        public BinaryOperation GetBinaryOperation();

        public BinaryOperation GetChainedBinaryOperation(BinaryOperation operation);
    }
}
