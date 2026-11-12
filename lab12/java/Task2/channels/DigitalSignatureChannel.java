package Task2.channels;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;

public class DigitalSignatureChannel implements DeliveryChannel{
    @Override
    public void deliver(String docTitle, String content) {
        String time = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss"));
        System.out.println("["+ time + "] " + "[DigitalSignatureChannel] " + docTitle + " " + content + " Формування електронного документа із цифровим підписом" );
    }
}
