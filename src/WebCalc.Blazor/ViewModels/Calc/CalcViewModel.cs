using System;
using WebCalc.Application.BinaryOperation;
using WebCalc.Application.Contracts.BinaryOperation;
using WebCalc.Application.Contracts.Services.Formater;
using WebCalc.Application.Contracts.Services.InputValidationService;
using WebCalc.Application.Services.Formater;
using WebCalc.Application.Services.InputValidationService;
using WebCalc.Blazor.ViewModels.Base;
using WebCalc.Blazor.ViewModels.CalcDisplay;
using WebCalc.Domain.BinaryOperation;
using WebCalc.Domain.Shared;

namespace WebCalc.Blazor.ViewModels.Calc
{
    public class CalcViewModel : ViewModelBase, ICalcViewModel
    {
        private readonly IBinaryOperationAppService binaryOperationAppService;
        private readonly IInputValidationService inputValidationService;
        private readonly IFormater formater;
        private readonly ICalcDisplayViewModel displayViewModel;

        public CalcViewModel(
            IBinaryOperationAppService binaryOperationAppService, 
            IInputValidationService inputValidationService, 
            IFormater formater,
            ICalcDisplayViewModel displayViewModel)
        {
            this.binaryOperationAppService = binaryOperationAppService;
            this.inputValidationService = inputValidationService;
            this.formater = formater;
            this.displayViewModel = displayViewModel;
        }

        public ICalcDisplayViewModel? DisplayViewModel 
        { 
            get => displayViewModel; 
        }

        public void OnOperationTypeChanged(OperationType operationType)
        {
            binaryOperationAppService.SetOperationType(operationType);
        }

        public void OnValidOperandGenerated(float operand)
        {
            binaryOperationAppService.SetOperand(operand);
        }

        public void ClearOperations()
        {
            binaryOperationAppService.ClearOperations();
        }

        public async Task UpdateDisplayAsync(char value)
        {
            if (value == Constants.MEMORY_ADD)
            {
                var displayMemory = displayViewModel.Memory;
                var memory = binaryOperationAppService.GetUpdatedMemory(
                    float.Parse(displayViewModel.Value),
                    float.Parse(string.IsNullOrWhiteSpace(displayMemory) ? "0" : displayMemory));
                displayViewModel.SetMemory(memory.ToString());

                return;
            }
            else if (value == Constants.MEMORY_CLEAR)
            {
                displayViewModel.ClearMemory();

                return;
            }
            else if (value == Constants.MEMORY_READ)
            {
                displayViewModel.ReadMemory();
                return;
            }
            else if (inputValidationService.IsEditionAllowed(value, displayViewModel.Value)) return;
            else if (value == Constants.CLEAR)
            {
                displayViewModel.Clear();
                binaryOperationAppService.ClearOperations();

                return;
            }
            else if (IsChainingCalculation(value))
            {
                binaryOperationAppService.SetResult();
                var result = binaryOperationAppService.GetResult();
                binaryOperationAppService.SetOperand(result!.Value);
                displayViewModel.Clear();

                displayViewModel.Append(result.ToString()!.ToArray());
            }
            else if (value == '=' && displayViewModel.PercentageOff)
            {
                var result = binaryOperationAppService.GetNumberWithoutPercentage(int.Parse(displayViewModel.Value));
                displayViewModel.Append(value);
                displayViewModel.Append(result.ToString()!.ToArray());

                return;
            }
            else if (value == '=')
            {
                binaryOperationAppService.SetResult();
                var result = binaryOperationAppService.GetResult();
                displayViewModel.Append(value);
                var resultString = await formater.GetFormatedStringFromAsync((double)result!);
                displayViewModel.Append(resultString.ToArray());

                return;
            }
            else
            {
                if (binaryOperationAppService.GetState() is BinaryOperationState.ResultSetted && (value == '+' || value == '-' || value == '*' || value == '/'))
                {
                    displayViewModel.Clear();
                    displayViewModel.Append(binaryOperationAppService.GetResult().ToString()!.ToArray());
                    binaryOperationAppService.SetOperand(float.Parse(displayViewModel.Value));
                }

                if (binaryOperationAppService.GetState() is BinaryOperationState.ResultSetted && (char.IsDigit(value) || value == Constants.FLOATING_POINT))
                {
                    displayViewModel.Clear();
                    binaryOperationAppService.ClearOperations();
                }

                if (value == Constants.NEGATION_OPERATION_SIGN)
                {
                    binaryOperationAppService.NegateOperand();
                }
            }
            displayViewModel.Append(value);
        }

        private bool IsChainingCalculation(char value) =>
            (value == '+' || value == '-' || value == '*' || value == '/') &&
            binaryOperationAppService.GetOperand2() is not null;
    }
}
