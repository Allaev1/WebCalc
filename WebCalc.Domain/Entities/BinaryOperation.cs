using WebCalc.Domain.Constants;
using WebCalc.Domain.Enums;
using WebCalc.Domain.Exceptions;

namespace WebCalc.Domain.Entities
{
    public class BinaryOperation
    {
        public double? FirstOperand { get; set; }

        public double? SecondOperand { get; private set; }

        public Operation? Operation { get; private set; }

        public double? Result { get; private set; }

        internal BinaryOperation() { }

        public void SetSecondOperand(double operand)
        {
            if (Operation is null)
                throw new OperatorNotSetException(ExceptionMessageConstants.SET_OPERATOR_MESSAGE);

            SecondOperand = operand;
        }

        public void SetOperation(Operation operation)
        {
            if (FirstOperand is null)
                throw new FirstOperandNotSetException(ExceptionMessageConstants.SET_FIRST_OPERAND_MESSAGE);

            Operation = operation;
        }

        public void CalculateResult()
        {
            ValidateOperation();

            Result = GetResult();
        }

        private void ValidateOperation()
        {
            if (FirstOperand is null)
                throw new FirstOperandNotSetException(ExceptionMessageConstants.SET_FIRST_OPERAND_MESSAGE);
            if (Operation is null)
                throw new OperatorNotSetException(ExceptionMessageConstants.SET_OPERATOR_MESSAGE);
            if (SecondOperand is null)
                throw new SecondOperandNotSetException(ExceptionMessageConstants.SET_SECOND_OPERAND_MESSAGE);
        }

        private double? GetResult() => Operation switch
        {
            Enums.Operation.Addition => FirstOperand + SecondOperand,
            Enums.Operation.Subtraction => FirstOperand - SecondOperand,
            Enums.Operation.Division => FirstOperand / SecondOperand,
            Enums.Operation.Multiplication => FirstOperand * SecondOperand,
            _ => throw new NotImplementedException()
        };
    }
}