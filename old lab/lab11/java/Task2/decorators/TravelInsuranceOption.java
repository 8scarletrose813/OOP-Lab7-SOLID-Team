package Task2.decorators;

import Task2.interfaces.BankCard;

public class TravelInsuranceOption extends CardDecorator{
    public TravelInsuranceOption(BankCard card) {
        super(card);
    }
    @Override
    public String getCardDetails() {
        return super.getCardDetails() + ", Страхування подорожей";
    }

    @Override
    public double getServiceCost() {
        return super.getServiceCost() + 50.0;
    }
}
