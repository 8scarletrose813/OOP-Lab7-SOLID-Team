package variant7_useraccountmanagement;

public class AuthGatewayService implements IUserManagementService{
    @Override public boolean authenticate(String userIdentifier, String secretKey) { return true; }
    @Override public void resetSecret(String userIdentifier) { System.out.println("Gateway secret reset for " + userIdentifier); }

    @Override public void registerUser(String userIdentifier, String secretKey) { throw new UnsupportedOperationException("Auth gateway does not create new user profiles."); }
    @Override public void assignRole(int userId, String role) { throw new UnsupportedOperationException("Auth gateway does not manage RBAC roles."); }
    @Override public void auditUserLogs(int userId) { throw new UnsupportedOperationException("Auth gateway does not store detailed logs."); }
    @Override public void exportUserDataGdpr(int userId) { throw new UnsupportedOperationException("Auth gateway cannot generate GDPR archives."); }
}
