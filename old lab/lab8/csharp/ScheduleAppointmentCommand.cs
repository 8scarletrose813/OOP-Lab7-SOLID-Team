// DTO-команда від клієнтського шару до контролера (за вимогою Завдання 3: "системна команда/DTO").
public sealed record ScheduleAppointmentCommand(int AppointmentId, Patient Patient, Doctor Doctor);
