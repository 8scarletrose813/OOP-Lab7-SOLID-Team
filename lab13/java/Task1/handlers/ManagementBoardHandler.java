package Task1.handlers;

import Task1.model.LoanApplication;

public class ManagementBoardHandler extends ApprovalHandler {

    @Override
    public void process(LoanApplication application) {
        System.out.println("[" + getCurrentTime() + "] [ManagementBoardHandler" + "] Правління банку отримало заявку "
                + application.getApplicationId() + " на суму " + application.getAmount());

        System.out.println("[" + getCurrentTime() + "] [ManagementBoardHandler" + "] Заявку "
                + application.getApplicationId() + " на суму " + application.getAmount()
                + " остаточно погоджено Правлінням банку.");
    }
}
