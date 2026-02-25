namespace Woodia.Abstractions;
public record Error(string Code, string Description, int? StatuCode)
{
    public static readonly Error None =
        new(string.Empty, string.Empty, null);
    public static readonly Error CannotCancelJob = new(
        "Job.CannotCancel", 
        "Cannot cancel the job because it is no longer in the 'Open' status. It might be already in progress or completed.", 
        StatusCodes.Status400BadRequest
    );
}