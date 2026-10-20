package variant7_employeemanagement;
import java.math.BigDecimal;
public class VolunteerEmployee extends Employee {
    private String organizationName;

    public VolunteerEmployee(String employeeId, String fullName, String organizationName) {
        super(employeeId, fullName, BigDecimal.ZERO);
        this.organizationName = organizationName;
    }

    @Override
    public void setSalary(BigDecimal newSalary) {
        // Порушення LSP: Волонтери працюють на безоплатній основі
        throw new IllegalStateException("Volunteers do not receive a salary and their base rate cannot be modified.");
    }

    @Override
    public BigDecimal calculateAnnualBonus(double performanceRating) {
        // Порушення LSP: Відмова від виплати фінансових бонусів
        throw new UnsupportedOperationException("Volunteers are not eligible for financial annual bonuses.");
    }
}
