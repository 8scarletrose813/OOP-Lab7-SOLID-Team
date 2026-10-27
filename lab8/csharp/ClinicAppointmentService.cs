public class ClinicAppointmentService
{
    private readonly AppointmentRepository _appointmentRepository;
    private readonly NotificationService _notificationService;

    public ClinicAppointmentService(AppointmentRepository appointmentRepository, NotificationService notificationService)
    {
        _appointmentRepository = appointmentRepository;
        _notificationService = notificationService;
    }

    public Appointment ScheduleAppointment(int appointmentId, Patient patient, Doctor doctor)
    {
        Appointment appointment = new Appointment(appointmentId, patient, doctor);
        _appointmentRepository.Save(appointment);
        _notificationService.SendSms(patient, appointment);
        return appointment;
    }
}