using System.Diagnostics.Contracts;
using System.Globalization;

namespace Kahoot.NET.Mathematics;

/// <summary>
/// Class to evaluate simple expressions using <see cref="TryEvaluate(ReadOnlySpan{char}, out long)"/> 
/// or <see cref="Evaluate(ReadOnlySpan{char})"/>. It performs this operation very fast and with no memory allocations what so ever on heap until the length of the expression
/// reaches beyond 512 and uses <see cref="System.Buffers.ArrayPool{T}"/> instead
/// </summary>
/// <remarks>
/// Expressions are very simple and designed by nature to make it faster,
/// so only simple expressions with simple operators and brackets
/// like + - * / ^ ( ).
/// </remarks>
public static unsafe class SimpleExpression
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static long ApplyOperation(char op, long left, long right)
    {
        return _operations[op].Function(left, right);
    }

    // keep in sync with below
    internal static readonly Dictionary<char, MathOperation> _operations = new()
    {
        { '+', new(&MathOperations.Add) },
        { '-', new(&MathOperations.Subtract) },
        { '*', new(&MathOperations.Multiply) },
        { '/', new(&MathOperations.Divide) },
        { '^', new(&MathOperations.Pow) }
    };

    internal static bool TryGetPrecedence(char ch, out int precedence)
    {
        switch (ch)
        {
            case '(':
                precedence = 0;
                return true;
            case '+':
            case '-':
                precedence = 1;
                return true;
            case '*':
            case '/':
                precedence = 2;
                return true;
            case '^':
                precedence = 3;
                return true;
            default:
                precedence = -1;
                return false;
        }
    }

    internal static int GetPrecedence(char ch)
    {
        _ = TryGetPrecedence(ch, out int precedence);
        return precedence;
    }

    /* This stack size count assumes that half of the expression contains numerics and the other half contains
     * operator symbols there is likely a better balanced amount because there are probably slightly more of one thing on average
    */

    internal const int OperandStackMaxSize = 256;
    internal const int OperatorStackMaxSize = 256;

    /// <summary>
    /// Tries to evaluate a long from a <see cref="ReadOnlySpan{T}"/> with a ideal 0 amount of heap allocations
    /// </summary>
    /// <param name="expression"></param>
    /// <param name="evaluated"></param>
    /// <returns>Whether a value could be evaluated or not</returns>
    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    public static bool TryEvaluate(ReadOnlySpan<char> expression, out long evaluated)
    {
        evaluated = 0; // zero evaluation

        if (expression.IsEmpty)
        {
            return true; // This is an edge case as it evaluates to zero
        }

        int length = expression.Length;

        int halfExpressionLength = length / 2;

        // Create stacks for operands and operators.
        using ValueStack<long> operandStack = halfExpressionLength > OperandStackMaxSize ?
            new(halfExpressionLength) :
            new(stackalloc long[halfExpressionLength]);

        using ValueStack<char> operatorStack = halfExpressionLength > OperatorStackMaxSize ?
            new(halfExpressionLength) :
            new(stackalloc char[halfExpressionLength]);

        for (int i = 0; (uint)i < (uint)length; i++)
        {
            char c = expression[i];

            // If the character is a digit, parse it as a long and push it onto the operand stack.
            if ((uint)(c - '0') <= 9)
            {
                int start = i;
                int take = 0;

                while (i < length && (uint)(expression[i] - '0') <= 9)
                {
                    take++;
                    i++;
                }

                i--;
                var operand = expression.Slice(start, take);

                // use TryParse instead because it allows for error handling

                if (!long.TryParse(operand, NumberStyles.None, null, out var result))
                {
                    return false;
                }

                operandStack.Push(result);
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
                    // this piece of code is copied around three times but cannot be turned into a function because the stacks
                    // have to have a using block to cleanup resources and we can't do that because we would have to pass the stack by a reference

                    // TODO: Figure out safe way to have this as function

                    var right = operandStack.Pop();
                    var left = operandStack.Pop();

                    var op = operatorStack.Pop();

                    operandStack.Push(ApplyOperation(op, left, right));
                }
                operatorStack.Pop();
            }
            // If the character is an operator, pop and apply all operators with higher or equal precedence.
            // Then push the operator onto the operator stack.
            else if (TryGetPrecedence(c, out var value))
            {
                while (operatorStack.Count > 0 && value <= GetPrecedence(operatorStack.Peek()))
                {
                    var right = operandStack.Pop();
                    var left = operandStack.Pop();

                    var op = operatorStack.Pop();

                    operandStack.Push(ApplyOperation(op, left, right));
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
            var right = operandStack.Pop();
            var left = operandStack.Pop();

            var op = operatorStack.Pop();

            operandStack.Push(ApplyOperation(op, left, right));
        }

        // The final result should be the only value remaining on the operand stack.
        evaluated = operandStack.Pop();

        return true;
    }

    /// <summary>
    /// Evaluates a simple mathematical string and returns the result, throws on format errors
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    /// <exception cref="FormatException"></exception>
    [Pure]
    public static long Evaluate(ReadOnlySpan<char> expression)
    {
        if (TryEvaluate(expression, out var eval))
        {
            return eval;
        }

        throw new FormatException("Input string was not in correct format");
    }
}
