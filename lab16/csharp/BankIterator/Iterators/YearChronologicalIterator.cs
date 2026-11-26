using BankIterator.Models;

namespace BankIterator.Iterators;

public sealed class YearChronologicalIterator : OperationIteratorBase
{
    private readonly int _year;

    public YearChronologicalIterator(IEnumerable<Operation> source, int year)
        : base(source.OrderBy(o => o.Date).ToList())
    {
        _year = year;
    }

    protected override bool Accepts(Operation operation) => operation.Date.Year == _year;
}
