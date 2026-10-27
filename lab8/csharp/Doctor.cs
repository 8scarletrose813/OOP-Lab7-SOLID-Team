public class Doctor
{
    private readonly IConsultationFeePolicy _feePolicy;

    public int Id { get; }
    public string Name { get; }
    public string Specialty { get; }
    public string Category => _feePolicy.CategoryName; // "General", "Specialist", "Professor"
    public double BaseConsultationFee { get; }

    public Doctor(int id, string name, string specialty, IConsultationFeePolicy feePolicy, double baseConsultationFee)
    {
        Id = id;
        Name = name;
        Specialty = specialty;
        _feePolicy = feePolicy;
        BaseConsultationFee = baseConsultationFee;
    }

    public double CalculateConsultationFee()
    {
        return _feePolicy.Calculate(BaseConsultationFee);
    }
}
