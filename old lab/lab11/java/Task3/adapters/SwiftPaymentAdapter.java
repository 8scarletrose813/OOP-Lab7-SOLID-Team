package Task3.adapters;

import Task3.interfaces.TreasuryPaymentProcessor;
import Task3.models.PaymentRequest;
import Task3.models.PaymentResult;
import Task3.swift.SwiftService;

public class SwiftPaymentAdapter implements TreasuryPaymentProcessor {
    private SwiftService swiftService;
    public SwiftPaymentAdapter(SwiftService swiftService) {
        this.swiftService = swiftService;
    }
    @Override
    public PaymentResult processPayment(PaymentRequest request ) {
        String encodedSwift = "SWIFT|from:" + request.getSenderAccount() + "|to:" + request.getReceiverAccount() + "|amt:" + request.getAmount() + "|cur:" + request.getCurrency();
        int code = swiftService.sendSwiftMessage(encodedSwift, request.getAmount());
        if (code == 200){
            return new PaymentResult(true, "Успіх");
        }
        return new PaymentResult(false, "Відхилено");
    }
}
