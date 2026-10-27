import java.util.HashMap;
import java.util.Map;

public class CurrencyRateService {
    private static CurrencyRateService instance;
    private Map<String, CurrencyRate> rates;
    private CurrencyRateService(){
        this.rates = new HashMap<>();
        System.out.println("[10:00][Казначейство] Службу курсів валют ініціалізовано.");
    }
    public static synchronized CurrencyRateService getInstance() {
        if(instance == null){
            instance = new CurrencyRateService();
        }
        return instance;
    }
    public void setRate(String currency, double buy, double sell, String time){
        if(rates.get(currency) != null){
            rates.get(currency).setBuyRate(buy);
            rates.get(currency).setSellRate(sell);
            rates.get(currency).setLastUpdated(time);
        }
        else{
           rates.put(currency, new CurrencyRate(currency, buy, sell, time));
        }
    }
    public CurrencyRate getRate(String currency){
        return rates.get(currency);
    }
}
