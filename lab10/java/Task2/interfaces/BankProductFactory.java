package Task2.interfaces;

public interface BankProductFactory {
    BankCard createCard();
    Account createAccount();
    TariffPlan createTariff();
}
