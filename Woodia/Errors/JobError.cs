namespace Woodia.Errors;

public static class JobError
{
    public static readonly Error JobNotFound = new
        ("Job.JobNotFound", "The requested Job was not found.", StatusCodes.Status404NotFound);

    public static readonly Error NotAClient = new(
        "User.NotAClient", 
        "Only clients are authorized to post job requests.", 
        StatusCodes.Status403Forbidden
    );
    public static readonly Error CannotLinkToParent = new(
        "Category.CannotLinkToParent", 
        "A Job cannot be linked directly to a parent category.", 
        StatusCodes.Status400BadRequest);
    public static readonly Error CannotCancelJob = new(
        "Job.CannotCancel", 
        "Cannot cancel the job because it is no longer in the 'Open' status. It might be already in progress or completed.", 
        StatusCodes.Status400BadRequest
    );
    public static readonly Error CannotStartJob = new(
        "Job.CannotStart", 
        "Cannot move the job to 'In Progress' status because it is not currently open. It might have been canceled or already started.", 
        StatusCodes.Status400BadRequest 
    );
    
    public static readonly Error CannotUpdateJob = new(
        "Job.CannotUpdate", 
        "Cannot update the job because it is no longer open. It might be already in progress, completed, or canceled.", 
        StatusCodes.Status400BadRequest 
    );
}