package Task2.decorators;

import Task2.interfaces.BankCard;

public class LoungeAccessOption extends CardDecorator{
    public LoungeAccessOption(BankCard card) {
        super(card);
    }
    @Override
    public String getCardDetails() {
        return super.getCardDetails() + ", Доступ до бізнес-залів";
    }

    @Override
    public double getServiceCost() {
        return super.getServiceCost() + 25.0;
    }
}
