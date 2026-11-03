using System;
using PrutCreditBank.Services;

namespace PrutCreditBank.Task3.Models
{
    public abstract class BankStatement
    {
        public string Period { get; }
        public decimal TotalIncome { get; }
        public decimal TotalExpense { get; }

        protected BankStatement(string period, decimal totalIncome, decimal totalExpense)
        {
            Period = period;
            TotalIncome = totalIncome;
            TotalExpense = totalExpense;
        }

        public abstract void GenerateAndSend();
    }


    public class PdfClientStatement : BankStatement
    {
        public PdfClientStatement(string period, decimal income, decimal expense) 
            : base(period, income, expense) { }

        public override void GenerateAndSend()
        {
            Logger.Log("Document", $"Сформовано виписку у форматі **PDF** для Клієнта за період {Period}. Доходи: {TotalIncome}, Витрати: {TotalExpense}. Надіслано клієнту.");
        }
    }


    public class XmlTaxStatement : BankStatement
    {
        public XmlTaxStatement(string period, decimal income, decimal expense) 
            : base(period, income, expense) { }

        public override void GenerateAndSend()
        {
            Logger.Log("Document", $"Сформовано виписку у форматі **XML** для Податкової служби за період {Period}. Надсилання електронного звіту.");
        }
    }


    public class CsvCompanyStatement : BankStatement
    {
        public CsvCompanyStatement(string period, decimal income, decimal expense) 
            : base(period, income, expense) { }

        public override void GenerateAndSend()
        {
            Logger.Log("Document", $"Сформовано виписку у форматі **CSV** для Бухгалтерії підприємства за період {Period}. Імпорт у внутрішню систему.");
        }
    }

    public class AuditorStatement : BankStatement
    {
        public AuditorStatement(string period, decimal income, decimal expense) 
            : base(period, income, expense) { }

        public override void GenerateAndSend()
        {
            Logger.Log("Document", $"Сформовано розширену виписку для **Аудиторів** за період {Period}. Повний аудит транзакцій виконано.");
        }
    }
}