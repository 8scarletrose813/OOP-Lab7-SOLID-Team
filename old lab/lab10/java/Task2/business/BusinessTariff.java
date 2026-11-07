package Task2.business;
import Task2.interfaces.TariffPlan;
public class BusinessTariff implements TariffPlan {
    @Override
    public String getTariffDetails() {
        return "Тариф 'Комерційний 1.5%'";
    }
}
