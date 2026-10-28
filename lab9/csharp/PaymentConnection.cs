namespace PrutCredit.Task2;

public sealed class PaymentConnection
{
    public string Id { get; }
    public string? SessionId { get; private set; }
    public Payment? CurrentPayment { get; private set; }
    public int PaymentsProcessed { get; private set; }

    internal PaymentConnection(int index)
    {
        Id = $"C-{index}";
        Establish();
    }

    /// <summary>Дорога операція: обмін сертифікатами, автентифікація, узгодження ключів.</summary>
    private void Establish()
    {
        Logger.Log(Id, "Встановлення захищеного з'єднання: сертифікати, автентифікація, узгодження ключів...");
        Thread.Sleep(300); // емуляція затримки
        SessionId = Guid.NewGuid().ToString("N")[..8];
        Logger.Log(Id, $"З'єднання встановлено, сесія {SessionId}");
    }

    public void Attach(Payment payment)
    {
        if (CurrentPayment is not null)
            throw new InvalidOperationException($"{Id} уже проводить платіж №{CurrentPayment.Number}");
        CurrentPayment = payment;
        PaymentsProcessed++;
    }

    /// <summary>Скидання стану перед повторною видачею: сума, рахунки, ідентифікатор операції. Сесія зберігається.</summary>
    internal void Reset() => CurrentPayment = null;
}
