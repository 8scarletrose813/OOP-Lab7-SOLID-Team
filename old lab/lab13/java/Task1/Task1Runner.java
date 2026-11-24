package Task1;

import Task1.handlers.*;
import Task1.model.LoanApplication;

public class Task1Runner {

    public static void runScenario() {
        ApprovalHandler creditManager = new CreditManagerHandler(50000);
        ApprovalHandler branchManager = new BranchManagerHandler(500000);
        ApprovalHandler creditCommittee = new CreditCommitteeHandler(5000000);
        ApprovalHandler managementBoard = new ManagementBoardHandler();

        creditManager.setNext(branchManager);
        branchManager.setNext(creditCommittee);
        creditCommittee.setNext(managementBoard);

        System.out.println("Етап 2: Подача заявок на 30 000, 300 000 та 3 000 000 грн");
        LoanApplication app1 = new LoanApplication("КР-101", 30000);
        LoanApplication app2 = new LoanApplication("КР-102", 300000);
        LoanApplication app3 = new LoanApplication("КР-103", 3000000);
        creditManager.process(app1);
        System.out.println();
        creditManager.process(app2);
        System.out.println();
        creditManager.process(app3);
        System.out.println();

        System.out.println("Етап 3: Подача заявки на 10 000 000 грн");
        LoanApplication app4 = new LoanApplication("КР-104", 10000000);
        creditManager.process(app4);
        System.out.println();

        System.out.println("Етап 4: Додавання Служби безпеки та повторна заявка на 3 000 000 грн");
        ApprovalHandler securityService = new SecurityServiceHandler(1000000);

        branchManager.setNext(securityService);
        securityService.setNext(creditCommittee);

        LoanApplication app5 = new LoanApplication("КР-105", 3000000);
        creditManager.process(app5);
    }
    public static void main(String[] args) {
        runScenario();
    }
}