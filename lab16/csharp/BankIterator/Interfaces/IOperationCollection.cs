using BankIterator.Models;

namespace BankIterator.Interfaces;

public interface IOperationCollection : IIterableCollection<Operation>
{
    IIterator<Operation> CreateNewestFirstIterator(int limit);

    IIterator<Operation> CreateYearIterator(int year);

    IIterator<Operation> CreateThresholdIterator(decimal threshold);

    string Describe();
}
