using Microsoft.AspNetCore.Components;
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
        }

        public async Task AppendAsync(char @char)
        {
            var oldValue = value;
            value = GetValidOperand(value, @char);

            if (oldValue != value)
            {
                await OnValidOperandGenerated.InvokeAsync(float.Parse(value));
            }

            expression = GetValidExpression(expression, @char);

            StateHasChanged();
        }

        public async Task AppendAsync(char[] chars)
        {
            foreach (var @char in chars)
            {
                await AppendAsync(@char);
            }
        }

        private string GetValidExpression(string expression, char @char)
        {
            (string? firstOperand, char? operationType, string? secondOperand) = GetExpressionComponents(expression);

            if (secondOperand is not null)
            {
                secondOperand = GetValidOperand(secondOperand, @char);
            }
            else if (operationType is not null)
            {
                if (char.IsDigit(@char))
                {
                    secondOperand = GetValidOperand(string.Empty, @char);
                }
                else
                {
                    operationType = @char;
                }
            }
            else if (firstOperand is not null)
            {
                firstOperand = GetValidOperand(firstOperand, @char);
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

        private string GetValidOperand(string source, char @char)
        {
            string temp = string.Empty;

            if (source == INITIAL_STRING && @char != Constants.FLOATING_POINT)
                temp += @char;
            else
                temp = source + @char;

            if (float.TryParse(temp, out float res))
                return temp;
            else
                return source;
        }
    }
}
