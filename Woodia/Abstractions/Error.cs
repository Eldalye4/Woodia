public record Error(string Code, string Description, int? StatuCode)
{
    public static readonly Error None =
        new(string.Empty, string.Empty, null);
}