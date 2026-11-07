package Task3.models;

public class PaymentResult {
    private boolean success;
    private String message;
    public PaymentResult(boolean success, String message) {
        this.success = success;
        this.message = message;
    }
    public boolean isSuccess() {
        return success;
    }
    public String getMessage() {
        return message;
    }

    @Override
    public String toString() {
        return ("Чи пройшов платіж: " + success + " " + message);
    }
}
