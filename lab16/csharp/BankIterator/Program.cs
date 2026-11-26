using System.Globalization;
using System.Text;
using BankIterator.Collections;
using BankIterator.Services;

Console.OutputEncoding = Encoding.UTF8;
CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

var client = DemoDataFactory.CreateClient();
var operations = new ClientOperations(client);
var log = new EventLog(new DateTime(2026, 11, 25, 9, 0, 0));
var runner = new ScenarioRunner(operations, log);

runner.ShowLatestOperations(10);
Console.WriteLine();
runner.ShowYearReport(2025);
Console.WriteLine();
runner.ShowFinancialMonitoringWithParallelTraversal();
