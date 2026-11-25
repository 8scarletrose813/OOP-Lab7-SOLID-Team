using PrutCredit.Models;

namespace PrutCredit.Services.CardStates;

public class ExpiredState : CardStateBase
{
    public override string Name => "термін дії закінчився";

    protected override string Hint => "Потрібен перевипуск картки";

    public override void Reissue(BankCard card)
    {
        card.ResetPinAttempts();
        Log(card, "Картку перевипущено");
        card.SetState(new IssuedState());
    }
}
