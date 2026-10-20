package variant7_useraccountmanagement;

public interface IAuthenticationService {
    boolean authenticate(String userIdentifier, String secretKey);
    void  resetSecret(String userIdentifier);
}
