# Завдання 5. Метричний аналіз (Low Coupling + High Cohesion)

**Базис «До»** — код після Завдань 1–3 (станом перед Завданням 4).
**«Після»** — код після Завдань 4–5.
**Правила підрахунку:** згідно з методичкою; типи `System.*` та примітиви не враховуються, лише типи проєкту. Для LCOM4 виключено конструктори, автовластивості та тривіальні аксесори.

## 1. CBO (Coupling Between Objects)

| Клас | Зв'язані класи ДО | CBO до | Зв'язані класи ПІСЛЯ | CBO після |
|---|---|---|---|---|
| `Appointment` | Patient, Doctor | 2 | Patient, Doctor | **2** |
| `Doctor` | — | 0 | IConsultationFeePolicy | **1** |
| `Patient` | — | 0 | IInsurancePolicy | **1** |
| `ClinicAppointmentService` | AppointmentRepository, NotificationService, Appointment, Patient, Doctor | **5** ❌ | AppointmentRepository, NotificationService, Appointment, ScheduleAppointmentCommand | **4** ✅ |
| `ConsultationFeePolicyFactory` | (новий) | — | IConsultationFeePolicy, GeneralFeePolicy, SpecialistFeePolicy, ProfessorFeePolicy | **4** |
| `InvoiceGenerator` | (новий) | — | Appointment | **1** |
| `ScheduleAppointmentCommand` | (новий) | — | Patient, Doctor | **2** |
| `GeneralFeePolicy` / `SpecialistFeePolicy` / `ProfessorFeePolicy` | (нові) | — | IConsultationFeePolicy | **1** |
| `NoInsurancePolicy` / `StandardInsurancePolicy` | (нові) | — | IInsurancePolicy | **1** |
| `FileAppointmentRepository` | AppointmentRepository, Appointment | 2 | без змін | **2** |
| `SmsNotificationService` | NotificationService, Patient, Appointment | 3 | без змін | **3** |
| `Program` (composition root) | FileAppointmentRepository, SmsNotificationService, ClinicAppointmentService, Patient, Doctor, Appointment | 6 | + ConsultationFeePolicyFactory, InvoiceGenerator, StandardInsurancePolicy, ScheduleAppointmentCommand | 10 |

Усі доменні сутності та контролер після рефакторингу мають **CBO ≤ 4**. `Program` — composition root, на нього норматив не поширюється (його задача — знати всі конкретні реалізації).

## 2. LCOM4

Вершини — методи; ребро — спільне поле екземпляра або прямий виклик одного методу іншим.

### До
**`Appointment`** — методи: `Confirm`, `Cancel`, `CalculateInsuranceCoverage`, `GenerateInvoice`.

* `Confirm` — `Cancel` (спільне поле `Status`).
* `CalculateInsuranceCoverage` — `GenerateInvoice` (спільні `Patient`, `ConsultationCost`).
* Між групами спільних полів немає → **2 компоненти, LCOM4 = 2**.

### Після
**`Appointment`** — методи: `Confirm`, `Cancel`. Обидва працюють зі `Status` → **LCOM4 = 1**. Розрахунок покриття перенесено до `Patient`, формування квитанції — до `InvoiceGenerator`.

### Підсумок

| Клас | LCOM4 до | LCOM4 після | Пояснення |
|---|---|---|---|
| `Appointment` | **2** ❌ | **1** ✅ | Дві відповідальності розділено |
| `Doctor` | 1 | 1 | Єдиний метод `CalculateConsultationFee` |
| `Patient` | — (методів немає, анемічний) | 1 | Єдиний метод `CalculateInsuranceCoverage` над `_insurance` |
| `ClinicAppointmentService` | 1 | 1 | Єдиний метод `ScheduleAppointment` |
| `ConsultationFeePolicyFactory` | — | 1 | `Create` і `Register` ділять `_policies` |
| `InvoiceGenerator` | — | 1 | Єдиний метод `Generate` |
| Класи політик (5 шт.) | — | 1 | По одному методу |

## 3. Порівняльна таблиця для звіту

| Клас | CBO до → після | LCOM4 до → після | Ціль досягнута |
|---|---|---|---|
| `Appointment` | 2 → 2 | 2 → 1 | ✅ |
| `Doctor` | 0 → 1 | 1 → 1 | ✅ |
| `Patient` | 0 → 1 | — → 1 | ✅ |
| `ClinicAppointmentService` | 5 → 4 | 1 → 1 | ✅ |

## 4. Інженерні висновки

1. **Контролер.** `ClinicAppointmentService` порушував норматив (CBO = 5). Команда-DTO прибрала прямі залежності від `Patient` та `Doctor` у сигнатурі методу, CBO = 4.
2. **Згуртованість.** LCOM4 = 2 у `Appointment` свідчив про дві незалежні відповідальності: життєвий цикл статусу та представлення (квитанція). Винесення в `InvoiceGenerator` (Pure Fabrication) довело LCOM4 до 1 без зміни виводу.
3. **Ціна поліморфізму.** CBO `Doctor` і `Patient` зріс з 0 до 1: це залежність від **стабільного інтерфейсу** (Protected Variations), а не від конкретних класів, тож вона прийнятна. Взамін прибрано розгалуження за типами.
4. **Нова категорія лікаря або страхування** не змінює жодного існуючого доменного класу.
5. **Обмеження підрахунку.** CBO `ClinicAppointmentService` = 4 за правилами методички (поле, виклик методу, параметр, повернення). Автоматичні інструменти, що рахують будь-яке використання типу у виразі (наприклад, `command.Patient`), можуть показати 6. Для запасу можна замінити `SendSms(Patient, Appointment)` на `SendSms(Appointment)` та створювати `Appointment` у фабричному методі.
