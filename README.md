# Лабораторна робота №8. Проєктування та рефакторинг за методологією GRASP
**Варіант:**  7 

### Розподіл обов'язків у команді

|    **Виконавець**    | **Мовний стек** | **Розподіл завдань** |
| :------------------: | :-------------: | :------------------: |
| Резніченко Анастасія |    **Java**     |        Task1         |
| Резніченко Анастасія |    **Java**     |        Task 2        |
| Резніченко Анастасія |    **Java**     |        Task 3        |
|     Батрак Орина     |     **C#**      |        Task 4        |
|     Батрак Орина     |     **C#**      |        Task 5        |
|    Спільна робота    |  **Java / C#**  |        Task 6        |
#### Завдання 1. Аудит Legacy-коду та Design Rationale (ADR)

Короткий опис виявлених проблем у вихідному коді та посилання на документ `docs/adr/ADR-001.md`

#### Завдання 2. Доменна інкапсуляція та конструювання (Information Expert + Creator)

##### Як працював Legacy-код (До рефакторингу):

- `Patient`, `Doctor` та `Appointment` були просто "мішками з даними". Вони самі нічого не вміли розраховувати та нічого не перевіряли.
```Java
public class Patient {
    public int id;
    public String fullName;
    public boolean hasInsurance;
    public String insurancePolicyNumber;
    public String phone;
}
```
**Чому це була проблема:** Через модифікатор `public` будь-який зовнішній клас міг напряму перезаписати внутрішній стан об'єкта невалідними значеннями в обхід будь-якої перевірки:
```Java
// Приклад того, що дозволяв робити legacy-код
appointment.consultationCost = -500.0; // Негативна ціна
patient.hasInsurance = false;          // Зміна страховки ззовні
```
    
- **Роздутий монолітний метод "все в одному":** Один метод `scheduleAppointmentConsole()` робив геть усе: 
	1. Зчитував дані з консолі через `Scanner`.
	2. Вручну створював порожні об'єкти й присвоював їм значення через `public`-поля (`patient.id = 22;`).
	3. Рахував коефіцієнти лікаря: *2.0 для професора, *1.4 для спеціаліста.
	4. Рахував 80% покриття страховки.
	5. Збирав текстову квитанцію вручну через конкатенацію рядків.
	6. Імітував запис у файл `appointments.log`.
	7. Імітував надсилання SMS.
```Java 
//Короткий приклад 
	public class LegacyClinicService {
    public void scheduleAppointmentConsole() {
        //1: Зчитування даних з консолі через Scanner
        Scanner scanner = new Scanner(System.in);
        System.out.print("Введіть категорію лікаря (General/Specialist/Professor): ");
        String docCategory = scanner.nextLine();
        // 2: Вручну створював порожні об'єкти й присвоював значення через public-поля
        Patient patient = new Patient();
        patient.id = 22; patient.fullName = name; patient.hasInsurance = true; 
        patient.insurancePolicyNumber = "INS-99201"; patient.phone = "+380684445566";
        // 3: Рахував коефіцієнти лікаря: 2.0 для професора, 1.4 для спеціаліста
        double fee = appointment.doctor.baseConsultationFee;
        if ("Professor".equalsIgnoreCase(appointment.doctor.category)) {fee *= 2.0;} 
        else if ("Specialist".equalsIgnoreCase(appointment.doctor.category)) {fee *= 1.4;} appointment.consultationCost = fee;
        //4: Рахував 80% покриття страховки та підсумкову суму
        double coverage = 0.0;
        if (appointment.patient.hasInsurance) {coverage = appointment.consultationCost * 0.80;}
        appointment.insuranceCoverage = coverage;
        appointment.patientPayable = appointment.consultationCost - appointment.insuranceCoverage;
        appointment.status = "Confirmed";
        //5: Збирав текстову квитанцію вручну через конкатенацію рядків
        System.out.println(invoice);
        //6: Імітував запис у файл appointments.log
        System.out.println("[Електронна карта] Запис #" + appointment.appointmentId + " збережено у файл appointments.log.");
        //7: Імітував надсилання SMS
        System.out.println("[SMS] Нагадування про прийом надіслано на номер " + appointment.patient.phone);}}
```
##### Що змінилось(Після рефакторингу):

