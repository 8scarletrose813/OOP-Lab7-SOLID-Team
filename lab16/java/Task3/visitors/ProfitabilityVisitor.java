package Task3.visitors;

import Task3.products.CardProduct;
import Task3.products.CreditProduct;
import Task3.products.DepositProduct;
import Task3.products.InvestmentAccount;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;

public class ProfitabilityVisitor implements ProductVisitor {
    private double totalProfit = 0.0;
    String time = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss"));
    @Override
    public void visit(DepositProduct deposit) {
        double profit = -(deposit.getBalance() * (deposit.getInterestRate() / 100.0));
        totalProfit += profit;
        System.out.println("[" + time + "] [ProfitabilityVisitor] Депозит " + deposit.getProductId() + ": річний результат для банку = " +  profit + " грн (витрати на відсотки)");
    }

    @Override
    public void visit(CreditProduct credit) {
        double income = credit.getDebtAmount() * (credit.getInterestRate() / 100.0);
        totalProfit += income;
        System.out.println("[" + time + "] [ProfitabilityVisitor] Кредит " + credit.getProductId() + ": річний прибуток банку = +" + income + " грн");
    }

    @Override
    public void visit(CardProduct card) {
        double income = 600.0 + (card.getCreditLimit() * 0.02);
        totalProfit += income;
        System.out.println("[" + time + "] [ProfitabilityVisitor] Картка " + card.getProductId() + ": річний прибуток банку = +" + income + " грн (обслуговування та комісії)");
    }

    @Override
    public void visit(InvestmentAccount investment) {
        double fee = investment.getPortfolioValue() * 0.015;
        totalProfit += fee;
        System.out.println("[" + time + "] [ProfitabilityVisitor] Інвестрахунок " + investment.getProductId() + ": річна комісія банку = +" + fee + " грн (1.5%)");
    }

    public double getTotalReserve() {
        return totalProfit;
    }
}
