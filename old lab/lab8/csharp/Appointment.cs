using System;

public class Appointment
{
    public int AppointmentId { get; }
    public Patient Patient { get; }
    public Doctor Doctor { get; }
    public double ConsultationCost { get; }
    public double InsuranceCoverage { get; }
    public double PatientPayable { get; }
    public string Status { get; private set; }

    // Делегуючі властивості(закон деметри)
    public string PatientName => Patient.FullName;
    public string DoctorName => Doctor.Name;
    public string DoctorCategory => Doctor.Category;

    public Appointment(int appointmentId, Patient patient, Doctor doctor)
    {
        AppointmentId = appointmentId;
        Patient = patient;
        Doctor = doctor;
        ConsultationCost = doctor.CalculateConsultationFee();
        InsuranceCoverage = patient.CalculateInsuranceCoverage(ConsultationCost);
        PatientPayable = ConsultationCost - InsuranceCoverage;
        Status = "Scheduled";
        Confirm();
    }

    public void Confirm()
    {
        if (string.Equals("Cancelled", Status, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Неможливо підтвердити скасований прийом");
        }
        Status = "Confirmed";
    }

    public void Cancel()
    {
        Status = "Cancelled";
    }
}
