package Task2.business;

import Task2.interfaces.BankCard;
public class BusinessCard implements BankCard {
    @Override
    public String getCardDetails() {
        return "Корпоративна картка 'Business'";
    }
}
