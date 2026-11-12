package Task2.cards;

import Task2.interfaces.BankCard;

public class BaseCard implements BankCard {
    @Override
    public String getCardDetails() {
        return "Базова банківська картка";
    }
    @Override
    public double getServiceCost() {
        return 10.0;
    }
}
