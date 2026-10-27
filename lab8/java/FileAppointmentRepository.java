
public class FileAppointmentRepository implements AppointmentRepository {
    @Override
    public Appointment save(Appointment appointment) {
        int appointmentId = appointment.getAppointmentId();
        System.out.println("[Електронна карта] Запис #" + appointment.getAppointmentId() + " збережено у файл appointments.log.");
        return appointment;

    }
}
