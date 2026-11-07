package Task2.decorators;

import Task2.interfaces.BankCard;

public abstract class CardDecorator implements BankCard {
    protected BankCard card;
    public CardDecorator(BankCard card){
        this.card = card;
    }

    @Override
    public String getCardDetails() {
        return card.getCardDetails();
    }

    @Override
    public double getServiceCost() {
        return card.getServiceCost();
    }
}
