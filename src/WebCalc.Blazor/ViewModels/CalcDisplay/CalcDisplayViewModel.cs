using System.ComponentModel;
using System.Runtime.CompilerServices;
using WebCalc.Application.BinaryOperation;
using WebCalc.Application.Contracts.BinaryOperation;
using WebCalc.Blazor.ViewModels.Base;
using WebCalc.Domain.Shared;

namespace WebCalc.Blazor.ViewModels.CalcDisplay
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

        public bool PercentageOff { get; private set; }

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

            Expression = GetValidExpression(Expression, @char);

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
            (string? firstOperand, char? operationType, string? secondOperand) = GetExpressionComponents(expression);

            if (operationType is not null && string.IsNullOrWhiteSpace(secondOperand) && (char.IsDigit(@char) || @char == Constants.FLOATING_POINT))
            {
                Value = INITIAL_STRING;
            }
        }

        private string GetValidExpression(string expression, char @char)
        {
            (string? firstOperand, char? operationType, string? secondOperand) = GetExpressionComponents(expression);

            if (@char == '=')
            {
                if (PercentageOff)
                {
                    PercentageOff = false;
                    return expression = $"{firstOperand}-{firstOperand}*0{Constants.FLOATING_POINT}{secondOperand}=";
                }
                else
                {
                    return expression += @char;
                }
            }
            else if (firstOperand is not null && @char == Constants.PERCENTAGE_OFF)
            {
                OnOperationTypeChanged?.Invoke(this, OperationType.Subtraction);
                operationType = '-';
                PercentageOff = true;
            }
            else if (firstOperand is not null && string.IsNullOrWhiteSpace(secondOperand) && (@char == '+' || @char == '-' || @char == '/' || @char == '*'))
            {
                OnOperationTypeChanged?.Invoke(this, GetOperationType(@char));
                operationType = @char;
            }
            else if (operationType is null)
            {
                if (@char == Constants.BACKSPACE)
                {
                    firstOperand = GetValidOperand(GetBackspaced(string.IsNullOrWhiteSpace(firstOperand) ? "0" : firstOperand));
                }
                else if (@char == Constants.NEGATION_OPERATION_SIGN)
                {
                    firstOperand = firstOperand!.Contains('-') ? firstOperand.Trim('(', ')', '-') : $"(-{firstOperand})";
                }
                else
                {
                    firstOperand = GetValidOperand(string.IsNullOrWhiteSpace(firstOperand) ? "0" : firstOperand, @char);
                }
            }
            else
            {
                var operand2 = binaryOperationAppService.GetOperand2();

                if (operand2 >= 0)
                {
                    secondOperand = Value;
                }
                else
                {
                    secondOperand = $"({Value})";
                }
            }

            return string.Concat(firstOperand, operationType, secondOperand);
        }

        private (string?, char?, string?) GetExpressionComponents(string expression)
        {
            var operands = new string[2];
            var operationTypeIndex = 0;
            if (expression.FirstOrDefault() == '(')
            {
                operands = expression.Split(new[] { ")-", ")+", ")/", ")*" }, 2, StringSplitOptions.RemoveEmptyEntries);

                if (operands[0].Last() != ')')
                {
                    operands[0] = $"{operands[0]})";
                }

                var tempIndex = expression.IndexOf(')') + 1;
                var operationType = expression.ElementAtOrDefault(tempIndex);

                if (operationType == default)
                {
                    operationTypeIndex = -1;
                }
                else
                {
                    operationTypeIndex = tempIndex;
                }
            }
            else
            {
                operands = expression.Split(new[] { '+', '-', '*', '/' }, 2);
                operationTypeIndex = expression.IndexOfAny(new[] { '+', '-', '*', '/' });
            }

            if (operands.ElementAtOrDefault(1) is string secondOperand)
            {
                return (operands[0], expression[operationTypeIndex], secondOperand);
            }
            else if (operationTypeIndex > 0)
            {
                return (operands[0], expression[operationTypeIndex], null);
            }
            else
            {
                return (operands[0], null, null);
            }
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

        private OperationType GetOperationType(char value)
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
                    throw new ArgumentException();
            }
        }
    }
}
