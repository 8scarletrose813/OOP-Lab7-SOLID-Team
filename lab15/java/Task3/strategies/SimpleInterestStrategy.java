package Task3.strategies;

public class SimpleInterestStrategy implements InterestCalculationStrategy{
    @Override
    public double calculate(double amount, double annualRate, int months) {
        return amount *(annualRate/100) * (months/12);
    }

    @Override
    public String getSchemeName() {
        return "Прості відсотки в кінці строку";
    }
}
