package Task2.channels;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;

public class BranchPrintChannel implements DeliveryChannel{
    @Override
    public void deliver(String docTitle, String content) {
        String time = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss"));
        System.out.println("["+ time + "] " + "[BranchPrintChannel] " + docTitle + " " + content + " Друк у відділенні банку" );
    }
}
