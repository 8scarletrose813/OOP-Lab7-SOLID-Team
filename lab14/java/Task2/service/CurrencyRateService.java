package Task2.service;

import Task2.observers.RateObserver;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.List;

public class CurrencyRateService {
    private List<RateObserver> subscribers = new ArrayList<>();
    private String currentCurrency;
    private double currentRate;
    private String getCurrentTime() {
        DateTimeFormatter time = DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss");
        return LocalDateTime.now().format(time);
    }
    public void subscribe(RateObserver observer){
        if (!subscribers.contains(observer)){
            subscribers.add(observer);
            System.out.println("[" + getCurrentTime() +"] [CurrencyRateService] Підписано нового отримувача: " + observer.getClass().getSimpleName());
        }
    }
    public void unsubscribe(RateObserver observer ){
        subscribers.remove(observer);
        System.out.println("[" + getCurrentTime() +"] [CurrencyRateService] Відписано отримувача: " + observer.getClass().getSimpleName());
    }
    private void notifyObservers(){
        for (RateObserver observer : subscribers){
            observer.update(currentCurrency, currentRate);
        }
    }
    public void setRate(String currency, double newRate){
        this.currentCurrency = currency;
        this.currentRate = newRate;
        System.out.println("[" + getCurrentTime() +"] [CurrencyRateService] Зміна курсу валюти " + currency + ": новий курс = " + newRate);
        notifyObservers();
    }
}
