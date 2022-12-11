using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;

namespace Kahoot.NET.Mathematics;

public static class SimpleExpression
{

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static long ApplyOperation(char op, long left, long right)
    {
        return operatorFunctions[op](left, right);
    }

    private static readonly Dictionary<char, Func<long, long, long>> operatorFunctions = new()
    {
        { '+', (x, y) => x + y },
        { '-', (x, y) => x - y },
        { '*', (x, y) => x * y },
        { '/', (x, y) => x / y },
        { '^', (x, y) => x % y },
        // Add more operators here as needed.
    };

    private static readonly Dictionary<char, int> precedences = new()
    {
        { '(', 0 },
        { '+', 1 },
        { '-', 1 },
        { '*', 2 },
        { '/', 2 },
        { '^', 3 }
    };

    public static bool TryEvaluate(ReadOnlySpan<char> expression, out long evaluated)
    {
        evaluated = 0; // zero evaluation

        // Create stacks for operands and operators.
        var operandStack = new UnmanagedStack<long>(stackalloc long[expression.Length / 2]);
        var operatorStack = new UnmanagedStack<char>(stackalloc char[expression.Length / 2]);

        ref char expressionRef = ref MemoryMarshal.GetReference(expression);

        int length = expression.Length;

        for (int i = 0; i < length; i++)
        {
            char c = Unsafe.Add(ref expressionRef, i);

            // If the character is a digit, parse it as a long and push it onto the operand stack.
            if (char.IsDigit(c))
            {
                int start = i;
                int take = 0;

                while (i < length && char.IsDigit(Unsafe.Add(ref expressionRef, i)))
                {
                    take++;
                    i++;
                }

                i--;
                var operand = expression.Slice(start, take);

                if (!long.TryParse(operand, NumberStyles.None, null, out var result))
                {
                    return false;
                }

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
            else if (precedences.TryGetValue(c, out var value))
            {
                while (operatorStack.Count > 0 && value <= precedences[operatorStack.Peek()])
                {
                    ApplyOperator(ref operandStack, operatorStack.Pop());
                }
                operatorStack.Push(c);
            }
            // If the character is anything else, it is an invalid character and we throw an exception.
            else
            {
                return false;
            }
        }

        // Pop and apply all remaining operators.
        while (operatorStack.Count > 0)
        {
            ApplyOperator(ref operandStack, operatorStack.Pop());
        }

        // The final result should be the only value remaining on the operand stack.
        evaluated = operandStack.Pop();

        return true;
    }

    public static long Evaluate(ReadOnlySpan<char> expression)
    {
        if (TryEvaluate(expression, out var eval))
        {
            return eval;
        }

        throw new FormatException("Input string was not in correct format");
    }


    private static void ApplyOperator(scoped ref UnmanagedStack<long> operandStack, char op)
    {
        var right = operandStack.Pop();
        var left = operandStack.Pop();
        operandStack.Push(ApplyOperation(op, left, right));
    }
}
