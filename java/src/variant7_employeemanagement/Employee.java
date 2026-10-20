package variant7_employeemanagement;
import java.math.BigDecimal;
public class Employee {
        private String employeeId;
        private String fullName;
        private BigDecimal baseSalary;

        public Employee(String employeeId, String fullName, BigDecimal baseSalary) {
            this.employeeId = employeeId;
            this.fullName = fullName;
            this.baseSalary = baseSalary;
        }

        public String getEmployeeId() { return employeeId; }
        public String getFullName() { return fullName; }
        public BigDecimal getBaseSalary() { return baseSalary; }

        public void setSalary(BigDecimal newSalary) {
            if (newSalary.compareTo(BigDecimal.ZERO) < 0) throw new IllegalArgumentException("Salary cannot be negative");
            this.baseSalary = newSalary;
        }

        public BigDecimal calculateAnnualBonus(double performanceRating) {
            if (performanceRating < 1.0 || performanceRating > 5.0) throw new IllegalArgumentException("Invalid rating");
            return baseSalary.multiply(BigDecimal.valueOf(0.10 * performanceRating));
        }
}
