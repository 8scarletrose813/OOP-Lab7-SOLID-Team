public class ClinicAppointmentService
{
    private readonly AppointmentRepository _appointmentRepository;
    private readonly NotificationService _notificationService;

    public ClinicAppointmentService(AppointmentRepository appointmentRepository, NotificationService notificationService)
    {
        _appointmentRepository = appointmentRepository;
        _notificationService = notificationService;
    }

    public Appointment ScheduleAppointment(ScheduleAppointmentCommand command)
    {
        Appointment appointment = new Appointment(command.AppointmentId, command.Patient, command.Doctor);
        _appointmentRepository.Save(appointment);
        _notificationService.SendSms(command.Patient, appointment);
        return appointment;
    }
}
