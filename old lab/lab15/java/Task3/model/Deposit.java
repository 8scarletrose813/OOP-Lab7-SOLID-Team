package Task3.model;

import Task3.strategies.InterestCalculationStrategy;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;

public class Deposit {
    private String depositId;
    private double amount;
    private double annualRate;
    private int months;
    private InterestCalculationStrategy strategy;

    public Deposit(String depositId, double amount, double annualRate, int months, InterestCalculationStrategy strategy) {
        this.depositId = depositId;
        this.amount = amount;
        this.annualRate = annualRate;
        this.months = months;
        this.strategy = strategy;
    }
    public void setStrategy(InterestCalculationStrategy strategy){
        if (strategy == null) {
            throw new IllegalArgumentException("Стратегія нарахування не може бути null");
        }
        this.strategy = strategy;
        String time = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss"));
        System.out.println("[" + time + "]  [Deposit] Для депозиту " + this.depositId + " змінено схему нарахування на: " + strategy.getSchemeName());
    }
    public double calculateIncome(){
        String time = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss"));
        double income = Math.round(strategy.calculate(amount, annualRate, months) * 100.0) / 100.0;
        System.out.println("[" + time + "] [Deposit] Депозит " + this.depositId + " (" + strategy.getSchemeName() + "): дохід = " + income + " грн");
        return income;
    }
}
