#if NET7_0_OR_GREATER
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Kahoot.NET.Mathematics;

// unsafe adaptive feature, could be used to parse larger strings but is unreliable right now
internal static partial class SimpleExpression<TNumber>
    where TNumber : unmanaged, INumber<TNumber>
{
    static unsafe SimpleExpression()
    {
        if (sizeof(TNumber) > 8) // if the size is greater than a double we can't use standard Pow method
        {


            genericOpFunctions.Add('^', (left, right) =>
            {
                var bigLeft = BigInteger.CreateChecked(left);
                int exponent = int.CreateChecked(right);

                return TNumber.CreateChecked(BigInteger.Pow(bigLeft, exponent));
            });
        }
        else
        {
            genericOpFunctions.Add('^', (left, right) =>
            {
                return TNumber.CreateChecked(Math.Pow(double.CreateChecked(left), double.CreateChecked(right)));
            });
        }
    }

    [Pure]
    public static TNumber Evaluate(ReadOnlySpan<char> expression)
    {
        if (TryEvaluate(expression, out var eval))
        {
            return eval;
        }

        throw new FormatException("Input string was not in correct format");
    }


    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
    public static bool TryEvaluate(ReadOnlySpan<char> expression, out TNumber evaluated, NumberStyles numberStyle = NumberStyles.None, IFormatProvider? provider = null)
    {
        evaluated = TNumber.Zero; // zero evaluation

        if (expression.IsEmpty)
        {
            return true; // This is an edge case as it evaluates to zero
        }

        int halfExpressionLength = expression.Length / 2;

        // Create stacks for operands and operators.
        using var operandStack = halfExpressionLength > SimpleExpression.OperandStackMaxSize ?
            new ValueStack<TNumber>(halfExpressionLength) :
            new ValueStack<TNumber>(stackalloc TNumber[halfExpressionLength]);

        using var operatorStack = halfExpressionLength > SimpleExpression.OperatorStackMaxSize ?
            new ValueStack<char>(halfExpressionLength) :
            new ValueStack<char>(stackalloc char[halfExpressionLength]);

        ref char expressionRef = ref MemoryMarshal.GetReference(expression);

        int length = expression.Length;

        for (int i = 0; i < length; i++)
        {
            char c = Unsafe.Add(ref expressionRef, i);

            // If the character is a digit, parse it as a long and push it onto the operand stack.
            if ((uint)(c - '0') <= 9)
            {
                int start = i;
                int take = 0;

                while (i < length && (uint)(Unsafe.Add(ref expressionRef, i) - '0') <= 9 || Unsafe.Add(ref expressionRef, i) == '.')
                {
                    take++;
                    i++;
                }

                i--;
                var operand = expression.Slice(start, take);

                /// use TryParse instead because it allows for error handling

                if (!TNumber.TryParse(operand, numberStyle, provider, out var result))
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
            else if (SimpleExpression.TryGetPrecedence(c, out var value))
            {
                while (operatorStack.Count > 0 && value <= SimpleExpression.GetPrecedence(operatorStack.Peek()))
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

    private static readonly Dictionary<char, Func<TNumber, TNumber, TNumber>> genericOpFunctions = new()
    {
        { '+', (x, y) => x + y },
        { '-', (x, y) => x - y },
        { '*', (x, y) => x * y },
        { '/', (x, y) => x / y },
        // Add more operators here as needed.
    };


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static TNumber ApplyOperation(char op, TNumber left, TNumber right)
    {
        return genericOpFunctions[op](left, right);
    }

    internal const int OperandStackMaxSizeGeneric = 256;
    internal const int OperatorStackMaxSizeGeneric = 256;
}
#endif
