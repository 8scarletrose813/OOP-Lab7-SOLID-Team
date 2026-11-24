package Task3.strategies;

public interface InterestCalculationStrategy {
    double calculate(double amount, double annualRate, int months);
    String getSchemeName();
}
