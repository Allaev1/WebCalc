using System.ComponentModel;
using System.Runtime.CompilerServices;
using WebCalc.Application.BinaryOperation;
using WebCalc.Application.Contracts.BinaryOperation;
using WebCalc.Blazor.ViewModels.Base;
using WebCalc.Domain.Shared;
using WebCalc.Domain.BinaryOperation;

namespace WebCalc.Blazor.ViewModels.Components.CalcDisplay
{
    public class CalcDisplayViewModel : ViewModelBase, ICalcDisplayViewModel
    {
        private const string INITIAL_STRING = "0";

        private bool memoryRead;

        private readonly IBinaryOperationAppService binaryOperationAppService;

        public CalcDisplayViewModel(IBinaryOperationAppService binaryOperationAppService)
        {
            this.binaryOperationAppService = binaryOperationAppService;
        }

        public event EventHandler<float>? OnValidOperandGenerated;
        public event EventHandler<OperationType>? OnOperationTypeChanged;
        public event EventHandler? MemorySetted;
        public event EventHandler? MemoryCleared;

        public string Value
        {
            get
            {
                return value;
            }
            private set
            {
                this.value = value;
                OnPropertyChanged();
            }
        }
        private string value = INITIAL_STRING;

        public string Memory
        {
            get
            {
                return memory;
            }
            private set
            {
                memory = value;
                OnPropertyChanged();
            }
        }
        private string memory = string.Empty;

        public string Expression
        {
            get
            {
                return expression;
            }
            private set
            {
                expression = value;
                OnPropertyChanged();
            }
        }
        private string expression = string.Empty;

        public bool PercentageOff { get; set; }

        public void SetMemory(string memory)
        {
            Memory = memory;
            MemorySetted?.Invoke(this, new());
        }

        public void ClearMemory()
        {
            Memory = string.Empty;
            MemoryCleared?.Invoke(this, new());
        }

        public void ReadMemory()
        {
            memoryRead = true;
            if (!string.IsNullOrWhiteSpace(Memory))
            {
                Value = Memory;
            }
        }

        public void Clear()
        {
            Value = INITIAL_STRING;
            Expression = string.Empty;
            memoryRead = false;
        }

        /// <summary>
        /// When passing "=" sign, value is cleared
        /// </summary>
        /// <param name="char"></param>
        /// <returns></returns>
        public void Append(char @char)
        {
            ClearValueIfNeeded(@char);

            var oldValue = Value;

            if (@char == Constants.BACKSPACE && memoryRead) return;

            if (@char == Constants.BACKSPACE)
            {
                Value = GetValidOperand(GetBackspaced(Value));
            }
            else
            {
                Value = GetValidOperand(memoryRead ? INITIAL_STRING : Value, @char);
                memoryRead = false;
            }

            if (oldValue != Value)
            {
                OnValidOperandGenerated?.Invoke(this, float.Parse(Value));
            }
            if (GetOperationTypeEnum(@char) is OperationType operationType)
            {
                OnOperationTypeChanged?.Invoke(this, operationType);
            }

            Expression = GetValidExpression(@char);

            if (@char == '=')
            {
                Value = INITIAL_STRING;
            }
        }

        public void Append(char[] chars)
        {
            var temp = new string(chars);

            Value = GetValidOperand(temp);
            if (!Expression.Contains('='))
            {
                Expression = GetValidOperand(temp);
            }
        }

        private void ClearValueIfNeeded(char @char)
        {
            if (binaryOperationAppService.GetState() is BinaryOperationState.Operand1Setted && (char.IsDigit(@char) || @char == Constants.FLOATING_POINT))
            {
                Value = INITIAL_STRING;
            }
        }

        private string GetValidExpression(char @char)
        {
            string? firstOperand = null;
            char? operationType = null;
            string? secondOperand = null;
            char? equationSign = null;

            var binaryOperationState = binaryOperationAppService.GetState();

            if (binaryOperationState is BinaryOperationState.ResultSetted)
            {
                return Expression;
            }

            switch (binaryOperationAppService.GetState())
            {
                case BinaryOperationState.Start:
                    firstOperand = Value;
                    break;
                case BinaryOperationState.SettingOperand1:
                    firstOperand = binaryOperationAppService.GetOperand1() >= 0 ? Value : $"({Value})";
                    break;
                case BinaryOperationState.Operand1Setted:
                    firstOperand = GetFirstOperandString();
                    operationType = GetOperationTypeChar();
                    break;
                case BinaryOperationState.OperationTypeSetted when PercentageOff && @char == '=':
                    firstOperand = GetFirstOperandString();
                    operationType = GetOperationTypeChar();
                    secondOperand = $"{binaryOperationAppService.GetOperand1()}*0{Constants.FLOATING_POINT}{Value}";
                    equationSign = '=';
                    break;
                case BinaryOperationState.OperationTypeSetted when PercentageOff:
                    firstOperand = GetFirstOperandString();
                    operationType = GetOperationTypeChar();
                    secondOperand = $"{binaryOperationAppService.GetOperand1()}*0{Constants.FLOATING_POINT}{Value}";
                    break;
                case BinaryOperationState.OperationTypeSetted when @char == '=':
                    firstOperand = GetFirstOperandString();
                    operationType = GetOperationTypeChar();
                    secondOperand = GetSecondOperandString();
                    equationSign = '=';
                    break;
                case BinaryOperationState.OperationTypeSetted:
                    firstOperand = GetFirstOperandString();
                    operationType = GetOperationTypeChar();
                    secondOperand = binaryOperationAppService.GetOperand2() >= 0 ? Value : $"({Value})";
                    break;
            }

            return string.Concat(firstOperand, operationType, secondOperand, equationSign);
        }

        private string GetValidOperand(string source, char? @char = null)
        {
            string temp = string.Empty;

            if (@char is null)
                temp = source;
            else if (source == INITIAL_STRING && @char != Constants.FLOATING_POINT)
                temp += @char;
            else if (@char == Constants.NEGATION_OPERATION_SIGN)
                temp = source.Contains('-') ? source.Trim('-') : $"-{source}";
            else if (@char == Constants.FLOATING_POINT && source.Contains(Constants.FLOATING_POINT))
                temp = source;
            else
                temp = source + @char;

            if (float.TryParse(temp, out float res))
                return temp;
            else
                return source;
        }

        private string GetBackspaced(string source)
        {
            var result = source.Substring(0, source.Length - 1);

            if (string.IsNullOrWhiteSpace(result))
            {
                result = "0";
            }

            return result;
        }

        private OperationType? GetOperationTypeEnum(char value)
        {
            switch (value)
            {
                case '+':
                    return OperationType.Addition;
                case '-':
                    return OperationType.Subtraction;
                case '*':
                    return OperationType.Multiplication;
                case '/':
                    return OperationType.Division;
                default:
                    return null;
            }
        }

        private char? GetOperationTypeChar()
        {
            switch (binaryOperationAppService.GetOperationType())
            {
                case OperationType.Addition:
                    return '+';
                case OperationType.Subtraction:
                    return '-';
                case OperationType.Multiplication:
                    return '*';
                case OperationType.Division: return '/';
                default:
                    return null;
            }
        }

        private string? GetFirstOperandString() => binaryOperationAppService.GetOperand1() >= 0 ? 
            binaryOperationAppService.GetOperand1().ToString() : 
            $"({binaryOperationAppService.GetOperand1()})";

        private string? GetSecondOperandString() => binaryOperationAppService.GetOperand2() >= 0 ?
            binaryOperationAppService.GetOperand2().ToString() : 
            $"({binaryOperationAppService.GetOperand2()})";
    }
}
