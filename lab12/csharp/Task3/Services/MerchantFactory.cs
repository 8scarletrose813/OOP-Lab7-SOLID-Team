using Lab12_Variant7.Common;
using Lab12_Variant7.Task3_TransactionHistory.Models;

namespace Lab12_Variant7.Task3_TransactionHistory.Services;

public class MerchantFactory
{
    private readonly Dictionary<string, Merchant> _merchants = new();

    public int Count => _merchants.Count;

    public Merchant Get(string name, string logo, string category, string mccCode, string address)
    {
        if (_merchants.TryGetValue(name, out var existing))
            return existing;

        var merchant = new Merchant(name, logo, category, mccCode, address);
        _merchants[name] = merchant;
        Logger.Log("Фабрика торговців", $"Створено об'єкт торговця '{name}'");
        return merchant;
    }
}
