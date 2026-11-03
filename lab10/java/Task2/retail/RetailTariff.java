package Task2.retail;

import Task2.interfaces.TariffPlan;
public class RetailTariff implements TariffPlan {
    @Override
    public String getTariffDetails() {
        return "Тариф 'Базовий 0 грн/міс'";
    }
}
