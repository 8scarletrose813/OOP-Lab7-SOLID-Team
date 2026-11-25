package Task2.model;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.HashMap;
import java.util.Map;

public class AccountLedger {
    private Map<String, Double> balances = new HashMap<>();
    public void openAccount(String accountId, double initialBalance){
        if (balances.containsKey(accountId)) {
            System.out.println("Помилка: Рахунок " + accountId + " вже існує.");
            return;
        }
        balances.put(accountId, initialBalance);
        String time = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss"));
        System.out.println("[" + time + "] [AccountLedger] Відкрито рахунок [" + accountId + "] з початковим балансом: " + initialBalance);
    }
    public void adjustBalance(String accountId, double delta){
        if (!balances.containsKey(accountId)) {
            System.out.println("Помилка: Рахунок " + accountId + " не існує.");
            return;
        }
        double currentBalance = balances.get(accountId);
        double newBalance = currentBalance + delta;
        balances.put(accountId, newBalance);
        String time = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss"));
        System.out.println("[" + time + "] [AccountLedger] Коригування рахунку " + accountId + " на " + delta + " грн. Новий залишок: " + newBalance + "грн");
    }
    public void printBalances(){
        String time = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss"));
        System.out.println( time + " [AccountLedger] Поточні залишки:");
        for (Map.Entry<String, Double> entry : balances.entrySet()) {
            System.out.println( time + " [AccountLedger] " + entry.getKey() + ": " + entry.getValue() + " грн");
        }
    }
    public LedgerMemento saveCheckpoint(String checkpointName) {
        String time = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss"));
        System.out.println("[" + time + "] [AccountLedger] Збережено контрольну точку: \"" + checkpointName + "\"");
        return new LedgerMemento(checkpointName, balances);
    }

    public void restore(LedgerMemento memento) {
        if (memento == null) {
            System.out.println("Помилка: Контрольна точка не знайдена.");
            return;
        }
        this.balances = memento.getSavedBalances();
        String time = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss"));
        System.out.println("[" + time + "] [AccountLedger] Відновлено стан книги рахунків на контрольну точку: \"" + memento.getCheckpointName() + "\"");
    }
}

