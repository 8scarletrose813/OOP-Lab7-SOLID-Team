package variant7_useraccountmanagement;

public class SelfServiceCustomerPortal implements IUserManagementService {
    @Override public void registerUser(String userIdentifier, String secretKey) { System.out.println("User registered: " + userIdentifier); }
    @Override public boolean authenticate(String userIdentifier, String secretKey) { return true; }
    @Override public void resetSecret(String userIdentifier) { System.out.println("Secret reset sent for " + userIdentifier); }
    @Override public void exportUserDataGdpr(int userId) { System.out.println("Exported GDPR archive for user " + userId); }

    @Override public void assignRole(int userId, String role) { throw new UnsupportedOperationException("Customers cannot assign administrative roles."); }
    @Override public void auditUserLogs(int userId) { throw new UnsupportedOperationException("Customers cannot access security audit logs."); }
}
