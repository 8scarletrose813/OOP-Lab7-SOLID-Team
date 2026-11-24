package Task3.strategies;

import java.math.BigDecimal;

public class MonthlyCapitalizationStrategy implements InterestCalculationStrategy{
    @Override
    public double calculate(double amount, double annualRate, int months) {
        double monthlyRate = annualRate / (100.0 * 12);
        return amount * Math.pow(1 + monthlyRate, months) - amount;
    }

    @Override
    public String getSchemeName() {
        return "Щомісячна капіталізація";
    }
}
