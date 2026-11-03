using System;
using PrutCreditBank.Services;
// Завдання 1
using PrutCreditBank.Task1.Builders;
using PrutCreditBank.Task1.Services;
// Завдання 3
using PrutCreditBank.Task3.Factories;

namespace PrutCreditBank
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            //ЗАВД1
            Logger.Log("System", "\n--- Виконання Завдання 1: Кредитна заявка ---");
            
            try
            {
                var customApp = new CreditApplicationBuilder()
                    .SetClient("Іваненко І. І.")
                    .SetAmount(300000)
                    .SetTerm(36)
                    .SetPurpose("Розвиток бізнесу")
                    .SetCollateral("Нерухомість")
                    .SetGuarantor("Петренко П. П.")
                    .SetIncomeCertificate(true)
                    .SetInsurance(true)
                    .Build();

                customApp.DisplayInfo();
            }
            catch (Exception ex) { Console.WriteLine($"[Помилка] {ex.Message}"); }

            try
            {
                var director = new CreditDirector(new CreditApplicationBuilder());
                var standardApp = director.BuildConsumerLoan("Ковальчук О. В.");
                standardApp.DisplayInfo();
            }
            catch (Exception ex) { Console.WriteLine($"[Помилка] {ex.Message}"); }

            try
            {
                var invalidApp = new CreditApplicationBuilder()
                    .SetClient("Шевченко Т. Г.")
                    .SetTerm(12)
                    .Build();
            }
            catch (Exception ex)
            {
                Logger.Log("Validation", $"{ex.Message}");
            }


            // ЗАВД3
            Logger.Log("System", "\n--- Виконання Завдання 3: Формування виписок ---");

            string period = "Жовтень 2026";
            decimal income = 150000.00m;
            decimal expense = 95000.00m;

            StatementGenerator clientGen = new ClientStatementGenerator();
            clientGen.ProcessStatement(period, income, expense);

            StatementGenerator taxGen = new TaxStatementGenerator();
            taxGen.ProcessStatement(period, income, expense);

            StatementGenerator companyGen = new CompanyStatementGenerator();
            companyGen.ProcessStatement(period, income, expense);

            Logger.Log("System", "--- Додавання нового адресата (Аудитор) без зміни коду процесів ---");
            StatementGenerator auditorGen = new AuditorStatementGenerator();
            auditorGen.ProcessStatement(period, income, expense);

            Logger.Log("System", "=== ВСІ СЦЕНАРІЇ УСПІШНО ВИКОНАНО ===");
        }
    }
}