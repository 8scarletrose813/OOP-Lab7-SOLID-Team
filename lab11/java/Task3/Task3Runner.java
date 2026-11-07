package Task3;

import Task3.adapters.SwiftPaymentAdapter;
import Task3.interfaces.TreasuryPaymentProcessor;
import Task3.models.PaymentRequest;
import Task3.models.PaymentResult;
import Task3.swift.SwiftService;

public class Task3Runner {
    public static void main(String[] args){
        SwiftService swiftService = new SwiftService();
        TreasuryPaymentProcessor processor = new SwiftPaymentAdapter(swiftService);
        PaymentRequest request1 = new PaymentRequest("UA123456789", "DE987654321", 1000.0, "EUR");
        PaymentResult  result1 = processor.processPayment(request1);
        System.out.println(result1);
        PaymentRequest request2 = new PaymentRequest("UA123445959", "DE98745321", 1000000000.0, "EUR");
        PaymentResult  result2 = processor.processPayment(request2);
        System.out.println(result2);
    }
}
