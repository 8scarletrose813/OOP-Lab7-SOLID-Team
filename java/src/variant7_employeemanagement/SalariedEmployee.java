package variant7_employeemanagement;

import java.math.BigDecimal;

public class SalariedEmployee extends Employee{
    private BigDecimal baseSalary;
    public SalariedEmployee( String employeeId, String fullName, BigDecimal baseSalary){
        super(employeeId, fullName);
        this.baseSalary = baseSalary;
    }
    public void setSalary(BigDecimal newSalary) {
        if (newSalary.compareTo(BigDecimal.ZERO) < 0) throw new IllegalArgumentException("Salary cannot be negative");
        this.baseSalary = newSalary;
    }
    public BigDecimal calculateAnnualBonus(double performanceRating) {
        if (performanceRating < 1.0 || performanceRating > 5.0) throw new IllegalArgumentException("Invalid rating");
        return baseSalary.multiply(BigDecimal.valueOf(0.10 * performanceRating));
    }
    public BigDecimal getBaseSalary() { return baseSalary; }
    
}
