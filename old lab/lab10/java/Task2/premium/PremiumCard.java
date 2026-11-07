package Task2.premium;

import Task2.interfaces.BankCard;
public class PremiumCard implements BankCard {
    @Override
    public String getCardDetails() {
        return "Картка 'Black Platinum'";
    }
}
