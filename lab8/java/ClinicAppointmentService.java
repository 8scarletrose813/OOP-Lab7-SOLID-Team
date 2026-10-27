public class ClinicAppointmentService{
    private AppointmentRepository appointmentRepository;
    private NotificationService notificationService;
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
