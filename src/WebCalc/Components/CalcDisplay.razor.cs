using Microsoft.AspNetCore.Components;
using System.IO.Pipes;
using System.Reflection.Metadata;
using WebCalc.Domain.BinaryOperation;

namespace WebCalc.Components
{
    public partial class CalcDisplay
    {
        private const string INITIAL_STRING = "0";
        private string value = INITIAL_STRING;
        private string expression = string.Empty;
        private string memory = string.Empty;
        private bool showMemory;

        [Parameter]
        public int MaxDisplayCharsCount { get; set; } = 15;

        [Parameter]
        public int MaxFractionalDigitsCount { get; set; } = 9;

        [Parameter]
        public EventCallback<float> OnValidOperandGenerated { get; set; }

        [Parameter]
        public EventCallback<OperationType> OnOperationTypeChanged { get; set; }

        public void SetMemory(string memory)
        {
            this.memory = memory;
            showMemory = true;
        }

        public void ClearMemory()
        {
            this.memory = string.Empty;
            showMemory = false;
        }

        public string Value => value;

        public string Expression => expression;

        public string Memory => memory;

        public void Clear()
        {
            value = INITIAL_STRING;
            expression = string.Empty;

            StateHasChanged();
        }

        /// <summary>
        /// When passing "=" sign, value is cleared
        /// </summary>
        /// <param name="char"></param>
        /// <returns></returns>
        public async Task AppendAsync(char @char)
        {
            ClearValueIfNeeded(@char);

            var oldValue = value;

            if (@char == Constants.BACKSPACE)
            {
                value = GetValidOperand(GetBackspaced(value));
            }
            else
            {
                value = GetValidOperand(value, @char);
            }

            if (oldValue != value)
            {
                await OnValidOperandGenerated.InvokeAsync(float.Parse(value));
            }

            expression = GetValidExpression(expression, @char);

            if (@char == '=')
            {
                value = INITIAL_STRING;
            }

            StateHasChanged();
        }

        public void Append(char[] chars)
        {
            var temp = new string(chars);

            value = GetValidOperand(temp);

            StateHasChanged();
        }

        private void ClearValueIfNeeded(char @char)
        {
            (string? firstOperand, char? operationType, string? secondOperand) = GetExpressionComponents(expression);

            if (operationType is not null && string.IsNullOrWhiteSpace(secondOperand) && (char.IsDigit(@char) || @char==Constants.FLOATING_POINT))
            {
                value = INITIAL_STRING;
            }
        }

        private string GetValidExpression(string expression, char @char)
        {
            (string? firstOperand, char? operationType, string? secondOperand) = GetExpressionComponents(expression);

            if (@char == '=')
            {
                return expression += @char;
            }
            else if (firstOperand is not null && string.IsNullOrWhiteSpace(secondOperand) && (@char == '+' || @char == '-' || @char == '/' || @char == '*'))
            {
                OnOperationTypeChanged.InvokeAsync(GetOperationType(@char));
                operationType = @char;
            }
            else if (operationType is null)
            {
                firstOperand = GetValidOperand(firstOperand ?? string.Empty, @char);
            }
            else
            {
                secondOperand = GetValidOperand(secondOperand ?? string.Empty, @char);
            }

            return string.Concat(firstOperand, operationType, secondOperand);
        }

        private (string?, char?, string?) GetExpressionComponents(string expression)
        {
            var operands = expression.Split('+', '-', '*', '/');
            var operationTypeIndex = expression.IndexOfAny(new[] { '+', '-', '*', '/' });

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
