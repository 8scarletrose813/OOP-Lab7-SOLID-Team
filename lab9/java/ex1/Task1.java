package ex1;

public class Task1 {
    public  static void main(String[] args){
        CurrencyRateService branch3 =CurrencyRateService.getInstance();
        branch3.setRate("USD", 41.20, 41.70, "10:01");
        CurrencyRateService branch5 = CurrencyRateService.getInstance();
        CurrencyRateService onlineBanking = CurrencyRateService.getInstance();
        onlineBanking.getRate("USD");
        System.out.println(branch3.getRate("USD"));
        System.out.println(branch5.getRate("USD"));
        System.out.println(onlineBanking.getRate("USD"));
        System.out.println(System.identityHashCode(branch3));
        System.out.println(System.identityHashCode(branch5));
        System.out.println(System.identityHashCode(onlineBanking));
        branch3.setRate("USD", 41.30, 41.80, "10:05");
        System.out.println(onlineBanking.getRate("USD"));

        
    }
}
