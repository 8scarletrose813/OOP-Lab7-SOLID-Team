package Task2.student;

import Task2.interfaces.Account;
import Task2.interfaces.BankCard;
import Task2.interfaces.BankProductFactory;
import Task2.interfaces.TariffPlan;
public class StudentProductFactory implements BankProductFactory {
    @Override
    public BankCard createCard() {
        return new StudentCard();
    }
    @Override
    public Account createAccount() {
        return new StudentAccount();
    }
    @Override
    public TariffPlan createTariff() {
        return new StudentTariff();
    }
}
