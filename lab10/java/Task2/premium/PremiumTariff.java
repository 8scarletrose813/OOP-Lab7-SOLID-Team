package Task2.premium;

import Task2.interfaces.TariffPlan;
public class PremiumTariff implements TariffPlan {
    @Override
    public String getTariffDetails() {
        return "Тариф 'VIP Premium'";
    }
}
