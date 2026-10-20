package variant7_employeemanagement;
public class VolunteerEmployee extends Employee {
    private String organizationName;

    public VolunteerEmployee(String employeeId, String fullName, String organizationName) {
        super(employeeId, fullName);
        this.organizationName = organizationName;
    }
}
