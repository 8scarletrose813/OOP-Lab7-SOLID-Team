// Опис предметної області: Приватна медична клініка та обслуговування пацієнтів.
// Завдання коду: Запис пацієнта на прийом та лабораторні аналізи — розрахунок вартості консультації залежно від категорії лікаря (General, Specialist, Professor), розрахунок покриття страхової компанії, підсумовування аналізів, створення медичної квитанції, збереження у реєстр прийомів та відправка SMS.

public class Patient {
    private int id;
    private String fullName;
    private boolean hasInsurance;
    private String insurancePolicyNumber;
    private String phone;
    public Patient(int id, String fullName, boolean hasInsurance, String insurancePolicyNumber, String phone){
        this.id = id;
        this.fullName = fullName;
        this.hasInsurance = hasInsurance;
        this.insurancePolicyNumber = insurancePolicyNumber;
        this.phone = phone;
    }

    public int getId() {
        return id;
    }
    public String getFullName() {
        return fullName;
    }
    public boolean isHasInsurance() {
        return hasInsurance;
    }
    public String getInsurancePolicyNumber() {
        return insurancePolicyNumber;
    }
    public String getPhone() {
        return phone;
    }
}