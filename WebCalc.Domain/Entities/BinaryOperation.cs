using WebCalc.Domain.Constants;
using WebCalc.Domain.Enums;
using WebCalc.Domain.Exceptions;

namespace WebCalc.Domain.Entities
{
    public class BinaryOperation
    {
        public Operand FirstOperand { get; private set; } = null!;

        public Operand SecondOperand { get; private set; } = null!;

        public Operation? Operation { get; private set; }

        public double? Result { get; private set; }

        internal BinaryOperation() { }

        public void SetFirstOperand(string value)
        {
            if (FirstOperand is null)
                FirstOperand = new();

            FirstOperand.SetValue(value);
        }

        public void SetSecondOperand(string value)
        {
            if (Operation is null)
                throw new OperatorNotSetException(ExceptionMessageConstants.SET_OPERATOR_MESSAGE);

            if(SecondOperand is null)
                SecondOperand = new();

            SecondOperand.SetValue(value);
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
            Enums.Operation.Addition => FirstOperand.Value + SecondOperand.Value,
            Enums.Operation.Subtraction => FirstOperand.Value - SecondOperand.Value,
            Enums.Operation.Division => FirstOperand.Value / SecondOperand.Value,
            Enums.Operation.Multiplication => FirstOperand.Value * SecondOperand.Value,
            _ => throw new NotImplementedException()
        };
    }
}