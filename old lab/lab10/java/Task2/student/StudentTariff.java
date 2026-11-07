package Task2.student;

import Task2.interfaces.TariffPlan;
public class StudentTariff implements TariffPlan {
    @Override
    public String getTariffDetails() {
        return "Тариф 'Студентський 0% комісії'";
    }
}
