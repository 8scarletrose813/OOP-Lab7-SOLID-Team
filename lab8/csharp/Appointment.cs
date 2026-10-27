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

    public Appointment(int appointmentId, Patient patient, Doctor doctor)
    {
        AppointmentId = appointmentId;
        Patient = patient;
        Doctor = doctor;
        ConsultationCost = doctor.CalculateConsultationFee();
        InsuranceCoverage = CalculateInsuranceCoverage();
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

    private double CalculateInsuranceCoverage()
    {
        if (Patient.HasInsurance)
        {
            return ConsultationCost * 0.80;
        }
        else
        {
            return 0.0;
        }
    }

    public string GenerateInvoice()
    {
        return "Квитанція прийому #" + AppointmentId +
                "\nПацієнт: " + Patient.FullName +
                "\nЛікар: " + Doctor.Name + " (" + Doctor.Category + ")" +
                "\nВартість: " + ConsultationCost + " грн" +
                "\nПокриття страховкою: " + InsuranceCoverage + " грн" +
                "\nДо сплати пацієнтом: " + PatientPayable + " грн";
    }
}