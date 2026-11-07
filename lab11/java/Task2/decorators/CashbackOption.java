package Task2.decorators;

import Task2.interfaces.BankCard;

public class CashbackOption extends CardDecorator{
    public CashbackOption(BankCard card) {
        super(card);
    }

    @Override
    public String getCardDetails() {
        return super.getCardDetails() + ", Кешбек";
    }

    @Override
    public double getServiceCost() {
        return super.getServiceCost() + 15.0;
    }
}
