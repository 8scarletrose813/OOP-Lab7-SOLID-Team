// Protected Variations: стабільний контракт для нестабільної точки (правила тарифікації за категорією лікаря).
public interface IConsultationFeePolicy
{
    string CategoryName { get; }
    double Calculate(double baseFee);
}
