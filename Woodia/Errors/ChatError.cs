namespace Woodia.Errors;

public static class ChatError
{
    public static readonly Error ChatNotFound = new
        ("Chat.ChatNotFound", "The requested Chat was not found.", StatusCodes.Status404NotFound);
    public static readonly Error AccessDenied = new(
        "ChatRoom.AccessDenied", 
        "You are not authorized to access this chat room because you are not a member of it.",
        StatusCodes.Status403Forbidden 
    );

}