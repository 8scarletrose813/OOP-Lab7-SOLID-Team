package Task2.retail;

import Task2.interfaces.Account;
public class RetailAccount implements Account {
    @Override
    public String getAccountDetails() {
        return "Поточний рахунок 'Фізична особа'";
    }
}
