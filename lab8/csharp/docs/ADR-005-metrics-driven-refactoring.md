# ADR-005: Рефакторинг за результатами метрик CBO та LCOM4

## Статус
Прийнято (Accepted)

## Контекст
Метричний аналіз коду після Завдань 1–3 виявив два порушення нормативів:

* `ClinicAppointmentService` (контролер): **CBO = 5** при нормативі ≤ 4 (залежить від `AppointmentRepository`, `NotificationService`, `Appointment`, `Patient`, `Doctor`).
* `Appointment` (доменна сутність): **LCOM4 = 2** при цілі 1. Граф методів розпадається на дві компоненти: {`Confirm`, `Cancel`} (поле `Status`) та {`CalculateInsuranceCoverage`, `GenerateInvoice`} (поля `Patient`, `Doctor`, суми).

## QOC-аналіз

* **Question 1:** Як знизити CBO контролера до ≤ 4?
  * *Option A:* передавати в метод окремо `Patient` та `Doctor` (поточний стан).
  * *Option B:* ввести DTO-команду `ScheduleAppointmentCommand` (Indirection).
  * *Option C:* передавати ідентифікатори й завантажувати сутності через додаткові репозиторії.
  * *Criteria:* Low Coupling; мінімум нових залежностей; відповідність Завданню 3 («приймає системну команду/DTO»).
  * *Conclusion:* **Option B**. Option C додає два нові інтерфейси репозиторіїв, що не дає виграшу за CBO.
* **Question 2:** Де має жити формування квитанції?
  * *Option A:* лишити `GenerateInvoice()` в `Appointment`.
  * *Option B:* перенести у `InvoiceGenerator` (Pure Fabrication).
  * *Option C:* додати до квитанції рядок статусу, щоб метод використовував `Status`.
  * *Criteria:* High Cohesion (LCOM4 = 1); Demeter's Law; відсутність зміни формату квитанції.
  * *Conclusion:* **Option B**. Option C штучно «лікує» метрику та змінює вивід. Формування тексту — відповідальність представлення, а не домену.

## Рішення
* `ScheduleAppointmentCommand(AppointmentId, Patient, Doctor)` — `record`; `ClinicAppointmentService.ScheduleAppointment` приймає команду.
* `InvoiceGenerator.Generate(Appointment)` замість `Appointment.GenerateInvoice()`.
* До `Appointment` додано делегуючі властивості `PatientName`, `DoctorName`, `DoctorCategory`, щоб `InvoiceGenerator` не порушував Закон Деметри (`appointment.Patient.FullName`).

## Наслідки
* **Позитивні:**
  * CBO контролера 5 → 4.
  * LCOM4 `Appointment` 2 → 1.
  * Формат квитанції не змінився.
* **Негативні / Компроміси:**
  * Додано два класи (`InvoiceGenerator`, `ScheduleAppointmentCommand`).
  * `Program` (composition root) має CBO = 10, оскільки збирає всі залежності. Норматив ≤ 4 застосовується до сутностей і контролерів, а не до composition root.
  * Команда містить доменні об'єкти, а не ідентифікатори. Подальший крок — завантаження сутностей контролером за ID.
