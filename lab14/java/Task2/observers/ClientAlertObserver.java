package Task2.observers;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;

public class ClientAlertObserver implements RateObserver{
    private final String clientName;
    private final double targetRate;

    public ClientAlertObserver(String clientName, double targetRate) {
        this.clientName = clientName;
        this.targetRate = targetRate;
    }

    @Override
    public void update(String currency, double newRate) {
        if(newRate <= targetRate){
            String time = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss"));
            System.out.println("["+ time + "] [ClientAlertObserver] Сповіщення для клієнта " + clientName + " : курс " + currency + " досяг порог " + targetRate + "(поточний курс: " + newRate + " )");
        }
    }
}
