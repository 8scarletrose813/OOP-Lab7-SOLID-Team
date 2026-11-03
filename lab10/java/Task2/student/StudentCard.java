package Task2.student;

import Task2.interfaces.BankCard;
public class StudentCard implements BankCard {
    @Override
    public String getCardDetails() {
        return "Студентська картка 'Youth ID'";
    }
}
