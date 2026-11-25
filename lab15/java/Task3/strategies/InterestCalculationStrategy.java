package Task3.strategies;
//Проблематика: Зберігання всіх формул розрахунку
//відсотків усередині класу депозиту через if-else ускладнює додавання нових схем (порушується принцип OCP)
// і не дозволяє змінювати умови вкладу під час роботи програми.
public interface InterestCalculationStrategy {
    double calculate(double amount, double annualRate, int months);
    String getSchemeName();
}