1. Замінили модифікатори всіх полів з `public` на `private` та додали публічні геттери (`getId()`, `getPhone()`, `isHasInsurance()` тощо).
``` Java
public class Patient {
  //Його поля
    public Patient(int id, String fullName, boolean hasInsurance, String insurancePolicyNumber, String phone){
    //Конструктор
    }
    public int getId() {
        return id;
    }
    public String getFullName() {
        return fullName;
    }
    public boolean isHasInsurance() {
        return hasInsurance;
    }
    public String getInsurancePolicyNumber() {
        return insurancePolicyNumber;
    }
    public String getPhone() {
        return phone;
    }
}
```
**Навіщо:** Повністю закрили прямий доступ до внутрішнього стану об'єктів ззовні. Тепер жоден зовнішній клас не може випадково чи навмисно змінити дані в обхід бізнес-правил.
2. Перенесли розрахунок ціни з процедурного `if-else` блоку в Legacy-code у новий метод класу `Doctor`:
```Java
 public double calculateConsultationFee(){
        if("Professor".equalsIgnoreCase(category)){
            return baseConsultationFee * 2.0;
        }
        else if("Specialist".equalsIgnoreCase(category)){
            return baseConsultationFee * 1.4;
        }
        else{
            return baseConsultationFee;
        }
    }
```
- **Навіщо:** Клас `Doctor` володіє даними про базову ставку та категорію, тому за патерном **Information Expert** саме він має рахувати підсумкову вартість своїх послуг.
3. Перенесли розрахунок страхового покриття та формування квитанції всередину `Appointment`:
```Java
//Створили метод розрахунку страховки: `calculateInsuranceCoverage()` (перевіряє `patient.isHasInsurance()` і бере 80% від ціни).

    private double calculateInsuranceCoverage(){
        if(patient.isHasInsurance()){
            return this.consultationCost * 0.80;
        }
        else {
            return 0.0;
        }
    }
//Додали методи управління станом: `confirm()` та `cancel()`.
	public void confirm(){
        if ("Cancelled".equalsIgnoreCase(this.status)){
            throw new IllegalStateException("Неможливо підтвердити скасований прийом");
        }
        this.status = "Confirmed";
    }
    public void cancel() {
        this.status = "Cancelled";
    }
//Додали бізнес-метод `generateInvoice()`, який сам збирає підсумковий рядок чека з внутрішніх даних.
    public String generateInvoice(){
		return "Квитанція прийому #" + appointmentId +
            "\nПацієнт: " + patient.getFullName() +
            "\nЛікар: " + doctor.getName() + " (" + doctor.getCategory() + ")" +
            "\nВартість: " + consultationCost + " грн" +
            "\nПокриття страховкою: " + insuranceCoverage + " грн" +
            "\nДо сплати пацієнтом: " + patientPayable + " грн" ;}
```
**Навіщо:** Прийом має всі посилання (`Patient`, `Doctor`, ціни), тому він сам повинен керувати своїми фінансовими підсумками та форматуванням квитанції.
4. Застосування патерну Creator для `Appointment`
Прибрали конструктор за замовчуванням і створили єдиний валідний конструктор:
```Java
    public Appointment(int appointmentId, Patient patient, Doctor doctor){
        this.appointmentId = appointmentId;
        this.patient = patient;
        this.doctor = doctor;
        this.consultationCost = doctor.calculateConsultationFee();
        this.insuranceCoverage = calculateInsuranceCoverage();
	        this.patientPayable = this.consultationCost-this.insuranceCoverage;
        this.status = "Scheduled";
        this.confirm();
    }

```
**Навіщо:** За патерном **Creator**, клас `Appointment` створює та ініціалізує власний внутрішній стан. Тепер виклик `new Appointment(...)` одразу створює повністю порахований та підтверджений прийом без необхідності вручну заповнювати поля ззовні.
#### Завдання 3. Консольна симуляція UI, координація та штучні абстракції (Controller + Indirection + Pure Fabrication)
##### Як працював Legacy-код (До рефакторингу):

 - У legacy-коді не існувало окремого UI-шару. Метод бізнес-сервісу напряму викликав `Scanner` для зчитування даних із клавіатури. Бізнес-сервіс жорстко прив'язаний до консолі. Його неможливо використати у веб-застосунку, графічному інтерфейсі (GUI) чи автоматизованих тестах.
