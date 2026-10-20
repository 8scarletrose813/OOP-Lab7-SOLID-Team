package variant7_useraccountmanagement;

public interface IUserManagementService {
    void registerUser(String userIdentifier, String secretKey);
    boolean authenticate(String userIdentifier, String secretKey);
    void resetSecret(String userIdentifier);
    void assignRole(int userId, String role);
    void auditUserLogs(int userId);
    void exportUserDataGdpr(int userId);
}
