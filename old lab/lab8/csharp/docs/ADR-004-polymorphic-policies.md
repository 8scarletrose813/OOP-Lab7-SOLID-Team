# ADR-004: Заміна розгалужень за категорією лікаря та наявністю страховки на поліморфні політики

## Статус
Прийнято (Accepted)

## Контекст
Після Завдань 1–3 у коді лишилися два процедурні розгалуження за типом:

* `Doctor.CalculateConsultationFee()` — каскад `if / else if / else` за рядком `Category` ("Professor" → ×2.0, "Specialist" → ×1.4, інакше ×1.0).
* `Appointment.CalculateInsuranceCoverage()` — `if (Patient.HasInsurance)` → 80 % вартості, інакше 0.

Обидва місця — точки нестабільності: нова категорія лікаря або новий вид страхування вимагає правки існуючих класів (порушення Open/Closed), а категорія як «магічний рядок» не перевіряється компілятором.

## QOC-аналіз

* **Question:** Як ізолювати змінні правила тарифікації та страхового покриття від доменних сутностей?
* **Options:**
  * *Option A:* залишити `if-else`, додавати гілки при появі нових категорій.
  * *Option B:* замінити рядок `Category` на `enum` і зробити `switch`.
  * *Option C:* стабільні інтерфейси `IConsultationFeePolicy` та `IInsurancePolicy` з окремою реалізацією на кожен випадок; відповідність «назва → політика» тримає фабрика зі словником.
* **Criteria:**
  * *Polymorphism:* змінна поведінка вибирається типом, а не умовним оператором.
  * *Protected Variations:* додавання нової категорії/страхування не змінює `Doctor`, `Patient`, `Appointment`.
  * *Low Coupling:* `Doctor` і `Patient` залежать лише від одного інтерфейсу (CBO = 1).
  * *Information Expert:* `Patient` знає свою страховку, тож саме він рахує покриття; `Appointment` лише питає пацієнта.
* **Conclusion:** Обрано **Option C**. Option A порушує OCP, Option B лишає `switch` і потребує перекомпіляції ядра при кожній новій категорії.

## Рішення
* `IConsultationFeePolicy` + `GeneralFeePolicy`, `SpecialistFeePolicy`, `ProfessorFeePolicy`.
* `ConsultationFeePolicyFactory` (Pure Fabrication, Dictionary Dispatch, регістронезалежний пошук). Невідома категорія → базовий тариф, як у гілці `else` вихідного коду.
* `IInsurancePolicy` + `NoInsurancePolicy`, `StandardInsurancePolicy` (покриття 80 %, номер полісу).
* `Doctor` отримує `IConsultationFeePolicy` через конструктор; `Category` — похідна властивість політики.
* `Patient` замість `HasInsurance` / `InsurancePolicyNumber` тримає `IInsurancePolicy` та метод `CalculateInsuranceCoverage(cost)`.
* `Appointment` більше не містить розгалужень і приватного методу розрахунку покриття.

## Наслідки
* **Позитивні:**
  * Нова категорія лікаря = новий клас + один рядок реєстрації у фабриці.
  * Нове страхування = новий клас, ядро не змінюється.
  * Політики тестуються ізольовано.
* **Негативні / Компроміси:**
  * Більше класів і файлів.
  * Змінився публічний API `Doctor` та `Patient` (конструктори), оновлено composition root у `Program`.
  * Незмінний рядковий вхід («Professor») тепер потрапляє лише у фабрику; помилка в назві мовчки дає базовий тариф (збережена поведінка вихідного коду).

## Відхилена альтернатива для статусів
Поле `Status` (`Scheduled/Confirmed/Cancelled`) має лише одну умовну перевірку в `Confirm()`. Каскаду немає, тому State-патерн був би надлишковим (YAGNI). Повернутись до рішення, якщо з'являться нові статуси та переходи.
