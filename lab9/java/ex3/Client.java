package ex3;

public class Client{
   private String fullName;
   private String accountNumber;
   public Client(String fullName, String accountNumber){
       this.fullName = fullName;
       this.accountNumber = accountNumber;
   }
    public String getFullName() {
        return fullName;
    }
    public void setFullName(String fullName) {
        this.fullName = fullName;
    }
    public String getAccountNumber() {
        return accountNumber;
    }
    public void setAccountNumber(String accountNumber) {
        this.accountNumber = accountNumber;
    }

    @Override
    public String toString() {
        return ("ПІБ: " + fullName + " Номер карти: " + accountNumber);
    }
}
