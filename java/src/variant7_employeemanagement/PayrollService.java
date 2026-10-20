package variant7_employeemanagement;
import java.math.BigDecimal;
public class PayrollService {
    public void processYearEndBonuses(Employee employee, double rating) {
        // HR-сервіс вважає, що для кожного співробітника можна обчислити бонус
        BigDecimal bonus = employee.calculateAnnualBonus(rating);
        System.out.println("Bonus for " + employee.getFullName() + ": $" + bonus);
    }
}

