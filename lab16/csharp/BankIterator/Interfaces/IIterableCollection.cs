namespace BankIterator.Interfaces;

public interface IIterableCollection<out T>
{
    IIterator<T> CreateIterator();
}
