# OOP-Lab7-SOLID-Team
Team project for Lab 7: SOLID principles and pragmatic development in Java &amp; C#

**Варіант:** 7 

### Команда:
* **Студент А (Java):** Резніченко Анастасія
* **Студент Б (C#):** Батрак Орина
## Розподіл завдань
| Завдання | Принцип / Тема | Стек | Виконавець | 
| :--- | :--- | :--- | :--- | 
| **Task 1** | Single Responsibility Principle (SRP) | С# | Батрак Орина| 
| **Task 2** | Open/Closed Principle (OCP) | Java | Резніченко Анастасія | 
| **Task 3** | Liskov Substitution Principle (LSP) | Java | Резніченко Анастасія| 
| **Task 4** | Interface Segregation Principle (ISP) | Java | Резніченко Анастасія|
| **Task 5** | Dependency Inversion Principle (DIP) | C# | Батрак Орина |
| **Task 6** | Прагматичний аналіз & Cross-Review | Java / C# | Спільно |

## Аналіз прагматичних принципів (DRY, KISS, YAGNI) та Крос-рев'ю

### Java
#### Таблиця Крос-рев'ю та аналізу SOLID (Завдання 2, 3, 4)

| Завдання | Використаний принцип                        | Мета principles                                                                                                | Що було зроблено & Що вийшло в результаті                                                                                                                                                                                                                                                  |
| -------- | ------------------------------------------- | -------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| Task 2   | **OCP** _(Open/Closed Principle)_           | Дозволити додавати новий функціонал без ризику зламати вже працюючий та протестований код.                     | **До:** У `ReportExporter` був жорсткий `switch` за форматами.<br><br>**Після:** Виділено інтерфейс `ReportFormatter` (патерн Strategy). Новий формат `MarkdownReportFormatter` додано **без зміни жодного рядка** в базовому експортері.                                                  |
| Task 3   | **LSP** _(Liskov Substitution Principle)_   | Гарантувати, що будь-який підтип можна безпечно використовувати замість базового класу без винятків у Runtime. | **До:** `VolunteerEmployee` успадковував `Employee` і викидав винятки при виплаті зарплати.<br><br>**Після:** Фінансові методи винесено у `SalariedEmployee`. `PayrollService` приймає лише `SalariedEmployee`, що забезпечило перевірку типів ще під час компіляції (Compile-time Safety) |
| Task 4   | **ISP** _(Interface Segregation Principle)_ | Захистити класи від залежностей від методів, які вони насправді не використовують.                             | **До:** Монолітний `IUserManagementService` змушував класи реалізовувати непотрібні методи з `UnsupportedOperationException`.<br><br>**Після:** Моноліт розділено на 5 рольових інтерфейсів. Класи імплементують тільки потрібні їм контракти, усі методи-заглушки видалено.               |
#### Таблиця Прагматичних принципів (DRY, KISS, YAGNI)

| **Принцип**                            | Мета принципу                                                       | Як реалізовано у моїх завданнях (2, 3, 4)                                                                                                                        | Кінцевий результат                                                          |
| -------------------------------------- | ------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------- |
| **DRY**(Don't Repeat Yourself)         | Усунути дублювання бізнес-правил та створити єдине джерело правди.  | Уся логіка розрахунку бонуса та перевірки зарплат сконцентрована в `SalariedEmployee`. Правила форматування кожного звіту винесені у відповідні класи-стратегії. | Позбулися дублювання перевірок типів та умовних операторів у сервісах.      |
| **KISS**(Keep It Simple, Stupid)       | Спростити код, прибравши складні розгалуження та приховані винятки. | Усунуто складний `switch` у звітних форматах та видалено всі виклики `UnsupportedOperationException` у сервісах акаунтів.                                        | Код став лінійним, легким для читання та без прихованих винятків у Runtime. |
| **YAGNI** _(You Aren't Gonna Need It)_ | Видалити невикористовуваний код, заглушки та рішення «на майбутнє». | Видалено застарілий монолітний інтерфейс `IUserManagementService` та всі 6 методів-заглушок у сервісах.                                                          | Кодова база містить лише той код, який дійсно викликається системою.        |

### Хід думок та аналіз змін у код
**Завдання 2. Open/Closed Principle (OCP**)
**Що взяли:** Клас `ReportExporter` та логіку форматування звітів.

Що було "До"

```java
``` public String export(ReportData data, ExportFormat format) {
        StringBuilder builder = new StringBuilder();
        switch (format) {
            case PDF:
		        //PDF
                break;
	        case EXCEL:
                //Exel
                break;
            case CSV:
                //CSV
                }
                break;
            case JSON:
                //JSON
	            break;
            default: throw new UnsupportedOperationException(...);}
        System.out.println("Вивід");
        return builder.toString();
    }
```
	
Після: створено інтерфейс ReportFormatter
	
```Java
package variant7_reportexporter;

public interface ReportFormatter {
    String formatReport(ReportData data);
}
```

Та кожен формат винесено в окремий клас (`JsonReportFormatter`, `XmlReportFormatter`, `MarkdownReportFormatter`) та оновлений ReportExporter.

```java
public class ReportExporter { 
	public void export(ReportData data, ReportFormatter formatter){              formatter.format(data);
	} 
}
```

Завдання 3. Liskov Substitution Principle (LSP)
**До (Проблема):** `VolunteerEmployee` успадковував `Employee`, але викидав виняток у фінансових методах.

```java
public class VolunteerEmployee extends Employee {
    private String organizationName;

    public VolunteerEmployee(String employeeId, String fullName, String organizationName) {
        // Конструктор 
    }

    @Override
    public void setSalary(BigDecimal newSalary) {
        // Порушення LSP: Волонтери працюють на безоплатній основі
    }

    @Override
    public BigDecimal calculateAnnualBonus(double performanceRating) {
        // Порушення LSP: Відмова від виплати фінансових бонусів
    }
}
```

**Після:** Базовий `Employee` залишено з загальними даними (ID, ПІБ). Фінансову логіку винесено у `SalariedEmployee`. Сервіс `PayrollService` переведено на роботу з `SalariedEmployee`.

```java
public class PayrollService {
    public void processYearEndBonuses(SalariedEmployee salariedEmployee, double rating) {
        BigDecimal bonus = salariedEmployee.calculateAnnualBonus(rating);
        System.out.println("Bonus for " + salariedEmployee.getFullName() + ": $" + bonus);
    }
}
```

Завдання 4. Interface Segregation Principle (ISP)

**До (Проблема):** Монолітний інтерфейс `IUserManagementService` змушував класи імплементувати непотрібні методи та викидати винятки.

```java
public interface IUserManagementService {
    void registerUser(String userIdentifier, String secretKey);
    boolean authenticate(String userIdentifier, String secretKey);
    void resetSecret(String userIdentifier);
    void assignRole(int userId, String role);
    void auditUserLogs(int userId);
    void exportUserDataGdpr(int userId);
}
```

та 

```java
public class AuthGatewayService implements IUserManagementService{
    @Override public boolean authenticate(String userIdentifier, String secretKey) { return true; }
    @Override public void resetSecret(String userIdentifier) { System.out.println("Gateway secret reset for " + userIdentifier); }
    // 3 не потрібних методи
}
```

**Після:** Моноліт розділено на 5 вузьких рольових інтерфейсів. Класи імплементують лише необхідні контракти.

```java
public class AuthGatewayService implements IAuthenticationService { @Override public boolean authenticate(String userIdentifier, String secretKey) { return true; } @Override public void resetSecret(String userIdentifier) { /* логіка */ } }
```
