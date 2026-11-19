using Lab12_Variant7.Task3_TransactionHistory.Models;

namespace Lab12_Variant7.Task3_TransactionHistory.Services;

public class TransactionHistoryService
{
    private static readonly string[] Names =
    {
        "Сільпо", "АТБ", "Фора", "Метро", "Новус", "Аптека Доброго Дня", "WOG", "OKKO", "UPG", "Нова пошта",
        "Rozetka", "Епіцентр", "Comfy", "Фокстрот", "McDonald's", "Львівська майстерня шоколаду",
        "Кінотеатр Буковина", "Київстар", "Укрзалізниця", "Аптека Подорожник"
    };

    private static readonly string[] Categories =
    {
        "Супермаркети", "Супермаркети", "Супермаркети", "Супермаркети", "Супермаркети", "Аптеки", "АЗС", "АЗС", "АЗС",
        "Пошта", "Інтернет-магазини", "Будівництво", "Електроніка", "Електроніка", "Фастфуд", "Кафе",
        "Розваги", "Зв'язок", "Транспорт", "Аптеки"
    };

    private static readonly string[] MccCodes =
    {
        "5411", "5411", "5411", "5411", "5411", "5912", "5541", "5541", "5541", "4215",
        "5969", "5200", "5732", "5732", "5814", "5812", "7832", "4814", "4112", "5912"
    };

    private readonly MerchantFactory _factory;
    private readonly Random _random = new(7);

    public TransactionHistoryService(MerchantFactory factory)
    {
        _factory = factory;
    }

    public List<Transaction> Generate(int count)
    {
        var history = new List<Transaction>(count);
        var start = new DateTime(2026, 1, 1);

        for (int i = 0; i < count; i++)
        {
            int m = _random.Next(Names.Length);
            var merchant = _factory.Get(
                Names[m],
                $"logo_{m + 1}.png",
                Categories[m],
                MccCodes[m],
                $"вул. Головна, {m + 1}, Чернівці");

            var amount = Math.Round((decimal)(_random.NextDouble() * 2000 + 10), 2);
            var date = start.AddMinutes(_random.Next(0, 60 * 24 * 300));
            var account = $"UA{_random.Next(1000, 1100)}";

            history.Add(new Transaction(amount, date, account, merchant));
        }

        return history;
    }
}
