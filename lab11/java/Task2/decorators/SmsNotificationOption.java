package Task2.decorators;

import Task2.interfaces.BankCard;

public class SmsNotificationOption extends CardDecorator{
    public SmsNotificationOption(BankCard card) {
        super(card);
    }
    @Override
    public String getCardDetails() {
        return super.getCardDetails() + ", SMS-інформування";
    }

    @Override
    public double getServiceCost() {
        return super.getServiceCost() + 5.0;
    }
}
