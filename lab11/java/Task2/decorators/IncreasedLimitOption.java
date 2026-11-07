package Task2.decorators;

import Task2.interfaces.BankCard;

public class IncreasedLimitOption extends CardDecorator{
    public IncreasedLimitOption(BankCard card) {
        super(card);
    }

    @Override
    public String getCardDetails() {
        return super.getCardDetails() + ", Підвищений ліміт";
    }

    @Override
    public double getServiceCost() {
        return super.getServiceCost() + 10.0;
    }
}
