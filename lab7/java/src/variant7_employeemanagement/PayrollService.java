package variant7_employeemanagement;
import java.math.BigDecimal;
public class PayrollService {
    public void processYearEndBonuses(SalariedEmployee salariedEmployee, double rating) {
        // HR-сервіс вважає, що для кожного співробітника можна обчислити бонус
        BigDecimal bonus = salariedEmployee.calculateAnnualBonus(rating);
        System.out.println("Bonus for " + salariedEmployee.getFullName() + ": $" + bonus);
    }
}


