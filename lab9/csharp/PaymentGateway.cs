namespace PrutCredit.Task2;

public sealed class PaymentGateway
{
    private readonly PaymentConnectionPool _pool;
    private readonly Queue<Payment> _queue = new();
    private readonly Dictionary<int, PaymentConnection> _active = new();

    public PaymentGateway(PaymentConnectionPool pool) => _pool = pool;

    public void Submit(Payment payment)
    {
        Logger.Log("Gateway", $"Надійшов платіж №{payment.Number}, {payment.AmountText}");
        if (_pool.TryAcquire(out var conn))
            Start(conn!, payment);
        else
        {
            _queue.Enqueue(payment);
            Logger.Log("Gateway", $"Вільних з'єднань немає → платіж №{payment.Number} у черзі (позиція {_queue.Count})");
        }
    }

    public void Complete(int paymentNumber)
    {
        if (!_active.Remove(paymentNumber, out var conn))
            throw new InvalidOperationException($"Платіж №{paymentNumber} не проводиться");
        Logger.Log("Gateway", $"Платіж №{paymentNumber} проведено");
        _pool.Release(conn);

        if (_queue.Count > 0 && _pool.TryAcquire(out var next))
            Start(next!, _queue.Dequeue());
    }

    public void CompleteAll()
    {
        while (_active.Count > 0)
            Complete(_active.Keys.Min());
    }

    private void Start(PaymentConnection conn, Payment payment)
    {
        conn.Attach(payment);
        _active[payment.Number] = conn;
        Logger.Log("Gateway", $"З'єднання {conn.Id} → платіж №{payment.Number}, {payment.AmountText}");
    }
}
