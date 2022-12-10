using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

namespace Kahoot.NET.Mathematics;

public static class SimpleExpression
{

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static double ApplyOperation(char op, double left, double right)
    {
        return op switch
        {
            '+' => left + right,
            '-' => left - right,
            '*' => left * right,
            '/' => left / right,
            '^' => Math.Pow(left, right),
            _ => throw new ArgumentException("Invalid operator", nameof(op)),
        };
    }

    private static ReadOnlySpan<char> Operators => new[] { '(', '+', '-', '*', '/', '^' };

    private static bool TryGetPrecedence(char operation, out int precedence)
    {
        switch (operation)
        {
            case '(':
                precedence = 0;
                break;
            case '+':
                precedence = 1;
                break;
            case '-':
                precedence = 1;
                break;
            case '*':
                precedence = 2;
                break;
            case '/':
                precedence = 2;
                break;
            case '^':
                precedence = 3;
                break;
            default:
                precedence = default;
                return false;
        }

        return true;
    }

    private static int GetPrecedence(char operation)
    {
        return operation switch
        {
            '(' => 0,
            '+' => 1,
            '-' => 1,
            '*' => 2,
            '/' => 2,
            '^' => 3,
            _ => throw new ArgumentException("Unknown operator")
        };
    }

    public static double Evaluate(ReadOnlySpan<char> expression)
    {
        // Convert the infix notation to RPN.
        var rpnQueue = new Queue<string>(expression.Length / 2);
        var operatorStack = new Stack<char>(expression.Length / 2);

        for (int i = 0; i < expression.Length; i++)
        {
            char c = expression[i];

            // If the character is a digit, parse it as a double and enqueue it to the RPN queue.
            if (char.IsDigit(c))
            {
                int start = i;
                int take = 0;

                while (i < expression.Length && (char.IsDigit(expression[i]) || expression[i] == '.'))
                {
                    take++;
                    i++;
                }

                i--;
                rpnQueue.Enqueue(new(expression.Slice(start, take)));
            }
            // If the character is an opening bracket, push it onto the operator stack.
            else if (c == '(')
            {
                operatorStack.Push(c);
            }
            // If the character is a closing bracket, pop and enqueue everything on the operator stack until we reach the matching opening bracket.
            else if (c == ')')
            {
                while (operatorStack.Peek() != '(')
                {
                    char op = operatorStack.Pop();
                    rpnQueue.Enqueue(op.ToString());
                }
                operatorStack.Pop();
            }
            // If the character is an operator, pop and enqueue everything on the operator stack that has a higher precedence. Then push the operator onto the operator stack.
            else if (TryGetPrecedence(c, out var value))
            {
                while (operatorStack.Count > 0 && value <= GetPrecedence(operatorStack.Peek()))
                {
                    char op = operatorStack.Pop();
                    rpnQueue.Enqueue(op.ToString());
                }

                operatorStack.Push(c);
            }
            // If the character is anything else, it is an invalid character and we throw an exception.
            else
            {
                throw new ArgumentException("Invalid character in expression", nameof(expression));
            }
        }


        // Pop and enqueue everything remaining on the operator stack.
        while (operatorStack.Count > 0)
        {
            char op = operatorStack.Pop();
            rpnQueue.Enqueue(op.ToString());
        }

        // Evaluate the RPN.
        var operandStack = new Stack<double>();

        while (rpnQueue.Count > 0)
        {
            string token = rpnQueue.Dequeue();

            // If the token is an operator, pop the required number of operands from the stack and apply the operator.
            // Then push the result back onto the stack.
            if (TryGetPrecedence(token[0], out _))
            {
                double right = operandStack.Pop();
                double left = operandStack.Pop();
                operandStack.Push(ApplyOperation(token[0], left, right));
            }
            // If the token is an operand, parse it as a double and push it onto the stack.
            else
            {
                if (double.TryParse(token.AsSpan(), out double result))
                {
                    operandStack.Push(result);
                }
                else
                {
                    throw new ArgumentException("Could not parse this numeric value");
                }
            }
        }

        return operandStack.Pop();
    }
}
