using BankIterator.Interfaces;
using BankIterator.Models;

namespace BankIterator.Services;

public sealed class ScenarioRunner
{
    private const decimal MonitoringThreshold = 400_000m;

    private readonly IOperationCollection _operations;
    private readonly EventLog _log;

    public ScenarioRunner(IOperationCollection operations, EventLog log)
    {
        _operations = operations;
        _log = log;
    }

    public void ShowLatestOperations(int count)
    {
        _log.Write("Сценарій", $"Онлайн-банкінг: {count} останніх операцій з усіх рахунків клієнта");
        _log.Write("Сховище", $"До обходу: {_operations.Describe()}");

        Traverse("OnlineBanking", _operations.CreateNewestFirstIterator(count));

        _log.Write("Сховище", $"Після обходу: {_operations.Describe()}");
    }

    public void ShowYearReport(int year)
    {
        _log.Write("Сценарій", $"Податковий звіт: операції за {year} рік у хронологічному порядку");
        _log.Write("Сховище", $"До обходу: {_operations.Describe()}");

        Traverse("TaxReport", _operations.CreateYearIterator(year));

        _log.Write("Сховище", $"Після обходу: {_operations.Describe()}");
    }

    public void ShowFinancialMonitoringWithParallelTraversal()
    {
        _log.Write("Сценарій",
            $"Фінмоніторинг: операції понад {MonitoringThreshold:N0} грн, паралельно з обходом онлайн-банкінгу");
        _log.Write("Сховище", $"До обходів: {_operations.Describe()}");

        var monitoring = _operations.CreateThresholdIterator(MonitoringThreshold);
        var banking = _operations.CreateNewestFirstIterator(5);

        _log.Write("FinMonitoring", $"Ітератор до обходу: {State(monitoring)}");
        _log.Write("OnlineBanking", $"Ітератор до обходу: {State(banking)}");

        var monitored = 0;
        var shown = 0;
        while (monitoring.HasNext() || banking.HasNext())
        {
            if (monitoring.HasNext())
            {
                _log.Write("FinMonitoring", $"{++monitored}. {monitoring.Next()}");
            }

            if (banking.HasNext())
            {
                _log.Write("OnlineBanking", $"{++shown}. {banking.Next()}");
            }
        }

        _log.Write("FinMonitoring", $"Ітератор після обходу: {State(monitoring)}, переглянуто: {monitored}");
        _log.Write("OnlineBanking", $"Ітератор після обходу: {State(banking)}, переглянуто: {shown}");
        _log.Write("Сценарій",
            $"Обходи незалежні: кожен пройшов власну послідовність (фінмоніторинг {monitored}, онлайн-банкінг {shown})");
        _log.Write("Сховище", $"Після обходів: {_operations.Describe()}");
    }

    private int Traverse(string source, IIterator<Operation> iterator)
    {
        _log.Write(source, $"Ітератор до обходу: {State(iterator)}");

        var count = 0;
        while (iterator.HasNext())
        {
            _log.Write(source, $"{++count}. {iterator.Next()}");
        }

        _log.Write(source, $"Ітератор після обходу: {State(iterator)}, переглянуто: {count}");
        return count;
    }

    private static string State(IIterator<Operation> iterator) =>
        iterator.HasNext() ? "є наступна операція" : "операцій більше немає";
}
