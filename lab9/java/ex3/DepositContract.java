package ex3;

import java.util.ArrayList;
import java.util.List;

public class DepositContract {
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
        return "DepositContract{" +
                "productType='" + productType + '\'' +
                ", interestRate=" + interestRate +
                ", termMonths=" + termMonths +
                ", earlyTerminationTerms='" + earlyTerminationTerms + '\'' +
                ", client=" + client +
                ", paymentSchedules=" + paymentSchedules +
                ", addendums=" + addendums +
                '}';
    }
}
