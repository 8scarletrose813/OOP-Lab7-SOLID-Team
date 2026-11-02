package variant7_useraccountmanagement;
// Опис предметної області: Екосистема пристроїв розумного дому.
// Завдання коду: Забезпечення єдиного програмного інтерфейсу для взаємодії з різними типами побутових розумних пристроїв.


public class SelfServiceCustomerPortal implements IAuthenticationService, IUserRegistrationService, IGdprExportService {
    @Override public void registerUser(String userIdentifier, String secretKey) { System.out.println("User registered: " + userIdentifier); }
    @Override public boolean authenticate(String userIdentifier, String secretKey) { return true; }
    @Override public void resetSecret(String userIdentifier) { System.out.println("Secret reset sent for " + userIdentifier); }
    @Override public void exportUserDataGdpr(int userId) { System.out.println("Exported GDPR archive for user " + userId); }
}
