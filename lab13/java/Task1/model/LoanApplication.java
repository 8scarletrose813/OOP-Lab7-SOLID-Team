package Task1.model;

public class LoanApplication {
    private String applicationId;
    private double amount;
    public LoanApplication(String applicationId, double amount) {
        this.applicationId = applicationId;
        this.amount = amount;
    }
    public String getApplicationId() {
        return applicationId;
    }
    public double getAmount() {
        return amount;
    }
}
