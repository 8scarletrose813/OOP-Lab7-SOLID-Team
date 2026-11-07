package Task3.interfaces;

import Task3.models.PaymentRequest;
import Task3.models.PaymentResult;

public interface TreasuryPaymentProcessor {
    PaymentResult processPayment(PaymentRequest paymentRequest);
}
