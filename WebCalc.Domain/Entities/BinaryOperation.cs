using WebCalc.Domain.Enums;
using WebCalc.Domain.Exceptions;

namespace WebCalc.Domain.Entities
{
    public class BinaryOperation
    {
        private const string SET_FIRST_OPERAND_MESSAGE = "Please set first operand";
        private const string SET_SECOND_OPERAND_MESSAGE = "Please set second operand";
        private const string SET_OPERATOR_MESSAGE = "Please set operator first";

        public double? FirstOperand { get; set; }

        public double? SecondOperand { get; private set; }

        public Operator? Operator { get; private set; }

        public double? Result { get; private set; }

        internal BinaryOperation() { }

        public void SetSecondOperand(double operand)
        {
            if (Operator is null)
                throw new OperatorNotSetException(SET_OPERATOR_MESSAGE);

            SecondOperand = operand;
        }

        public void SetOperator(Operator op)
        {
            if (FirstOperand is null)
                throw new FirstOperandNotSetException(SET_FIRST_OPERAND_MESSAGE);

            Operator = op;
        }

        public void CalculateResult()
        {
            ValidateOperation();

            Result = GetResult();
        }

        private void ValidateOperation()
        {
            if (FirstOperand is null)
                throw new FirstOperandNotSetException(SET_FIRST_OPERAND_MESSAGE);
            if (Operator is null)
                throw new OperatorNotSetException(SET_OPERATOR_MESSAGE);
            if (SecondOperand is null)
                throw new SecondOperandNotSetException(SET_SECOND_OPERAND_MESSAGE);
        }

        private double? GetResult() => Operator switch
        {
            Enums.Operator.Addition => FirstOperand + SecondOperand,
            Enums.Operator.Subtraction => FirstOperand - SecondOperand,
            Enums.Operator.Division => FirstOperand / SecondOperand,
            Enums.Operator.Multiplication => FirstOperand * SecondOperand,
            _ => throw new NotImplementedException()
        };
    }
}