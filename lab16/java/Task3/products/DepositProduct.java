package Task3.products;

import Task3.visitors.ProductVisitor;

public class DepositProduct implements BankProduct {
    private String productId;
    private double balance;
    private double interestRate;
    public DepositProduct(String productId, double balance, double interestRate) {
        this.productId = productId;
        this.balance = balance;
        this.interestRate = interestRate;
    }
    public String getProductId() {
        return productId;
    }
    public double getBalance() {
        return balance;
    }
    public double getInterestRate() {
        return interestRate;
    }
    public void setBalance(double balance) {
        this.balance = balance;
    }
    @Override
    public void accept(ProductVisitor visitor){
        visitor.visit(this);
    }
}
