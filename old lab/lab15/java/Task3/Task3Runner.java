package Task3;

import Task3.model.Deposit;
import Task3.strategies.*;

public class Task3Runner {

    public static void runScenario() {
        Deposit deposit = new Deposit("DEP-001", 100000.0, 12.0, 12, new SimpleInterestStrategy());
        deposit.calculateIncome();

        deposit.setStrategy(new MonthlyCapitalizationStrategy());
        deposit.calculateIncome();

        deposit.setStrategy(new DailyAccrualStrategy());
        deposit.calculateIncome();

        deposit.setStrategy(new TieredRateStrategy());
        deposit.calculateIncome();

        deposit.setStrategy(new BonusRateStrategy(1.5));
        deposit.calculateIncome();
    }

    public static void main(String[] args) {
        runScenario();
    }
}