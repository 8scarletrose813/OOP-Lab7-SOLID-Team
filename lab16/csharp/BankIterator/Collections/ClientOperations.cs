using BankIterator.Interfaces;
using BankIterator.Iterators;
using BankIterator.Models;

namespace BankIterator.Collections;

public sealed class ClientOperations : IOperationCollection
{
    private readonly Client _client;

    public ClientOperations(Client client) => _client = client;

    public IIterator<Operation> CreateIterator() => CreateNewestFirstIterator(int.MaxValue);

    public IIterator<Operation> CreateNewestFirstIterator(int limit) =>
        new NewestFirstIterator(Flatten(), limit);

    public IIterator<Operation> CreateYearIterator(int year) =>
        new YearChronologicalIterator(Flatten(), year);

    public IIterator<Operation> CreateThresholdIterator(decimal threshold) =>
        new ThresholdIterator(Flatten(), threshold);

    public string Describe()
    {
        var blocks = _client.Accounts.SelectMany(a => a.Blocks).ToList();
        var archived = blocks.Count(b => b.IsArchived);
        var operations = blocks.Sum(b => b.Operations.Count);
        return $"клієнт {_client.Name}, рахунків: {_client.Accounts.Count}, " +
               $"місячних блоків: {blocks.Count} (архівних: {archived}), операцій: {operations}";
    }

    private IEnumerable<Operation> Flatten() =>
        _client.Accounts
            .SelectMany(account => account.Blocks)
            .SelectMany(block => block.Operations);
}
