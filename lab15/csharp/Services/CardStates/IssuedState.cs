using PrutCredit.Models;

namespace PrutCredit.Services.CardStates;

public class IssuedState : CardStateBase
{
    public override string Name => "випущена";

    protected override string Hint => "Спершу активуйте картку";

    public override void Activate(BankCard card)
    {
        Log(card, "Картку активовано");
        card.SetState(new ActiveState());
    }

    public override void BlockPermanently(BankCard card)
    {
        Log(card, "Картку заблоковано назавжди до активації");
        card.SetState(new PermanentlyBlockedState());
    }

    public override void Expire(BankCard card)
    {
        Log(card, "Термін дії закінчився до активації");
        card.SetState(new ExpiredState());
    }
}
