using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

namespace Kahoot.NET.Mathematics;

public static class SimpleExpression
{

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static long ApplyOperation(char op, long left, long right)
    {
        return op switch
        {
            '+' => left + right,
            '-' => left - right,
            '*' => left * right,
            '/' => left / right,
            '^' => (long)Math.Pow(left, right),
            _ => throw new ArgumentException("Invalid operator", nameof(op)),
        };
    }


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

    public static long Evaluate(ReadOnlySpan<char> expression)
    {
        // Create stacks for operands and operators.
        var operandStack = new UnmanagedStack<long>(stackalloc long[expression.Length / 2]);
        var operatorStack = new UnmanagedStack<char>(stackalloc char[expression.Length / 2]);

        int length = expression.Length;

        for (int i = 0; i < length; i++)
        {
            char c = expression[i];

            // If the character is a digit, parse it as a long and push it onto the operand stack.
            if ((uint)(c - '0') <= 9)
            {
                int start = i;
                int take = 0;

                while (i < length && (uint)(c - '0') <= 9)
                {
                    take++;
                    i++;
                }

                i--;
                var operand = expression.Slice(start, take);

                // Manually convert the operand string to a long value which is slightly more dangerous than using the in built library
                operandStack.Push(long.Parse(operand, NumberStyles.None));
            }
            // If the character is an opening bracket, push it onto the operator stack.
            else if (c == '(')
            {
                operatorStack.Push(c);
            }
            // If the character is a closing bracket, pop and apply all operators until the matching opening bracket is found.
            else if (c == ')')
            {
                while (operatorStack.Peek() != '(')
                {
                    ApplyOperator(ref operandStack, operatorStack.Pop());
                }
                operatorStack.Pop();
            }
            // If the character is an operator, pop and apply all operators with higher or equal precedence.
            // Then push the operator onto the operator stack.
            else if (TryGetPrecedence(c, out var value))
            {
                while (operatorStack.Count > 0 && value <= GetPrecedence(operatorStack.Peek()))
                {
                    ApplyOperator(ref operandStack, operatorStack.Pop());
                }
                operatorStack.Push(c);
            }
            // If the character is anything else, it is an invalid character and we throw an exception.
            else
            {
                throw new ArgumentException("Invalid character in expression", nameof(expression));
            }
        }

        // Pop and apply all remaining operators.
        while (operatorStack.Count > 0)
        {
            ApplyOperator(ref operandStack, operatorStack.Pop());
        }

        // The final result should be the only value remaining on the operand stack.
        long ret = operandStack.Pop();

        operandStack.Dispose();
        operatorStack.Dispose();


        return ret;
    }


    private static void ApplyOperator(ref UnmanagedStack<long> operandStack, char op)
    {
        var right = operandStack.Pop();
        var left = operandStack.Pop();
        operandStack.Push(ApplyOperation(op, left, right));
    }
}
