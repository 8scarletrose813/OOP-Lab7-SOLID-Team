package ex3;

public class PaymentSchedule implements Cloneable {
    private String paymentDate;
    private double amount;
    public PaymentSchedule(String paymentDate, double amount){
        this.paymentDate = paymentDate;
        this.amount = amount;
    }
    @Override
    protected Object clone() throws CloneNotSupportedException {
        return super.clone();
    }
    public String getPaymentDate() {
        return paymentDate;
    }
    public void setPaymentDate(String paymentDate) {
        this.paymentDate = paymentDate;
    }
    public double getAmount() {
        return amount;
    }
    public void setAmount(double amount) {
        this.amount = amount;
    }

    @Override
    public String toString() {
        return ("Дата чергового платежу: " + paymentDate + '\'' +
                "сума платежу" + amount);
    }
}
