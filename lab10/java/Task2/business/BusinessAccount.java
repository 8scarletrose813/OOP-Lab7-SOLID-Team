package Task2.business;
import Task2.interfaces.Account;
public class BusinessAccount implements Account {
    @Override
    public String getAccountDetails() {
        return "Розрахунковий рахунок ФОП/ТОВ";
    }
}
