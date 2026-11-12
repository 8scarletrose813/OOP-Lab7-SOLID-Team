package Task2.channels;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;

public class OnlineBankingPdfChannel implements DeliveryChannel{
    @Override
    public void deliver(String docTitle, String content) {
        String time = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss"));
        System.out.println("["+ time + "] " + "[OnlineBankingPdfChannel] " + docTitle + " " + content + " Формування PDF в онлайн-банкінгу" );
    }
}
