package Task2;
import Task2.interfaces.*;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
public class BankBranch {
    private final String branchName;
    public BankBranch(String branchName) {
        this.branchName = branchName;
    }
    public void openClientPackage(String clientName, BankProductFactory factory){
        if (factory == null) throw new IllegalArgumentException("Фабрика не вказана!");
        BankCard bankCard = factory.createCard();
        Account bankAccount = factory.createAccount();
        TariffPlan tariffPlan = factory.createTariff();

        String time = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss"));
        System.out.println( "[" + time + "]" + "[" + branchName + "]" + "Відкрито новий пакет послуг для клієнта: " + clientName);

        System.out.println("Картка: " + bankCard.getCardDetails());
        System.out.println("Рахунок: " + bankAccount.getAccountDetails());
        System.out.println("Тариф: " + tariffPlan.getTariffDetails());
    }
}