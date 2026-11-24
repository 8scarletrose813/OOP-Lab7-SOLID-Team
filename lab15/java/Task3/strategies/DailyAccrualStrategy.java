package Task3.strategies;

public class DailyAccrualStrategy implements InterestCalculationStrategy {
    @Override
    public double calculate(double amount, double annualRate, int months) {
        double dailyRate =  annualRate/(100*365);
        int days = months * 30;
        return amount * Math.pow(1+ dailyRate, days) - amount;
    }

    @Override
    public String getSchemeName() {
        return "Щоденне нарахування";
    }
}
