package Task2.premium;

import Task2.interfaces.Account;
import Task2.interfaces.BankCard;
import Task2.interfaces.BankProductFactory;
import Task2.interfaces.TariffPlan;
public class PremiumProductFactory implements BankProductFactory {
    @Override
    public BankCard createCard() {
        return new PremiumCard();
    }
    @Override
    public Account createAccount() {
        return new PremiumAccount();
    }
    @Override
    public TariffPlan createTariff() {
        return new PremiumTariff();
    }
}
