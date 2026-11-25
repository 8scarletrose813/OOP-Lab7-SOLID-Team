package Task3.products;

import Task3.visitors.ProductVisitor;

public class CardProduct implements BankProduct {

    private final String productId;
    private double balance;
    private final double creditLimit;

    public CardProduct(String productId, double balance, double creditLimit) {
        this.productId = productId;
        this.balance = balance;
        this.creditLimit = creditLimit;
    }
    public String getProductId() {
        return productId;
    }
    public double getBalance() {
        return balance;
    }
    public void setBalance(double balance) {
        this.balance = balance;
    }
    public double getCreditLimit() {
        return creditLimit;
    }
    @Override
    public void accept(ProductVisitor visitor) {
        visitor.visit(this);
    }
}
