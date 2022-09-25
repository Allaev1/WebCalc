using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCalc.Domain.BinaryOperation;
using WebCalc.Domain.Shared;

namespace WebCalc.Domain.BinaryOperation
{
    public class BinaryOperationManager : IBinaryOperationManager
    {
        private BinaryOperation memoryOperation;
        private readonly UnaryOperation.UnaryOperation negationOperation;
        private readonly UnaryOperation.UnaryOperation percentageFrom;

        public BinaryOperation MainOperation { get; }

        public BinaryOperationManager()
        {
            MainOperation = new();
            memoryOperation = new();
            negationOperation = new(OperationType.Multiplication, -1);
            percentageFrom = new(OperationType.Multiplication, 0.01f);
        }

        public float GetNegateOperand()
        {
            if (MainOperation.OperationState is BinaryOperationState.SettingOperand1)
            {
                negationOperation.SetOperand(MainOperation.Operand1!.Value);
            }
            else
            {
                negationOperation.SetOperand(MainOperation.Operand2!.Value);
            }

            negationOperation.SetResult();
            MainOperation.SetOperand(negationOperation.Result);
            return negationOperation.Result!.Value;
        }

        public float GetMemoryAddResult(float operand)
        {
            if (memoryOperation.OperationState is not BinaryOperationState.ResultSetted) memoryOperation.SetOperand(0);
            else memoryOperation.SetOperand(memoryOperation.Result);

            memoryOperation.SetOperationType(OperationType.Addition);
            memoryOperation.SetOperand(operand);
            memoryOperation.SetResult();

            return memoryOperation.Result!.Value;
        }

        public float GetWithoutPercentage(int percentageOff)
        {
            var binaryOperation = new BinaryOperation();

            percentageFrom.SetOperand(percentageOff);
            percentageFrom.SetResult();
            var percentage = this.percentageFrom.Result!.Value;

            binaryOperation.SetOperand(MainOperation.Operand1!.Value);
            binaryOperation.SetOperationType(OperationType.Multiplication);
            binaryOperation.SetOperand(percentage);
            binaryOperation.SetResult();

            MainOperation.SetOperand(binaryOperation.Result);
            MainOperation.SetResult();

            return MainOperation.Result!.Value;
        }

        public void ClearMemory()
        {
            memoryOperation.Clear();
        }

        public float? ReadMemory() 
        {
            MainOperation.SetOperand(memoryOperation.Result);
            return memoryOperation.Result.HasValue? memoryOperation.Result.Value : null; 
        }
    }
}
