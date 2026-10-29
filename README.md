# Лабораторна робота №9: Породжувальні шаблони проєктування

**Варіант: 7** 
**Предметна область: Банк «Прут-Кредит»**

### Розподіл обов'язків у команді

| **Виконавець**       | **Мовний стек** | **Розподіл завдань** |
| -------------------- | --------------- | -------------------- |
| Резніченко Анастасія | **Java**        | Task1                |
| Батрак Орина         | **C#**          | Task 2               |
| Резніченко Анастасія | **Java**        | Task 3               |
| Кожен окремо         | **Java / C#**   | Task 4               |
| Спільна робота       | **Java / C#**   | Task 5               |
## Завдання 1: Курси валют у відділеннях (Шаблон Singleton)

### 1. Аналіз проблеми та вибір шаблону

У вихідній системі кожне відділення банку створювало власний екземпляр служби курсів валют, через що виникала розсинхронізація даних (різні відділення банку бачили різний курс USD в один і той самий час). Для вирішення цієї проблеми обрано шаблон **Singleton (Одинак)**, який гарантує наявність лише одного об'єкта `CurrencyRateService` на рівні всієї програми та надає глобальну точку доступу до нього.

### 2. Покроковий хід думок та реалізація

#### Крок 1. Модель даних (`CurrencyRate.java`)

Створено клас для збереження атрибутів конкретної валюти (`currencyCode`, `buyRate`, `sellRate`, `lastUpdated`).

Java

```
public class CurrencyRate {
    private String currencyCode;
    private double buyRate;
    double sellRate;
    private String lastUpdated;

    public CurrencyRate(String currencyCode, double buyRate, double sellRate, String lastUpdated) {
        this.currencyCode = currencyCode;
        this.buyRate = buyRate;
        this.sellRate = sellRate;
        this.lastUpdated = lastUpdated;
    }
    
    // геттери та сеттери...

    @Override
    public String toString() {
        return (currencyCode + " Купівля " + buyRate + " / Продаж " + sellRate + " Час: " + lastUpdated);
    }
}
```

#### Крок 2. Єдиний сервіс курсів (`CurrencyRateService.java`)

- Конструктор зроблено `private`, щоб унеможливити створення об'єкта через `new`.
    
- Додано потокобезпечний статичний метод `getInstance()` з ключовим словом `synchronized`.
    
- Курси зберігаються в `HashMap<String, CurrencyRate>`.
    

Java

```
public class CurrencyRateService {
    private static CurrencyRateService instance;
    private Map<String, CurrencyRate> rates;

    private CurrencyRateService() {
        this.rates = new HashMap<>();
        System.out.println("[10:00][Казначейство] Службу курсів валют ініціалізовано.");
    }

    public static synchronized CurrencyRateService getInstance() {
        if (instance == null) {
            instance = new CurrencyRateService();
        }
        return instance;
    }

    public void setRate(String currency, double buy, double sell, String time) {
        if (rates.get(currency) != null) {
            rates.get(currency).setBuyRate(buy);
            rates.get(currency).setSellRate(sell);
            rates.get(currency).setLastUpdated(time);
        } else {
            rates.put(currency, new CurrencyRate(currency, buy, sell, time));
        }
    }

    public CurrencyRate getRate(String currency) {
        return rates.get(currency);
    }
}
```

#### Крок 3. Перевірка та демонстрація (`Task1.java`)

Перевіряємо, що три різні змінні (`branch3`, `branch5`, `onlineBanking`) звертаються до одного й того самого екземпляра в пам'яті.

Java

```
public class Task1 {
    public static void main(String[] args) {
        CurrencyRateService branch3 = CurrencyRateService.getInstance();
        branch3.setRate("USD", 41.20, 41.70, "10:01");

        CurrencyRateService branch5 = CurrencyRateService.getInstance();
        CurrencyRateService onlineBanking = CurrencyRateService.getInstance();

        // Усі відділення та онлайн-банкінг бачать однаковий курс
        System.out.println(branch3.getRate("USD"));
        System.out.println(branch5.getRate("USD"));
        System.out.println(onlineBanking.getRate("USD"));

        // Доказ єдиного об'єкта в пам'яті за хеш-кодом
        System.out.println(System.identityHashCode(branch3));
        System.out.println(System.identityHashCode(branch5));
        System.out.println(System.identityHashCode(onlineBanking));

        // Зміна курсу в одній точці миттєво відображається всюди
        branch3.setRate("USD", 41.30, 41.80, "10:05");
        System.out.println(onlineBanking.getRate("USD"));
    }
}
```
## Завдання 2. Object Pool — з'єднання з платіжною системою

**Проблема.** Платіжна система дозволяє ≤ 4 одночасних захищених з'єднань; встановлення з'єднання дороге.
Під кожен платіж відкривалося нове з'єднання → блокування банку за перевищення ліміту.

**Рішення.** `PaymentConnectionPool` ліниво створює до 4 з'єднань, видає їх через `TryAcquire`,
після платежу `Release` очищує дані (`Reset`) і повертає з'єднання у вільні — без повторного встановлення.

**Стратегія при нестачі з'єднань — FIFO-черга** (`PaymentGateway`): платіж не губиться (на відміну від відмови),
порядок обробки справедливий, ліміт платіжної системи не порушується. Недолік — зростання затримки в пік;
для критичних платежів можна додати пріоритетну чергу.