```Java
// LegacyClinicService.java
Scanner scanner = new Scanner(System.in);
System.out.println("=== Запис пацієнта на прийом ===");
System.out.print("Введіть ім'я пацієнта: ");
String name = scanner.nextLine();
```

- Не існувало окремого контролера, який би координував виконання сценарію. Один метод робив абсолютно все: зчитував ввід, створював сутності, виконував розрахунки, зберігав дані та надсилав сповіщення. Порушення принципу єдиної відповідальності (SRP). Клас "знає та робить занадто багато".
```Java
// LegacyClinicService.java
public void scheduleAppointmentConsole() {
	//1: Зчитування даних з консолі через Scanner
    //2: Створення об'єктів
	// 3: Рахував коефіцієнти лікаря: 2.0 для професора, 1.4 для спеціаліста
    //4: Рахував 80% покриття страховки та підсумкову суму
    //5: Збирав текстову квитанцію вручну через конкатенацію рядків
    //6: Імітував запис у файл appointments.log
    //7: Імітував надсилання SMS}
```

- У системі не було інтерфейсів `AppointmentRepository` або `NotificationService`. Сповіщення та збереження імітувалися безпосередньо викликами `System.out.println` усередині бізнес-методу. Високе зчеплення (High Coupling). Якщо потрібно змінити текстовий файл на базу даних (SQL) чи SMS-провайдера, довелося б редагувати та перекомпілювати код бізнес-логіки.
```Java
// LegacyClinicService.java
System.out.println("[Електронна карта] Запис #" + appointment.appointmentId + " збережено у файл appointments.log.");
System.out.println("[SMS] Нагадування про прийом надіслано на номер " + appointment.patient.phone);
```

- Не існувало спеціалізованих інфраструктурних класів `FileAppointmentRepository` чи `SmsNotificationService`.
##### Що змінилось(Після рефакторингу):

1.  Створили проміжні інтерфейси для збереження даних та відправки сповіщень.
```Java
//AppointmentRepository.java
public interface AppointmentRepository{
    Appointment save(Appointment appointment);}
//NotificationService.java
public interface NotificationService {
    void sendSms(Patient patient, Appointment appointment);}
```
**Навіщо:** Паттерн **Indirection** (Посередництво) послаблює зв'язок між контролером та конкретними технічними реалізаціями. Контролер тепер знає лише про контракт (інтерфейс), а не про те, _як саме_ файл записується чи надсилається SMS.

2. Винесли файловий лог та SMS-сповіщення в окремі класи, які імплементують створені інтерфейси.
```Java
//FileAppointmentRepository.java
public class FileAppointmentRepository implements AppointmentRepository {
    @Override
    public void save(Appointment appointment) {
        System.out.println("[Електронна карта] Запис #" + appointment.getAppointmentId() + " збережено у файл appointments.log.");
    }
}
//SmsNotificationService.java
public class SmsNotificationService implements NotificationService {
    @Override
    public void sendSms(Patient patient, Appointment appointment) {
        System.out.println("[SMS] Нагадування про прийом надіслано на номер " + patient.getPhone());
    }
}
```
**Навіщо:** Паттерн **Pure Fabrication** (Штучне створення). У реальному бізнес-домені лікарні немає об'єктів "Файл" чи "SMS Шлюз". Створення цих класів дозволяє зберегти високу згуртованість (High Cohesion) і не перевантажувати доменні сутності (`Patient`, `Appointment`) технічними обов'язками.

