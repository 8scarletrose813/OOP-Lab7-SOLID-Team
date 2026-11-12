package Task2;

import Task2.channels.*;
import Task2.documents.AccountStatement;
import Task2.documents.PaymentDocument;
import Task2.documents.PaymentReceipt;

public class Task2Runner {
    public static void runScenario() {
        DeliveryChannel deliveryChannel = new BranchPrintChannel();
        PaymentDocument paymentDocument = new PaymentReceipt(deliveryChannel, "КВ-1045", 1250.50, "Оплата комунальних послуг");
        paymentDocument.generate();
        DeliveryChannel deliveryChannel1 = new OnlineBankingPdfChannel();
        PaymentDocument paymentDocument1 = new PaymentReceipt(deliveryChannel1, "КВ-1045", 1250.50, "Оплата комунальних послуг");
        paymentDocument1.generate();
        DeliveryChannel deliveryChannel2 = new DigitalSignatureChannel();
        PaymentDocument paymentDocument2 = new AccountStatement(deliveryChannel2, "Ковальчук Олена Іванівна", "UA213223130000026007233566001", 48500.75);
        paymentDocument2.generate();
        DeliveryChannel deliveryChannel3 = new DiiaChannel();
        PaymentDocument paymentDocument3 = new AccountStatement(deliveryChannel3, "Ковальчук Олена Іванівна", "UA213223130000026007233566001", 43500.7);
        paymentDocument3.generate();
    }
    public static void main(String[] args){
        runScenario();
    }
}
