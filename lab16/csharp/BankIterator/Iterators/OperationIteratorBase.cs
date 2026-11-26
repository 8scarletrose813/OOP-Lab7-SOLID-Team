using BankIterator.Interfaces;
using BankIterator.Models;

namespace BankIterator.Iterators;

public abstract class OperationIteratorBase : IIterator<Operation>
{
    private readonly IReadOnlyList<Operation> _items;
    private int _position;

    protected OperationIteratorBase(IReadOnlyList<Operation> items) => _items = items;

    protected virtual bool Accepts(Operation operation) => true;

    public bool HasNext()
    {
        while (_position < _items.Count && !Accepts(_items[_position]))
        {
            _position++;
        }

        return _position < _items.Count;
    }

    public Operation Next()
    {
        if (!HasNext())
        {
            throw new InvalidOperationException("Операцій більше немає.");
        }

        return _items[_position++];
    }

    public void Reset() => _position = 0;
}
