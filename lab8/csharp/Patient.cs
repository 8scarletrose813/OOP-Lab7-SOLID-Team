public class Patient
{
    public int Id { get; }
    public string FullName { get; }
    public bool HasInsurance { get; }
    public string InsurancePolicyNumber { get; }
    public string Phone { get; }

    public Patient(int id, string fullName, bool hasInsurance, string insurancePolicyNumber, string phone)
    {
        Id = id;
        FullName = fullName;
        HasInsurance = hasInsurance;
        InsurancePolicyNumber = insurancePolicyNumber;
        Phone = phone;
    }
}