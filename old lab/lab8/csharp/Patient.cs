public class Patient
{
    private readonly IInsurancePolicy _insurance;

    public int Id { get; }
    public string FullName { get; }
    public string Phone { get; }

    public Patient(int id, string fullName, IInsurancePolicy insurance, string phone)
    {
        Id = id;
        FullName = fullName;
        _insurance = insurance;
        Phone = phone;
    }

    // Information Expert: пацієнт знає свою страхову політику, тож Appointment не розгалужується за HasInsurance.
    public double CalculateInsuranceCoverage(double consultationCost)
    {
        return _insurance.CalculateCoverage(consultationCost);
    }
}
