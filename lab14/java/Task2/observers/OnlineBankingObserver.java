package Task2.observers;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;

public class OnlineBankingObserver implements RateObserver{

    @Override
    public void update(String currency, double newRate) {
        String time = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss"));
        System.out.println("["+ time + "] [OnlineBankingObserver] Оновлено курс " + currency + ": у веб- та мобільному банкінгу = " + newRate );
    }
}
