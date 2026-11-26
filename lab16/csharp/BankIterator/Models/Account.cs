namespace BankIterator.Models;

public sealed class Account
{
    private readonly List<MonthBlock> _blocks = new();

    public Account(string number, string title)
    {
        Number = number;
        Title = title;
    }

    public string Number { get; }

    public string Title { get; }

    public IReadOnlyList<MonthBlock> Blocks => _blocks;

    public void Add(Operation operation)
    {
        var block = _blocks.FirstOrDefault(b => b.Year == operation.Date.Year && b.Month == operation.Date.Month);
        if (block is null)
        {
            block = new MonthBlock(operation.Date.Year, operation.Date.Month);
            _blocks.Add(block);
        }

        block.Add(operation);
    }

    public void ArchiveOlderThan(DateTime cutoff)
    {
        foreach (var block in _blocks.Where(b => new DateTime(b.Year, b.Month, 1) < cutoff))
        {
            block.Archive();
        }
    }
}
