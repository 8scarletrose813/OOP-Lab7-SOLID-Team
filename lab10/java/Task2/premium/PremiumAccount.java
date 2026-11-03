package Task2.premium;

import Task2.interfaces.Account;
public class PremiumAccount implements Account {
    @Override
    public String getAccountDetails() {
        return "Рахунок з персональним банкіром";
    }
}
