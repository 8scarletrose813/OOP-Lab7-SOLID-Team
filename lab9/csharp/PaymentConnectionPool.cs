namespace PrutCredit.Task2;

public sealed class PaymentConnectionPool
{
    private readonly object _sync = new();
    private readonly Queue<PaymentConnection> _free = new();   
    private readonly HashSet<PaymentConnection> _busy = new();
    private readonly List<PaymentConnection> _all = new();

    public int MaxSize { get; }
    public int CreatedCount { get { lock (_sync) return _all.Count; } }

    public PaymentConnectionPool(int maxSize)
    {
        if (maxSize <= 0) throw new ArgumentOutOfRangeException(nameof(maxSize));
        MaxSize = maxSize;
    }

    /// <summary>Видає вільне з'єднання, створює нове (якщо не досягнуто ліміту) або повертає false.</summary>
    public bool TryAcquire(out PaymentConnection? connection)
    {
        lock (_sync)
        {
            if (_free.Count > 0)
                connection = _free.Dequeue();
            else if (_all.Count < MaxSize)
            {
                connection = new PaymentConnection(_all.Count + 1);
                _all.Add(connection);
            }
            else
            {
                connection = null;
                return false;
            }
            _busy.Add(connection);
            return true;
        }
    }

    public void Release(PaymentConnection connection)
    {
        lock (_sync)
        {
            if (!_busy.Remove(connection))
                throw new InvalidOperationException($"{connection.Id} не видавалося пулом або вже повернене");
            connection.Reset();
            _free.Enqueue(connection);
            Logger.Log("Pool", $"{connection.Id} повернено, дані платежу очищено");
        }
    }

    public void PrintStatistics()
    {
        lock (_sync)
        {
            Logger.Log("Pool", "=== Статистика ===");
            foreach (var c in _all)
                Logger.Log("Pool", $"{c.Id}: платежів проведено — {c.PaymentsProcessed}");
            Logger.Log("Pool", $"Загальна кількість встановлених з'єднань: {_all.Count} (ліміт {MaxSize})");
        }
    }
}
