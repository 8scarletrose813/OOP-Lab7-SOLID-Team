package Task3;

import Task3.products.CardProduct;
import Task3.products.CreditProduct;
import Task3.products.DepositProduct;
import Task3.products.InvestmentAccount;
import Task3.products.BankProduct;
import Task3.visitors.ClientReportVisitor;
import Task3.visitors.ProfitabilityVisitor;
import Task3.visitors.ReserveCalculationVisitor;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.List;

public class Task3Runner {

    private static String now() {
        return LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss"));
    }

    public static void runScenario() {
        List<BankProduct> clientPortfolio = List.of(
                new DepositProduct("DEP-101", 100000.0, 14.0),
                new CreditProduct("CRD-202", 50000.0, 24.0, 12),
                new CardProduct("CRD-303", 15000.0, 40000.0),
                new InvestmentAccount("INV-404", 200000.0, "Помірний")
        );

        System.out.println("[" + now() + "] [Task3Runner] 1. Розрахунок резервів НБУ для портфеля");
        ReserveCalculationVisitor reserveVisitor = new ReserveCalculationVisitor();
        for (BankProduct product : clientPortfolio) {
            product.accept(reserveVisitor);
        }
        System.out.println("[" + now() + "] [Task3Runner] Загальна сума резервів НБУ: "
                + String.format("%.2f", reserveVisitor.getTotalReserve()) + " грн\n");

        System.out.println("[" + now() + "] [Task3Runner] 2. Розрахунок прибутковості портфеля для банку");
        ProfitabilityVisitor profitabilityVisitor = new ProfitabilityVisitor();
        for (BankProduct product : clientPortfolio) {
            product.accept(profitabilityVisitor);
        }
        System.out.println("[" + now() + "] [Task3Runner] Чистий річний результат для банку: "
                + String.format("%.2f", profitabilityVisitor.getTotalReserve()) + " грн\n");

        System.out.println("[" + now() + "] [Task3Runner] 3. Формування звіту для клієнта (нова операція)");
        ClientReportVisitor reportVisitor = new ClientReportVisitor();
        for (BankProduct product : clientPortfolio) {
            product.accept(reportVisitor);
        }
    }

    public static void main(String[] args) {
        runScenario();
    }
}