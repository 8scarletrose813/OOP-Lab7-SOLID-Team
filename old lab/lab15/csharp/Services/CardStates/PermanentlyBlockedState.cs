using PrutCredit.Models;

namespace PrutCredit.Services.CardStates;

public class PermanentlyBlockedState : CardStateBase
{
    public override string Name => "заблокована назавжди";

    protected override string Hint => "Розблокування неможливе, потрібен перевипуск картки";

    public override void Reissue(BankCard card)
    {
        card.ResetPinAttempts();
        Log(card, "Картку перевипущено");
        card.SetState(new IssuedState());
    }
}
