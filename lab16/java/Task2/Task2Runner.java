package Task2;

import Task2.history.DayHistory;
import Task2.model.AccountLedger;

public class Task2Runner {

    public static void runScenario() {
        AccountLedger ledger = new AccountLedger();
        DayHistory history = new DayHistory();

        ledger.openAccount("UA1001", 50000.0);
        ledger.openAccount("UA1002", 120000.0);
        ledger.openAccount("UA1003", 30000.0);
        ledger.printBalances();

        history.save(ledger.saveCheckpoint("Початок дня"));

        ledger.adjustBalance("UA1001", 15000.0);
        ledger.adjustBalance("UA1002", -20000.0);
        ledger.printBalances();

        history.save(ledger.saveCheckpoint("Перед закриттям дня"));

        ledger.adjustBalance("UA1001", -45000.0);
        ledger.adjustBalance("UA1003", -29000.0);
        ledger.printBalances();

        ledger.restore(history.get("Перед закриттям дня"));
        ledger.printBalances();

        ledger.restore(history.get("Початок дня"));
        ledger.printBalances();
    }

    public static void main(String[] args) {
        runScenario();
    }
}