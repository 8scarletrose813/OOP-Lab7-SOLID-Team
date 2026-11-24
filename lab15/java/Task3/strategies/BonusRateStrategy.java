package Task3.strategies;

public class BonusRateStrategy implements  InterestCalculationStrategy{
    private double bonusRate;

    public BonusRateStrategy(double bonusRate) {
        this.bonusRate = 1.5;
    }

    @Override
    public double calculate(double amount, double annualRate, int months) {
        double adjustedRate = annualRate + bonusRate;
        return amount * (adjustedRate/100.0)*(months/12.0);
    }

    @Override
    public String getSchemeName() {
        return "Бонусна ставка";
    }
}
