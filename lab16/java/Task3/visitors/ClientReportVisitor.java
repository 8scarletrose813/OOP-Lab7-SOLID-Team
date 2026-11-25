package Task3.visitors;

import Task3.products.CardProduct;
import Task3.products.CreditProduct;
import Task3.products.DepositProduct;
import Task3.products.InvestmentAccount;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;

public class ClientReportVisitor implements ProductVisitor {
    String time = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss"));

    @Override
    public void visit(DepositProduct deposit) {
        System.out.println("[" + time + "] [ClientReportVisitor] [Звіт] Депозит №" + deposit.getProductId() + " Сума: " + deposit.getBalance() + " грн  Ставка: " + deposit.getInterestRate() + "% річних");
    }

    @Override
    public void visit(CreditProduct credit) {
        System.out.println("[" + time + "] [ClientReportVisitor] [Звіт] Кредит №" + credit.getProductId() + " Залишок боргу: " + credit.getDebtAmount() + " грн Ставка: " + credit.getInterestRate() + "% Прострочення: " + credit.getOverdueDays() + " дн.");
    }

    @Override
    public void visit(CardProduct card) {
        System.out.println("[" + time + "] [ClientReportVisitor] [Звіт] Картка №" + card.getProductId() + " Власні кошти: " + card.getBalance() + " грн Кредитний ліміт: " + card.getCreditLimit() + " грн");
    }

    @Override
    public void visit(InvestmentAccount investment) {
        System.out.println("[" + time + "] [ClientReportVisitor] [Звіт] Інвестрахунок №" + investment.getProductId() + " Вартість активів: " + investment.getPortfolioValue() + " грн Ризик-профіль: " + investment.getRiskLevel());
    }
}
