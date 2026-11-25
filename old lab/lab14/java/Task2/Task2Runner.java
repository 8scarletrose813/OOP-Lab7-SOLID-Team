package Task2;

import Task2.observers.*;
import Task2.service.CurrencyRateService;

public class Task2Runner {

    public static void runScenario() {
        CurrencyRateService rateService = new CurrencyRateService();

        RateObserver branchDisplay = new BranchDisplayObserver();
        RateObserver onlineBanking = new OnlineBankingObserver();
        RateObserver conversionModule = new ConversionModuleObserver();
        RateObserver clientAlert = new ClientAlertObserver("Коваленко І.П.", 41.00);

        System.out.println("Етап 1: Підписка отримувачів та курс 41.50");
        rateService.subscribe(branchDisplay);
        rateService.subscribe(onlineBanking);
        rateService.subscribe(conversionModule);
        rateService.subscribe(clientAlert);

        System.out.println();
        rateService.setRate("USD", 41.50);
        System.out.println();

        System.out.println("Етап 2: Зміна курсу на 40.90");
        rateService.setRate("USD", 40.90);
        System.out.println();

        System.out.println("Етап 3: Відписка клієнта та нова зміна курсу ");
        rateService.unsubscribe(clientAlert);

        System.out.println();
        rateService.setRate("USD", 40.75);
    }

    public static void main(String[] args) {
        runScenario();
    }
}