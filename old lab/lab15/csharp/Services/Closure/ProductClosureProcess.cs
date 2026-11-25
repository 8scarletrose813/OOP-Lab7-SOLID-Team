using PrutCredit.Interfaces;
using PrutCredit.Models;

namespace PrutCredit.Services.Closure;

public abstract class ProductClosureProcess<TProduct> where TProduct : BankProduct
{
    private readonly ILogger _logger;

    protected ProductClosureProcess(ILogger logger)
    {
        _logger = logger;
    }

    public ClosureResult Close(TProduct product, Client requester)
    {
        Log($"Початок закриття: {product.DisplayName} {product.Id}");

        if (!IdentifyClient(product, requester))
        {
            return Stop("клієнта не ідентифіковано: особа не є власником продукту");
        }

        if (!CheckDebt(product))
        {
            return Stop($"наявна заборгованість {product.Debt:F2} грн");
        }

        var payout = SettleAndWithhold(product);

        CloseProduct(product);

        return IssueCertificate(product, payout);
    }

    protected abstract decimal FinalSettlement(TProduct product);

    protected virtual decimal WithholdTax(TProduct product, decimal payout)
    {
        Log("Утримання податку: не передбачено для цього продукту");
        return payout;
    }

    protected void Log(string message) => _logger.Log(GetType().Name, message);

    private bool IdentifyClient(TProduct product, Client requester)
    {
        Log("Крок 1/5: ідентифікація клієнта");

        if (requester.Id != product.Owner.Id)
        {
            return false;
        }

        Log($"Клієнта ідентифіковано: {requester.FullName}");
        return true;
    }

    private bool CheckDebt(TProduct product)
    {
        Log("Крок 2/5: перевірка заборгованості");

        if (product.Debt > 0)
        {
            return false;
        }

        Log("Заборгованості немає");
        return true;
    }

    private decimal SettleAndWithhold(TProduct product)
    {
        Log("Крок 3/5: фінальні розрахунки");

        var payout = FinalSettlement(product);
        payout = WithholdTax(product, payout);

        Log($"До виплати клієнту: {payout:F2} грн");
        return payout;
    }

    private void CloseProduct(TProduct product)
    {
        Log("Крок 4/5: закриття продукту");
        product.MarkClosed();
        Log($"{product.DisplayName} {product.Id} закрито");
    }

    private ClosureResult IssueCertificate(TProduct product, decimal payout)
    {
        Log("Крок 5/5: видача довідки");

        var certificate =
            $"Довідка: {product.DisplayName} №{product.Id} закрито. " +
            $"Власник: {product.Owner.FullName}. Виплачено: {payout:F2} грн";

        Log(certificate);
        return ClosureResult.Ok(certificate);
    }

    private ClosureResult Stop(string reason)
    {
        Log($"Закриття зупинено: {reason}");
        return ClosureResult.Fail(reason);
    }
}