3. Створили тонкий контролер `ClinicAppointmentService`. Він отримує залежності через конструктор, викликає конструктор `Appointment` (де спрацьовує бізнес-математика з Завдання 2), зберігає результат у репозиторій та надсилає SMS.
```Java
public class ClinicAppointmentService{
    private final AppointmentRepository appointmentRepository;
    private final NotificationService notificationService;
    public ClinicAppointmentService(AppointmentRepository appointmentRepository, NotificationService notificationService){
        this.appointmentRepository = appointmentRepository;
        this.notificationService = notificationService;
    }
    public Appointment scheduleAppointment(int appointmentId, Patient patient, Doctor doctor){
        Appointment appointment = new Appointment(appointmentId, patient, doctor);
        appointmentRepository.save(appointment);
        notificationService.sendSms(patient, appointment);
        return appointment;
    }
}
```
**Навіщо:** Паттерн **Controller**. Він є першим об'єктом за межами UI, який керує виконанням сценарію. У ньому **немає консольного вводу/виводу** (`Scanner`/`println`) та немає математичних формул — він лише координує роботу сутностей та інфраструктури.

4. Перетворили `Main` на "тонку" точку входу. Він лише компонує залежності, передає вхідні дані до контролера та друкує сформовану квитанцію.
```Java
public class Main { 
public static void main(String[] args) { 
// 1. Ініціалізація інфраструктури та контролера (Dependency Injection) 
	AppointmentRepository repository = new FileAppointmentRepository();
	NotificationService notificationService = new SmsNotificationService(); 
	ClinicAppointmentService clinicAppointmentService = new ClinicAppointmentService(repository, notificationService); 
// 2. Підготовка тестових даних 
	Patient patient = new Patient(101, "Іван Петренко", true, "INS-998877", "+380971234567"); Doctor doctor = new Doctor(501, "Д-р Олексій Коваль", "Кардіолог", "Professor", 800.0); 
// 3. Виклик контролера та вивід результату 
	Appointment appointment = clinicAppointmentService.scheduleAppointment(3001, patient, doctor); 
	System.out.println(appointment.generateInvoice()); 
	} 
}
```
**Навіщо:** Вимога **Thin Console UI**. Шар UI тепер не містить жодного `if-else`, обчислень чи прямих викликів файлової системи. Його єдина відповідальність — відобразити користувачу готовий результат.


## Завдання 4 — Polymorphism + Protected Variations

### Що було

У коді лишалися два каскади умовних операторів за типом:

1. `Doctor.CalculateConsultationFee()` — `if / else if / else` за рядком `Category`:
   * `"Professor"` → `BaseConsultationFee * 2.0`
   * `"Specialist"` → `BaseConsultationFee * 1.4`
   * інакше → `BaseConsultationFee`
2. `Appointment.CalculateInsuranceCoverage()` — `if (Patient.HasInsurance)` → 80 % вартості, інакше `0`.

Обидва місця — точки нестабільності: нова категорія лікаря чи новий вид страхування змушували б редагувати існуючі класи.

### Що зроблено

#### Тарифікація за категорією лікаря

