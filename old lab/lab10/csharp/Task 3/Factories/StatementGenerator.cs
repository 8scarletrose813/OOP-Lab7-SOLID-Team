using PrutCreditBank.Task1.Services;
using PrutCreditBank.Task3.Models;

namespace PrutCreditBank.Task3.Factories
{

    public abstract class StatementGenerator
    {
        public abstract BankStatement CreateStatement(string period, decimal income, decimal expense);

        public void ProcessStatement(string period, decimal income, decimal expense)
        {
            Logger.Log("Process", $"1. Вибір операцій та транзакцій за період: {period}...");
            Logger.Log("Process", $"2. Обчислення підсумків (Доходи: {income}, Витрати: {expense})...");
            
            BankStatement statement = CreateStatement(period, income, expense);
            
            Logger.Log("Process", "3. Фіксація та підготовка документа...");
            statement.GenerateAndSend();
            Logger.Log("Process", "4. Процес обробки виписки успішно завершено.\n");
        }
    }
}