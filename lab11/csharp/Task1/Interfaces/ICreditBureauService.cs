using PrutCreditBank.StructuralPatterns.Common.Models;
using PrutCreditBank.StructuralPatterns.Task1_CreditBureau.Models;

namespace PrutCreditBank.StructuralPatterns.Task1_CreditBureau.Interfaces;

public interface ICreditBureauService
{
    CreditHistory GetCreditHistory(Employee requester, Client client);
}
