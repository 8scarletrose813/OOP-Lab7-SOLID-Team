namespace BankIterator.Models;

public sealed class MonthBlock
{
    private readonly List<Operation> _operations = new();

    public MonthBlock(int year, int month)
    {
        Year = year;
        Month = month;
    }

    public int Year { get; }

    public int Month { get; }

    public bool IsArchived { get; private set; }

    public IReadOnlyList<Operation> Operations => _operations;

    public void Add(Operation operation) => _operations.Add(operation);

    public void Archive() => IsArchived = true;
}
