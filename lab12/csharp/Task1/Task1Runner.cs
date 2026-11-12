using Lab12_Variant7.Common;
using Lab12_Variant7.Task1_BankStructure.Models;
using Lab12_Variant7.Task1_BankStructure.Services;

namespace Lab12_Variant7.Task1_BankStructure;

public static class Task1Runner
{
    public static void Run()
    {
        Logger.Log("Завдання 1", "Структура банку і звітність (Composite)");

        var bank = new BankDivision("Банк Прут-Кредит");

        var north = new BankDivision("Північне регіональне управління");
        var south = new BankDivision("Південне регіональне управління");
        bank.Add(north);
        bank.Add(south);

        var chernivtsi = new BankDivision("Відділення Чернівці-Центр");
        chernivtsi.Add(new Cashier("Каса 1", 120_000m, 85));
        chernivtsi.Add(new Cashier("Каса 2", 95_500m, 60));

        var miniBranch = new BankDivision("Міні-відділення Рів'єра");
        miniBranch.Add(new Cashier("Каса 1", 30_000m, 25));
        chernivtsi.Add(miniBranch);
        north.Add(chernivtsi);

        var kitsman = new BankDivision("Відділення Кіцмань");
        kitsman.Add(new Cashier("Каса 1", 70_000m, 40));
        north.Add(kitsman);

        var khotyn = new BankDivision("Відділення Хотин");
        khotyn.Add(new Cashier("Каса 1", 55_000m, 32));
        khotyn.Add(new Cashier("Каса 2", 48_200m, 28));
        south.Add(khotyn);

        var storozhynets = new BankDivision("Відділення Сторожинець");
        storozhynets.Add(new Cashier("Каса 1", 64_300m, 37));
        var miniStorozhynets = new BankDivision("Міні-відділення Банчени");
        miniStorozhynets.Add(new Cashier("Каса 1", 18_000m, 12));
        storozhynets.Add(miniStorozhynets);
        south.Add(storozhynets);

        var report = new TreasuryReportService();

        Logger.Log("Завдання 1", "Структура банку:");
        report.PrintTree(bank);

        Logger.Log("Завдання 1", "Показники одним викликом для банку і регіону:");
        report.PrintSummary(bank);
        report.PrintSummary(north);
    }
}
