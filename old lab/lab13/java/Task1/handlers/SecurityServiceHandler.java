package Task1.handlers;

import Task1.model.LoanApplication;

public class SecurityServiceHandler extends ApprovalHandler {
    private double minCheckAmount;
    public SecurityServiceHandler(double minCheckAmount) {
        this.minCheckAmount = minCheckAmount;
    }
    @Override
    public void process(LoanApplication application) {
        System.out.println("[" + getCurrentTime() + "] [SecurityServiceHandler" + "] Отримано заявку "
                + application.getApplicationId() + " на перевірку безпеки.");

        if (application.getAmount() > minCheckAmount) {
            System.out.println("[" + getCurrentTime() + "] [SecurityServiceHandler" + "] "
                    + "Служба безпеки провела поглиблену перевірку клієнта за заявкою "
                    + application.getApplicationId() + " та підтвердила безпечність операції.");
        } else {
            System.out.println("[" + getCurrentTime() + "] [SecurityServiceHandler" + "] "
                    + "Сума заявки не перевищує " + minCheckAmount + " грн, додаткова перевірка не потрібна.");
        }

        passToNext(application);
    }
    }
