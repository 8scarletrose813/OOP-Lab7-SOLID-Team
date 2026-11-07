using PrutCreditBank.Task1.Models;

namespace PrutCreditBank.Task1.Builders
{
    public interface ICreditApplicationBuilder
    {
        ICreditApplicationBuilder SetClient(string client);
        ICreditApplicationBuilder SetAmount(decimal amount);
        ICreditApplicationBuilder SetTerm(int termMonths);
        ICreditApplicationBuilder SetPurpose(string purpose);
        ICreditApplicationBuilder SetCollateral(string collateral);
        ICreditApplicationBuilder SetGuarantor(string guarantor);
        ICreditApplicationBuilder SetIncomeCertificate(bool hasCertificate);
        ICreditApplicationBuilder SetInsurance(bool hasInsurance);
        CreditApplication Build();
    }
}