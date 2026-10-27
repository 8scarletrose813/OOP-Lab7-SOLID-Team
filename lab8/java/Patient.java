// Опис предметної області: Приватна медична клініка та обслуговування пацієнтів.
// Завдання коду: Запис пацієнта на прийом та лабораторні аналізи — розрахунок вартості консультації залежно від категорії лікаря (General, Specialist, Professor), розрахунок покриття страхової компанії, підсумовування аналізів, створення медичної квитанції, збереження у реєстр прийомів та відправка SMS.

public class Patient {
    public int id;
    public String fullName;
    public boolean hasInsurance;
    public String insurancePolicyNumber;
    public String phone;
}