using PrutCredit.Models;

namespace PrutCredit.Services.CardStates;

public class TemporarilyBlockedState : CardStateBase
{
    public override string Name => "тимчасово заблокована";

    protected override string Hint => "Розблокуйте картку, щоб продовжити";

    public override void Unblock(BankCard card)
    {
        card.ResetPinAttempts();
        Log(card, "Картку розблоковано, лічильник спроб PIN скинуто");
        card.SetState(new ActiveState());
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
