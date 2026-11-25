namespace PrutCredit.Models;

public record ClosureResult(bool Success, string Message)
{
    public static ClosureResult Ok(string message) => new(true, message);

    public static ClosureResult Fail(string message) => new(false, message);
}
