package Task2.business;

import Task2.interfaces.Account;
import Task2.interfaces.BankCard;
import Task2.interfaces.BankProductFactory;
import Task2.interfaces.TariffPlan;

public class BusinessProductFactory implements BankProductFactory {
    @Override
    public BankCard createCard() {
        return new BusinessCard();
    }
    @Override
    public Account createAccount() {
        return new BusinessAccount();
    }
    @Override
    public TariffPlan createTariff() {
        return new BusinessTariff();
    }
}