| Файл | Що в ньому |
|---|---|
| `IConsultationFeePolicy.cs` | Стабільний інтерфейс (**Protected Variations**): `string CategoryName { get; }` та `double Calculate(double baseFee)` |
| `FeePolicies.cs` | Три реалізації (**Polymorphism**): `GeneralFeePolicy` (×1.0), `SpecialistFeePolicy` (×1.4), `ProfessorFeePolicy` (×2.0) |
| `ConsultationFeePolicyFactory.cs` | **Pure Fabrication + Dictionary Dispatch**: словник «назва категорії → політика», пошук без урахування регістру. Метод `Create(string category)`. Невідома категорія → `GeneralFeePolicy` (збережено поведінку гілки `else`) |
| `Doctor.cs` | Приймає `IConsultationFeePolicy` у конструкторі; `CalculateConsultationFee()` тепер лише делегує політиці; `Category` — похідна властивість (`_feePolicy.CategoryName`) |

Зміна сигнатури конструктора `Doctor`:

```csharp
// було
new Doctor(501, "Д-р Олексій Коваль", "Вища", "Professor", 800.0);
// стало
new Doctor(501, "Д-р Олексій Коваль", "Вища", feePolicyFactory.Create("Professor"), 800.0);
```

#### Страхове покриття

| Файл | Що в ньому |
|---|---|
| `IInsurancePolicy.cs` | Стабільний інтерфейс: `double CalculateCoverage(double consultationCost)` |
| `InsurancePolicies.cs` | `NoInsurancePolicy` (покриття 0) та `StandardInsurancePolicy` (покриття 80 %, зберігає `PolicyNumber`) |
| `Patient.cs` | Замість `HasInsurance` + `InsurancePolicyNumber` тримає `IInsurancePolicy`; додано метод `CalculateInsuranceCoverage(double cost)` (**Information Expert**: пацієнт знає свою страховку) |
| `Appointment.cs` | Приватний `CalculateInsuranceCoverage()` з `if` **видалено**; у конструкторі: `InsuranceCoverage = patient.CalculateInsuranceCoverage(ConsultationCost)` |

Зміна сигнатури конструктора `Patient`:

```csharp
// було
new Patient(101, "Іван Петренко", true, "INS-998877", "+380971234567");
// стало
new Patient(101, "Іван Петренко", new StandardInsurancePolicy("INS-998877"), "+380971234567");
```

Пацієнт без страховки: `new NoInsurancePolicy()` замість `HasInsurance = false`.

#### Що вирішено не робити

Поле `Status` (`Scheduled / Confirmed / Cancelled`) має одну перевірку в `Confirm()`, каскаду немає. Патерн State був би надлишковим, тому `Status` залишено рядком. Обґрунтування — в ADR-004.

### Як тепер додати нову категорію лікаря

1. Створити клас, що реалізує `IConsultationFeePolicy` (наприклад, `ResidentFeePolicy`, `CategoryName => "Resident"`).
2. Додати один рядок `Register(new ResidentFeePolicy());` у конструктор `ConsultationFeePolicyFactory`.
3. Змінювати `Doctor`, `Appointment`, `ClinicAppointmentService` **не потрібно**.

Нова страхова схема — аналогічно: новий клас з `IInsurancePolicy`, передати його в `Patient`.

---



## Завдання 5 — Low Coupling (CBO) + High Cohesion (LCOM4)

Метрики, пораховані за методичкою на коді після Завдання 3, виявили два порушення нормативів.

### Проблема 1: CBO контролера = 5 (норма ≤ 4)

`ClinicAppointmentService` залежав від `AppointmentRepository`, `NotificationService`, `Appointment`, `Patient`, `Doctor`.

**Рішення (Indirection):** DTO-команда.

| Файл | Зміна |
|---|---|
| `ScheduleAppointmentCommand.cs` (новий) | `public sealed record ScheduleAppointmentCommand(int AppointmentId, Patient Patient, Doctor Doctor);` |
| `ClinicAppointmentService.cs` | `ScheduleAppointment(int, Patient, Doctor)` → `ScheduleAppointment(ScheduleAppointmentCommand command)` |

Залежності сервісу тепер: `AppointmentRepository`, `NotificationService`, `Appointment`, `ScheduleAppointmentCommand` → **CBO = 4**.

