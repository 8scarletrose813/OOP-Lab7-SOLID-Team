using PrutCreditBank.Task1.Builders;
using PrutCreditBank.Task1.Models;

namespace PrutCreditBank.Task1.Services
{
    public class CreditDirector
    {
        private readonly ICreditApplicationBuilder _builder;

        public CreditDirector(ICreditApplicationBuilder builder)
        {
            _builder = builder;
        }

        public CreditApplication BuildConsumerLoan(string clientName)
        {
            Logger.Log("Director", "Формування типової заявки «Споживчий кредит».");
            return _builder
                .SetClient(clientName)
                .SetAmount(50000)
                .SetTerm(12)
                .SetPurpose("Споживчі потреби")
                .SetIncomeCertificate(true)
                .SetInsurance(false)
                .Build();
        }

        public CreditApplication BuildAutoLoan(string clientName, string carModel)
        {
            Logger.Log("Director", $"Формування типового «Автокредиту» на авто {carModel}.");
            return _builder
                .SetClient(clientName)
                .SetAmount(400000)
                .SetTerm(60)
                .SetPurpose($"Купівля автомобіля {carModel}")
                .SetCollateral(carModel)
                .SetIncomeCertificate(true)
                .SetInsurance(true)
                .Build();
        }
    }
}