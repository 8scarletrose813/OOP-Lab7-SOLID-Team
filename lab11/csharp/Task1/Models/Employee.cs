//Для перевірки клієнта банк звертається до зовнішнього бюро кредитних історій. 
// Кожен запит платний і виконується кілька секунд.
namespace PrutCreditBank.StructuralPatterns.Task1_CreditBureau.Models;

public sealed record Employee(string Name, EmployeeRole Role);
