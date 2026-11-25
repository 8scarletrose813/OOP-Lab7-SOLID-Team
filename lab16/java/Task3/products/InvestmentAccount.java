package Task3.products;

import Task3.visitors.BankProduct;
import Task3.visitors.ProductVisitor;

public class InvestmentAccount implements BankProduct {

    private final String productId;
    private double portfolioValue;
    private final String riskLevel;
    public InvestmentAccount(String productId, double portfolioValue, String riskLevel) {
        this.productId = productId;
        this.portfolioValue = portfolioValue;
        this.riskLevel = riskLevel;
    }
    public String getProductId() {
        return productId;
    }
    public double getPortfolioValue() {
        return portfolioValue;
    }
    public void setPortfolioValue(double portfolioValue) {
        this.portfolioValue = portfolioValue;
    }
    public String getRiskLevel() {
        return riskLevel;
    }
    @Override
    public void accept(ProductVisitor visitor) {
        visitor.visit(this);
    }
}
