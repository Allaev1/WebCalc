using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCalc.Application.Contracts.Services.InputValidationService;
using WebCalc.Application.Contracts.Services.Settings;
using WebCalc.Domain.BinaryOperation;

namespace WebCalc.Application.Services.InputValidationService
{
    public class InputValidationService : IInputValidationService
    {
        private readonly ISettings settings;

        public InputValidationService(
            ISettings settings)
        {
            this.settings = settings;
        }

        public bool IsEditionAllowed(
            char input,
            string value) =>
            TryToBackspaceResult(input) ||
            TryToBackspaceAndOperand2IsZero(input) ||
            TryToExceedMaxCountOfCharsOnDisplay(input, value) ||
            TryToNegateZero(input, value) ||
            TryToBackspaceOperationType(input);

        private bool TryToBackspaceResult(char input)
            => Domain.BinaryOperation.BinaryOperation.Instance().OperationState is BinaryOperationState.ResultSetted && input == Application.BinaryOperation.Constants.BACKSPACE;

        private bool TryToExceedMaxCountOfCharsOnDisplay(char input, string value)
            => value.Count() == settings.MaxDisplayCharsCount && (char.IsDigit(input) || input == Application.BinaryOperation.Constants.FLOATING_POINT);

        private bool TryToBackspaceAndOperand2IsZero(char value)
            => value == Application.BinaryOperation.Constants.BACKSPACE && Domain.BinaryOperation.BinaryOperation.Instance().Operand2 is 0;

        private bool TryToNegateZero(char input, string value)
            => input == Application.BinaryOperation.Constants.NEGATION_OPERATION_SIGN && value == "0";

        private bool TryToBackspaceOperationType(char value)
        => value == Application.BinaryOperation.Constants.BACKSPACE &&
            Domain.BinaryOperation.BinaryOperation.Instance().OperationType is not null &&
            Domain.BinaryOperation.BinaryOperation.Instance().Operand2 is null;
    }
}
