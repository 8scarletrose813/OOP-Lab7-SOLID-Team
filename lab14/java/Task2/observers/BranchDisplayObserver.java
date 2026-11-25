package Task2.observers;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;

public class BranchDisplayObserver implements RateObserver{
    @Override
    public void update(String currency, double newRate) {
        String time = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss"));
        System.out.println("["+ time + "] [BranchDisplayObserver] Зміна курсу валюти " + currency + " на екранах у відділеннях: " + newRate );
    }
}
