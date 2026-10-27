// Protected Variations: стабільний контракт для нестабільної точки (правила страхового покриття).
public interface IInsurancePolicy
{
    double CalculateCoverage(double consultationCost);
}
