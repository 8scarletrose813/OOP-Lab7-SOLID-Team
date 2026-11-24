package Task1.handlers;

import Task1.model.LoanApplication;

public class CreditCommitteeHandler extends ApprovalHandler {
    private double maxAmount;

    public CreditCommitteeHandler (double maxAmount) {
        this.maxAmount = maxAmount;
    }
    @Override
    public void process(LoanApplication application) {
        System.out.println("[" + getCurrentTime() + "] [" + "CreditCommitteeHandler" + "] Отримано заявку "
                + application.getApplicationId() + " на суму " + application.getAmount());
        if(application.getAmount() <= maxAmount){
            System.out.println("[" + getCurrentTime() + "] [CreditCommitteeHandler" + "] Заявку "
                    + application.getApplicationId() + " погоджено на суму " + application.getAmount());
            return;
        } else {
            System.out.println("[" + getCurrentTime() + "] [CreditCommitteeHandler" + "] Сума перевищує ліміт ("
                    + maxAmount + "), передача заявки далі");
            passToNext(application);
        }
    }
}
