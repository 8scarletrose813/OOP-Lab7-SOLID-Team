package Task3.products;

import Task3.visitors.BankProduct;
import Task3.visitors.ProductVisitor;

public class CreditProduct implements BankProduct {
    private final String productId;
    private double debtAmount;
    private final double interestRate;
    private int overdueDays;
    public CreditProduct(String productId, double debtAmount, double interestRate, int overdueDays) {
        this.productId = productId;
        this.debtAmount = debtAmount;
        this.interestRate = interestRate;
        this.overdueDays = overdueDays;
    }
    public String getProductId() {
        return productId;
    }
    public double getDebtAmount() {
        return debtAmount;
    }
    public void setDebtAmount(double debtAmount) {
        this.debtAmount = debtAmount;
    }
    public double getInterestRate() {
        return interestRate;
    }
    public int getOverdueDays() {
        return overdueDays;
    }
    public void setOverdueDays(int overdueDays) {
        this.overdueDays = overdueDays;
    }
    @Override
    public void accept(ProductVisitor visitor) {
        visitor.visit(this);
    }
}