### Проблема 2: LCOM4 `Appointment` = 2 (ціль 1)

Методи `Appointment` розпадалися на два незв'язані кластери:

* `Confirm`, `Cancel` — працюють зі `Status`;
* `CalculateInsuranceCoverage`, `GenerateInvoice` — працюють з `Patient`, `ConsultationCost`.

`CalculateInsuranceCoverage` пішов у `Patient` (Завдання 4). Залишалось сформувати квитанцію.

**Рішення (Pure Fabrication):** формування квитанції — відповідальність представлення, а не домену.

| Файл | Зміна |
|---|---|
| `InvoiceGenerator.cs` (новий) | `string Generate(Appointment appointment)` — той самий текст квитанції, що був у `Appointment.GenerateInvoice()` |
| `Appointment.cs` | Метод `GenerateInvoice()` **видалено**. Додано делегуючі властивості `PatientName`, `DoctorName`, `DoctorCategory` |

Делегуючі властивості потрібні для **Закону Деметри**: `InvoiceGenerator` звертається до `appointment.PatientName`, а не до ланцюжка `appointment.Patient.FullName`.

Тепер `Appointment` має лише `Confirm` і `Cancel` над одним полем `Status` → **LCOM4 = 1**.

### Зміни в `Program.cs` (composition root)

* створюються `ConsultationFeePolicyFactory` та `InvoiceGenerator`;
* пацієнт отримує `new StandardInsurancePolicy("INS-998877")`;
* лікар отримує політику з фабрики;
* виклик сервісу: `ScheduleAppointment(new ScheduleAppointmentCommand(3001, patient, doctor))`;
* квитанція: `Console.WriteLine(invoiceGenerator.Generate(appointment));`.

Очікуваний вивід (як і до рефакторингу):

```
[Електронна карта] Запис #3001 збережено у файл appointments.log.
[SMS Gateway] Надіслано SMS на номер: +380971234567
Квитанція прийому #3001
Пацієнт: Іван Петренко
Лікар: Д-р Олексій Коваль (Professor)
Вартість: 1600 грн
Покриття страховкою: 1280 грн
До сплати пацієнтом: 320 грн
```

---

## Метрики (коротко)

Повні розрахунки, графи методів та висновки — у `docs/lab8-task5-metrics.md`.

| Клас | CBO | LCOM4 |
|---|---|---|
| `Appointment` | 2 → 2 | 2 → **1** |
| `Doctor` | 0 → 1 | 1 → 1 |
| `Patient` | 0 → 1 | — → 1 |
| `ClinicAppointmentService` | 5 → **4** | 1 → 1 |

Ключове: CBO `Doctor` і `Patient` зріс з 0 до 1, але це залежність від **стабільного інтерфейсу**, і вона замінила розгалуження за типами. `Program` (composition root) має CBO = 10, норматив ≤ 4 до нього не застосовується.

---

## Відображення на патерни GRASP

| Патерн | Де застосовано |
|---|---|
| Polymorphism | `GeneralFeePolicy` / `SpecialistFeePolicy` / `ProfessorFeePolicy`; `NoInsurancePolicy` / `StandardInsurancePolicy` |
| Protected Variations | `IConsultationFeePolicy`, `IInsurancePolicy` (правила тарифікації та страхування); раніше — `AppointmentRepository`, `NotificationService` |
| Information Expert | `Patient.CalculateInsuranceCoverage` (пацієнт знає свою страховку); `Doctor.CalculateConsultationFee` |
| Pure Fabrication | `ConsultationFeePolicyFactory`, `InvoiceGenerator` |
| Indirection | `ScheduleAppointmentCommand` між клієнтським шаром і контролером |
| Low Coupling / High Cohesion | Метрики CBO ≤ 4 та LCOM4 = 1 (розділ 4) |
| Закон Деметри | Делегуючі властивості `Appointment.PatientName / DoctorName / DoctorCategory` |

---
