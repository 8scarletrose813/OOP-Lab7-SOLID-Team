using System.Globalization;

namespace PrutCredit.Task2;

public sealed record Payment(int Number, string Account, decimal Amount)
{
    private static readonly NumberFormatInfo Nfi = new() { NumberGroupSeparator = " ", NumberDecimalDigits = 0 };

    public string AmountText => Amount.ToString("N0", Nfi) + " грн";
}
