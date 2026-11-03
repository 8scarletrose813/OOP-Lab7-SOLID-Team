package Task2.retail;

import Task2.interfaces.BankCard;
public class RetailCard implements BankCard {
    @Override
    public String getCardDetails() {
        return "Дебетова картка 'Стандарт'";
    }
}
