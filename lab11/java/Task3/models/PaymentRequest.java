package Task3.models;

public class PaymentRequest {
    private String senderAccount;
    private String receiverAccount;
    private double amount;
    private String currency;
    public PaymentRequest(String senderAccount, String receiverAccount, double amount, String currency){
        this.senderAccount = senderAccount;
        this.receiverAccount = receiverAccount;
        this.amount = amount;
        this.currency = currency;
    }
    public String getSenderAccount() {
        return senderAccount;
    }
    public void setSenderAccount(String senderAccount) {
        this.senderAccount = senderAccount;
    }
    public String getReceiverAccount() {
        return receiverAccount;
    }
    public void setReceiverAccount(String receiverAccount) {
        this.receiverAccount = receiverAccount;
    }
    public double getAmount() {
        return amount;
    }
    public void setAmount(double amount) {
        this.amount = amount;
    }
    public String getCurrency() {
        return currency;
    }
    public void setCurrency(String currency) {
        this.currency = currency;
    }
}
