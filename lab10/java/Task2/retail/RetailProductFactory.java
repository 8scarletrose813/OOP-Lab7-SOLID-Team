package Task2.retail;

import Task2.interfaces.Account;
import Task2.interfaces.BankCard;
import Task2.interfaces.BankProductFactory;
import Task2.interfaces.TariffPlan;
public class RetailProductFactory implements BankProductFactory {
    @Override
    public BankCard createCard() {
        return new RetailCard();
    }
    @Override
    public Account createAccount() {
        return new RetailAccount();
    }
    @Override
    public TariffPlan createTariff() {
        return new RetailTariff();
    }
}
