using PrutCreditBank.Task3.Models;

namespace PrutCreditBank.Task3.Factories
{

    public class ClientStatementGenerator : StatementGenerator
    {
        public override BankStatement CreateStatement(string period, decimal income, decimal expense)
        {
            return new PdfClientStatement(period, income, expense);
        }
    }

    public class TaxStatementGenerator : StatementGenerator
    {
        public override BankStatement CreateStatement(string period, decimal income, decimal expense)
        {
            return new XmlTaxStatement(period, income, expense);
        }
    }

    public class CompanyStatementGenerator : StatementGenerator
    {
        public override BankStatement CreateStatement(string period, decimal income, decimal expense)
        {
            return new CsvCompanyStatement(period, income, expense);
        }
    }

    public class AuditorStatementGenerator : StatementGenerator
    {
        public override BankStatement CreateStatement(string period, decimal income, decimal expense)
        {
            return new AuditorStatement(period, income, expense);
        }
    }
}