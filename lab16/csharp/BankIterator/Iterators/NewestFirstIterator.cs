using BankIterator.Models;

namespace BankIterator.Iterators;

public sealed class NewestFirstIterator : OperationIteratorBase
{
    public NewestFirstIterator(IEnumerable<Operation> source, int limit)
        : base(source.OrderByDescending(o => o.Date).Take(limit).ToList())
    {
    }
}
