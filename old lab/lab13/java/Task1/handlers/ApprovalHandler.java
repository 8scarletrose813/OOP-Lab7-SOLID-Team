package Task1.handlers;

import Task1.model.LoanApplication;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;

public abstract class ApprovalHandler {
    protected ApprovalHandler nextHandler;
    public void setNext(ApprovalHandler nextHandler){
        this.nextHandler = nextHandler;
    }
    protected String getCurrentTime(){
        DateTimeFormatter time = DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss");
        return LocalDateTime.now().format(time);
    }
    protected void passToNext(LoanApplication application){
        if (nextHandler != null){
            nextHandler.process(application);
        }
        else{
            System.out.println("[" + getCurrentTime() + "]  [ApprovalHandler] Заявку #" + application.getApplicationId()
                    + " на суму " + application.getAmount() + " не вдалося погодити: ланцюг обробки завершився.");
        }

    }
    public abstract void process(LoanApplication application);
}
