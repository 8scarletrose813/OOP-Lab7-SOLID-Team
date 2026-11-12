package Task2.documents;

import Task2.channels.DeliveryChannel;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;

public class PaymentOrder extends PaymentDocument {
    private String sender;
    private String receiver;
    private double amount;

    public PaymentOrder(String sender, String receiver, double amount, DeliveryChannel channel) {
        super(channel);
        this.sender = sender;
        this.receiver = receiver;
        this.amount = amount;
    }

    @Override
    public void generate() {
        String time = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss"));
        System.out.println("["+ time + "] " + "[PaymentOrder] " + " Підготовка платіжного доручення" );
        String content = "Відправник: " + sender + ", Отримувач: " + receiver + ", Сума: " + amount;
        channel.deliver("Платіжне доручення ", content);
    }
}
