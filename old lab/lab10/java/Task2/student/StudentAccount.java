package Task2.student;

import Task2.interfaces.Account;
public class StudentAccount implements Account {
    @Override
    public String getAccountDetails() {
        return "Пільговий картковий рахунок 'Студентський'";
    }
}
