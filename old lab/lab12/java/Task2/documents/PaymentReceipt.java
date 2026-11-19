package Task2.documents;

import Task2.channels.DeliveryChannel;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;

public class PaymentReceipt extends PaymentDocument{
    private String receiptNumber;
    private double amount;
    private String purpose;

    public PaymentReceipt(DeliveryChannel channel, String receiptNumber, double amount, String purpose) {
        super(channel);
        this.receiptNumber = receiptNumber;
        this.amount = amount;
        this.purpose = purpose;
    }

    @Override
    public void generate() {
        String time = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss"));
        System.out.println("["+ time + "] " + "[PaymentReceipt] " + " Підготовка квитанції про оплату" );
        String content = "Номер квитанції: " + receiptNumber + ", Сума оплати: " + amount + ", Призначення платежу: " + purpose;
        channel.deliver("Квитанція про оплату ", content);
    }
}
