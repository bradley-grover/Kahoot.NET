using System;
using System.Buffers;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Kahoot.NET.Mathematics;

internal ref struct UnmanagedStack<T>
    where T : unmanaged
{
    internal Span<T> _buffer;
    internal T[]? _arrayToReturnToPool;
    internal int _pos;

    public UnmanagedStack(Span<T> initialBuffer)
    {
        _buffer = initialBuffer;
    }

    public int Count
    {
        get => _pos;
    }


    public int Capacity
    {
        get => _buffer.Length;
    }

    public void Clear()
    {
        _buffer.Clear();
        _pos = 0;
    }

    public T Peek()
    {
        int size = _pos - 1;

        Span<T> values = _buffer;

        if ((uint)size >= (uint)values.Length)
        {
            throw new InvalidOperationException("Stack is empty");
        }

        return values[size];
    }

    public bool TryPeek([MaybeNullWhen(false)] out T result)
    {
        int size = _pos - 1;
        Span<T> values = _buffer;

        if ((uint)size >= (uint)values.Length)
        {
            result = default;
            return false;
        }

        result = values[size];
        return true;
    }

    public T Pop()
    {
        int size = _pos - 1;
        Span<T> buffer = _buffer;

        if ((uint)size >= (uint)buffer.Length)
        {
            ThrowOnEmptyStack();
        }

        _pos = size;

        T item = buffer[size];

        return item;
    }

    public bool TryPop([MaybeNullWhen(false)] out T result)
    {
        int size = _pos - 1;
        Span<T> buffer = _buffer;

        if ((uint)size >= (uint)buffer.Length)
        {
            result = default!;
            return false;
        }

        _pos = size;

        result = buffer[size];

        return true;
    }

    public void Push(T item)
    {
        int size = _pos;
        Span<T> buffer = _buffer;

        if ((uint)size < (uint)buffer.Length)
        {
            buffer[size] = item;
            _pos = size + 1;
        }
        else
        {
            PushWithResize(item);
        }
    }


    [MethodImpl(MethodImplOptions.NoInlining)]
    private void Grow(int additionalCapacityBeyondPos)
    {
        Debug.Assert(additionalCapacityBeyondPos > 0);
        Debug.Assert(_pos > _buffer.Length - additionalCapacityBeyondPos, "Grow called incorrectly, no resize is needed.");

        const uint ArrayMaxLength = 0x7FFFFFC7; // same as Array.MaxLength

        // Increase to at least the required size (_pos + additionalCapacityBeyondPos), but try
        // to double the size if possible, bounding the doubling to not go beyond the max array length.
        int newCapacity = (int)Math.Max(
            (uint)(_pos + additionalCapacityBeyondPos),
            Math.Min((uint)_buffer.Length * 2, ArrayMaxLength));

        // Make sure to let Rent throw an exception if the caller has a bug and the desired capacity is negative.
        // This could also go negative if the actual required length wraps around.
        T[] poolArray = ArrayPool<T>.Shared.Rent(newCapacity);

        _buffer.Slice(0, _pos).CopyTo(poolArray);

        T[]? toReturn = _arrayToReturnToPool;
        _buffer = _arrayToReturnToPool = poolArray;

        if (toReturn != null)
        {
            ArrayPool<T>.Shared.Return(toReturn);
        }
    }


    private void PushWithResize(T item)
    {
        Debug.Assert(_pos == _buffer.Length);
        Grow(_pos + 1);
        _buffer[_pos] = item;
        _pos++;
    }

    public void Dispose()
    {
        T[]? toReturn = _arrayToReturnToPool;
        this = default;

        if (toReturn != null)
        {
            ArrayPool<T>.Shared.Return(toReturn);
        }
    }

    [DoesNotReturn]
    private void ThrowOnEmptyStack()
    {
        Debug.Assert(_pos == 0);
        throw new InvalidOperationException("The stack is empty");
    }
}
