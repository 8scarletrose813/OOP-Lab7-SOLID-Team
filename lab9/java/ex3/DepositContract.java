package ex3;

import java.util.ArrayList;
import java.util.List;

public class DepositContract implements Cloneable {
    private String productType;
    private double interestRate;
    private int termMonths;
    private String earlyTerminationTerms;
    private Client client;
    private List<PaymentSchedule> paymentSchedules;
    private List<Addendum> addendums;
    public DepositContract(String productType, double interestRate, int termMonths, String earlyTerminationTerms) {
        this.productType = productType;
        this.interestRate = interestRate;
        this.termMonths = termMonths;
        this.earlyTerminationTerms = earlyTerminationTerms;
        this.addendums = new ArrayList<>();
        this.paymentSchedules = new ArrayList<>();
        this.client = null;
        System.out.println("[11:00] [Система] Тривале формування еталонного договору...");
        try {
            Thread.sleep(1500);
        } catch (InterruptedException e) {
            System.out.println("Формування договору було перервано!");
        }
    }
    public void addAddendum(Addendum addendum){
        if(addendum != null){
            addendums.add(addendum);
        }
    }
    public void addPaymentSchedule(PaymentSchedule schedule){
        if (schedule != null ){
            paymentSchedules.add(schedule);
        }
    }

    public void setClient(Client client) {
        this.client = client;
    }

    @Override
    public String toString() {
        return ("\nproductType='" + productType + '\'' +
                "\n, interestRate=" + interestRate +
                "\n, termMonths=" + termMonths +
                "\n, earlyTerminationTerms='" + earlyTerminationTerms + '\'' +
                "\n, client=" + client +
                "\n, paymentSchedules=" + paymentSchedules +
                "\n, addendums=" + addendums);
    }
    @Override
    protected Object clone() throws CloneNotSupportedException {
        DepositContract clone =  (DepositContract) super.clone();
        ArrayList<Addendum> addendums = new ArrayList<>();
        ArrayList<PaymentSchedule> paymentSchedules = new ArrayList<>();
        for (Addendum addendum: this.addendums){
            Addendum cloneAddendums = new Addendum(addendum);
            addendums.add(cloneAddendums);
        }
        for(PaymentSchedule item: this.paymentSchedules){
           PaymentSchedule clonePaymentSchedule = (PaymentSchedule) item.clone();
            paymentSchedules.add(clonePaymentSchedule);

        }
        clone.addendums = addendums;
        clone.paymentSchedules = paymentSchedules;
        return clone;
    }
}
