package variant7_useraccountmanagement;

public class AuthGatewayService implements IAuthenticationService{
    @Override public boolean authenticate(String userIdentifier, String secretKey) { return true; }
    @Override public void resetSecret(String userIdentifier) { System.out.println("Gateway secret reset for " + userIdentifier); }
}
