package Task2.documents;

import Task2.channels.DeliveryChannel;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;

public class AccountStatement extends PaymentDocument{
    private String clientName;
    private String iban;
    private double balance;

    public AccountStatement(DeliveryChannel channel, String clientName, String iban, double balance) {
        super(channel);
        this.clientName = clientName;
        this.iban = iban;
        this.balance = balance;
    }

    @Override
    public void generate() {
        String time = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss"));
        System.out.println("["+ time + "] " + "[AccountStatement] " + " Підготовка довідки про стан рахунку" );
        String content = "ПІБ: " + clientName + ", IBAN: " + iban + ", Залишок: " + balance;
        channel.deliver("Довідка про стан рахунку ", content);
    }
}
