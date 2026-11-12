package Task2;

import Task2.cards.BaseCard;
import Task2.decorators.*;
import Task2.interfaces.BankCard;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;

public class Task2Runner {
    private static final String source = "[Task2/BaseCard]";
    private static final String source2 = "[Task2/Decorator]";
    public static void main(String[] args){
        BaseCard baseCard = new BaseCard();
        String time = LocalDateTime.now().format(DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss"));
        String details = baseCard.getCardDetails();
        double cost = baseCard.getServiceCost();
        System.out.println("[" + time + "] " + source + " " + details + " " + cost);
        BankCard сard = new BaseCard();
        сard = new CashbackOption(сard);
        сard = new TravelInsuranceOption(сard);
        сard = new SmsNotificationOption(сard);
        String details1 = сard.getCardDetails();
        double cost1 = сard.getServiceCost();
        System.out.println("[" + time + "] " + source2 + " " + details1 + " " + cost1);
        BankCard bankCard1 = new BaseCard();
        bankCard1 = new IncreasedLimitOption(bankCard1);
        bankCard1 = new LoungeAccessOption(bankCard1);
        String details2 = bankCard1.getCardDetails();
        double cost2 = bankCard1.getServiceCost();
        System.out.println("[" + time + "] " + source2 + " " + details2 + " " + cost2);

    }
}
