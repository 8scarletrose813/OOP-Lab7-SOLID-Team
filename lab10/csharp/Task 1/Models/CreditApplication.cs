namespace PrutCreditBank.Models
{
    public class CreditApplication
    {
        public string Client { get; }
        public decimal Amount { get; }
        public int TermMonths { get; }
        public string Purpose { get; }
        public string Collateral { get; }
        public string Guarantor { get; }
        public bool HasIncomeCertificate { get; }
        public bool HasInsurance { get; }

        internal CreditApplication(
            string client, 
            decimal amount, 
            int termMonths, 
            string purpose, 
            string collateral, 
            string guarantor, 
            bool hasIncomeCertificate, 
            bool hasInsurance)
        {
            Client = client;
            Amount = amount;
            TermMonths = termMonths;
            Purpose = purpose;
            Collateral = collateral;
            Guarantor = guarantor;
            HasIncomeCertificate = hasIncomeCertificate;
            HasInsurance = hasInsurance;
        }

        public void DisplayInfo()
        {
            System.Console.WriteLine("--- Деталі кредитної заявки ---");
            System.Console.WriteLine($"  Клієнт: {Client ?? "Не вказано"}");
            System.Console.WriteLine($"  Сума: {Amount} грн");
            System.Console.WriteLine($"  Строк: {TermMonths} міс.");
            System.Console.WriteLine($"  Мета: {Purpose ?? "Не вказано"}");
            System.Console.WriteLine($"  Застава: {Collateral ?? "Відсутня"}");
            System.Console.WriteLine($"  Поручитель: {Guarantor ?? "Відсутній"}");
            System.Console.WriteLine($"  Довідка про доходи: {(HasIncomeCertificate ? "Так" : "Ні")}");
            System.Console.WriteLine($"  Страхування: {(HasInsurance ? "Так" : "Ні")}");
         
        }
    }
}