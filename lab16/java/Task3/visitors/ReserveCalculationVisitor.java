package Task3.visitors;

import Task3.products.CardProduct;
import Task3.products.CreditProduct;
import Task3.products.DepositProduct;
import Task3.products.InvestmentAccount;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;

public class ReserveCalculationVisitor implements ProductVisitor {
    private double totalReserve = 0.0;

    @Override
    public void visit(DepositProduct deposit) {
        String time = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss"));
        double reserve = 0.0;
        totalReserve += reserve;
        System.out.println("[" + time + "] [ReserveCalculationVisitor] Депозит " + deposit.getProductId() + " (баланс " + deposit.getBalance() + " грн): резерв НБУ = " + reserve + " грн (0%)");
    }

    @Override
    public void visit(CreditProduct credit) {
        String time = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss"));
        double rate;
        if (credit.getOverdueDays() > 0) {
            rate = 0.20;
        } else {
            rate = 0.05;
        }
        double reserve = Math.round(credit.getDebtAmount() * rate * 100.0) / 100.0;
        totalReserve += reserve;
        System.out.println("[" + time + "] [ReserveCalculationVisitor] Кредит " + credit.getProductId() + " (борг " + credit.getDebtAmount() + " грн, прострочення " + credit.getOverdueDays() + " дн.): резерв НБУ = " + reserve + " грн (" + (int) (rate * 100) + "%)");
    }

    @Override
    public void visit(CardProduct card) {
        String time = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss"));
        double reserve = Math.round(card.getCreditLimit() * 0.10 * 100.0) / 100.0;
        totalReserve += reserve;
        System.out.println("[" + time + "] [ReserveCalculationVisitor] Картка " + card.getProductId() + " (ліміт " + card.getCreditLimit() + " грн): резерв НБУ = " + reserve + " грн (10%)");
    }

    @Override
    public void visit(InvestmentAccount investment) {
        String time = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss"));
        double reserve = Math.round(investment.getPortfolioValue() * 0.15 * 100.0) / 100.0;
        totalReserve += reserve;
        System.out.println("[" + time + "] [ReserveCalculationVisitor] Інвестрахунок " + investment.getProductId() + " (портфель " + investment.getPortfolioValue() + " грн): резерв НБУ = " + reserve + " грн (15%)");
    }

    public double getTotalReserve() {
        return Math.round(totalReserve * 100.0) / 100.0;
    }
}
