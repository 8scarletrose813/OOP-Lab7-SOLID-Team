using PrutCredit.Models;

namespace PrutCredit.Services.CardStates;

public class ActiveState : CardStateBase
{
    public override string Name => "активна";

    protected override string Hint => "Картка вже активна";

    public override void Pay(BankCard card, decimal amount, string pin)
    {
        if (!card.VerifyPin(pin))
        {
            var attempts = card.RegisterFailedPin();
            Log(card, $"Невірний PIN, спроба {attempts} з {BankCard.MaxPinAttempts}. Платіж {amount:F2} грн відхилено");

            if (attempts >= BankCard.MaxPinAttempts)
            {
                Log(card, "Перевищено ліміт спроб введення PIN");
                card.SetState(new TemporarilyBlockedState());
            }

            return;
        }

        card.ResetPinAttempts();

        if (!card.TryDebit(amount))
        {
            Log(card, $"Платіж {amount:F2} грн відхилено: недостатньо коштів (залишок {card.Balance:F2} грн)");
            return;
        }

        Log(card, $"Платіж {amount:F2} грн проведено, залишок {card.Balance:F2} грн");
    }

    public override void BlockPermanently(BankCard card)
    {
        Log(card, "Картку заблоковано назавжди");
        card.SetState(new PermanentlyBlockedState());
    }

    public override void Expire(BankCard card)
    {
        Log(card, "Термін дії картки закінчився");
        card.SetState(new ExpiredState());
    }
}