**Очікуваний журнал (скорочено):** встановлення C-1…C-4 → «З'єднання C-1 → платіж №1001, 15 000 грн» …
платежі №1005–1007 у черзі → «C-1 повернено, дані платежу очищено» → «З'єднання C-1 → платіж №1005» …
статистика: C-1: 2, C-2: 2, C-3: 2, C-4: 1; встановлено з'єднань — 4.


## Завдання 3: Оформлення договорів (Шаблон Prototype)

### 1. Аналіз проблеми та вибір шаблону

Створення банківського договору з нуля є "дорогою" операцією через необхідність тривалої підготовки даних. При цьому договори одного продукту є типовими та відрізняються лише клієнтом і доповненнями.

Звичайне поверхневе копіювання призводило до того, що зміни у договорі одного клієнта псували еталонний шаблон. Для розв'язання проблеми обрано шаблон **Prototype (Прототип)** із реалізацією **глибокого копіювання (Deep Copy)**.

### 2. Покроковий хід думок та реалізація

#### Крок 1. Конструктор копіювання та clone у вкладених класах

- Для `Addendum.java` реалізовано конструктор копіювання.
    
- Для `PaymentSchedule.java` реалізовано метод `clone()`.
    

Java

```
// Addendum.java — Конструктор копіювання
public Addendum(Addendum other) {
    this.title = other.title;
    this.description = other.description;
}

// PaymentSchedule.java — Cloneable
@Override
public Object clone() throws CloneNotSupportedException {
    return super.clone();
}
```

#### Крок 2. Реалізація глибокого копіювання у `DepositContract.java`

- У конструкторі додано штучну затримку `Thread.sleep(1500)` для емуляції "дорогого" створення.
    
- Метод `clone()` створює нові колекції `ArrayList` і копіює кожен елемент окремо, забезпечуючи повну незалежність копії.
    

Java

```
public class DepositContract implements Cloneable {
    private String productType;
    private double interestRate;
    private int termMonths;
    private String earlyTerminationTerms;
    private Client client;
    private ArrayList<PaymentSchedule> paymentSchedules = new ArrayList<>();
    private ArrayList<Addendum> addendums = new ArrayList<>();

    // "Дорогий" конструктор
    public DepositContract(String productType, double interestRate, int termMonths, String earlyTerminationTerms) {
        this.productType = productType;
        this.interestRate = interestRate;
        this.termMonths = termMonths;
        this.earlyTerminationTerms = earlyTerminationTerms;
        try {
            System.out.println("[11:00] [Система] Тривале формування еталонного договору...");
            Thread.sleep(1500);
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
    }

    // Глибоке копіювання (Deep Copy)
    @Override
    protected Object clone() throws CloneNotSupportedException {
        DepositContract clone = (DepositContract) super.clone();
        ArrayList<Addendum> addendums = new ArrayList<>();
        ArrayList<PaymentSchedule> paymentSchedules = new ArrayList<>();

        for (Addendum addendum : this.addendums) {
            Addendum cloneAddendums = new Addendum(addendum);
            addendums.add(cloneAddendums);
        }
        for (PaymentSchedule item : this.paymentSchedules) {
            PaymentSchedule clonePaymentSchedule = (PaymentSchedule) item.clone();
            paymentSchedules.add(clonePaymentSchedule);
        }

        clone.addendums = addendums;
        clone.paymentSchedules = paymentSchedules;
        return clone;
    }
}
```

#### Крок 3. Перевірка незалежності об'єктів у `Task3.java`

Створюємо еталонний шаблон, клонуємо його для 3 клієнтів (миттєво, без повторної затримки в 1.5 с) та модифікуємо договір першого клієнта.

Java

```
public class Task3 {
    public static void main(String[] args) throws CloneNotSupportedException {
        // 1. Створення еталонного договору (із затримкою 1.5 с)
        DepositContract depositContract = new DepositContract("Депозит Стабільний", 15.0, 12, "Виплата 50% відсотків при достроковому закритті");
        depositContract.addAddendum(new Addendum("Базове обслуговування", "Стандартний пакет послуг"));
        depositContract.addPaymentSchedule(new PaymentSchedule("01.12.2026", 1250.0));

        // 2. Миттєве клонування для 3 клієнтів
        Client client1 = new Client("Шевченко Тарас Григорович", "UA44300001000002600123456789");
        DepositContract contract1 = (DepositContract) depositContract.clone();
        contract1.setClient(client1);

        Client client2 = new Client("Українка Леся Петрівна", "UA44300001000002600987654321");
        DepositContract contract2 = (DepositContract) depositContract.clone();
        contract2.setClient(client2);

        Client client3 = new Client("Франко Іван Якович", "UA44300001000002600555555555");
        DepositContract contract3 = (DepositContract) depositContract.clone();
        contract3.setClient(client3);

        // 3. Модифікація договору лише Першого клієнта
        contract1.addAddendum(new Addendum("Персональний менеджер", "Право на дострокове зняття до 20% суми"));
        contract1.addPaymentSchedule(new PaymentSchedule("01.01.2027", 1300.0));

        // 4. Доказ незалежності: еталон не змінився
        System.out.println("Еталонний договір:");
        System.out.println(depositContract);

        System.out.println("Змінений договір клієнта 1:");
        System.out.println(contract1);
    }
}
```
