using BankIterator.Models;

namespace BankIterator.Iterators;

public sealed class ThresholdIterator : OperationIteratorBase
{
    private readonly decimal _threshold;

    public ThresholdIterator(IEnumerable<Operation> source, decimal threshold)
        : base(source.OrderBy(o => o.Date).ToList())
    {
        _threshold = threshold;
    }

    protected override bool Accepts(Operation operation) => Math.Abs(operation.Amount) > _threshold;
}
