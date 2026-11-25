using PrutCredit.Lab14.Task1.Common;
using PrutCredit.Lab14.Task1.Interfaces;
using PrutCredit.Lab14.Task1.Model;

namespace PrutCredit.Lab14.Task1.Services;

public class LoanMediator : ILoanMediator
{
    private readonly ILoanManager _manager;
    private readonly IScoringService _scoring;
    private readonly ISecurityService _security;
    private readonly ILegalDepartment _legal;
    private readonly ICashier _cashier;

    public LoanMediator(
        ILoanManager manager,
        IScoringService scoring,
        ISecurityService security,
        ILegalDepartment legal,
        ICashier cashier)
    {
        _manager = manager;
        _scoring = scoring;
        _security = security;
        _legal = legal;
        _cashier = cashier;

        foreach (var participant in new ILoanParticipant[] { manager, scoring, security, legal, cashier })
        {
            participant.SetMediator(this);
        }
    }

    public void Notify(ILoanParticipant sender, LoanEvent loanEvent, LoanApplication application)
    {
        Log($"отримав «{Describe(loanEvent)}» від «{sender.Name}» по заявці #{application.Id}");

        switch (loanEvent)
        {
            case LoanEvent.ApplicationSubmitted:
                StartReview(application);
                break;
            case LoanEvent.ScoringPassed:
                application.ScoringApproved = true;
                ContinueIfApproved(application);
                break;
            case LoanEvent.SecurityPassed:
                application.SecurityApproved = true;
                ContinueIfApproved(application);
                break;
            case LoanEvent.ScoringFailed:
            case LoanEvent.SecurityFailed:
                Reject(application, sender);
                break;
            case LoanEvent.ContractSigned:
                application.Status = LoanStatus.ContractSigned;
                Log($"договір підписано, передає заявку #{application.Id} касиру");
                _cashier.IssueFunds(application);
                break;
            case LoanEvent.FundsIssued:
                application.Status = LoanStatus.Issued;
                Log($"кошти видано, повідомляє менеджера по заявці #{application.Id}");
                _manager.OnCompleted(application);
                break;
        }
    }

    private void StartReview(LoanApplication application)
    {
        application.Status = LoanStatus.UnderReview;
        Log($"запускає скоринг і службу безпеки по заявці #{application.Id}");
        _scoring.Evaluate(application);

        if (application.Status != LoanStatus.Rejected)
        {
            _security.Check(application);
        }
    }

    private void ContinueIfApproved(LoanApplication application)
    {
        if (application.Status == LoanStatus.Rejected)
        {
            return;
        }

        if (application.ScoringApproved == true && application.SecurityApproved == true)
        {
            application.Status = LoanStatus.ContractPreparation;
            Log($"обидва висновки позитивні, передає заявку #{application.Id} юристам");
            _legal.PrepareContract(application);
        }
    }

    private void Reject(LoanApplication application, ILoanParticipant source)
    {
        if (application.Status == LoanStatus.Rejected)
        {
            return;
        }

        application.Status = LoanStatus.Rejected;
        Log($"зупиняє процес по заявці #{application.Id}, причина: відмова «{source.Name}»");
        _manager.OnRejected(application, source.Name);
    }

    private static void Log(string message)
    {
        EventLog.Write("Координатор", message);
    }

    private static string Describe(LoanEvent loanEvent) => loanEvent switch
    {
        LoanEvent.ApplicationSubmitted => "подання заявки",
        LoanEvent.ScoringPassed => "позитивний висновок скорингу",
        LoanEvent.ScoringFailed => "негативний висновок скорингу",
        LoanEvent.SecurityPassed => "позитивний висновок служби безпеки",
        LoanEvent.SecurityFailed => "негативний висновок служби безпеки",
        LoanEvent.ContractSigned => "підписання договору",
        LoanEvent.FundsIssued => "видачу коштів",
        _ => loanEvent.ToString()
    };
}
