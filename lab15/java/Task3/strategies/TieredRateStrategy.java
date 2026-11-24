package Task3.strategies;

public class TieredRateStrategy implements InterestCalculationStrategy{
    @Override
    public double calculate(double amount, double annualRate, int months) {
        if(amount >= 100000){
            annualRate += 2.0;
        } else if (amount >= 50000) {
            annualRate += 1.0;
        }
        return amount * (annualRate / 100.0) * (months / 12.0);
    }

    @Override
    public String getSchemeName() {
        return "Ступінчаста ставка";
    }
}
