public record CategoryNavigationResponse(
    int Id,
    string Name,
    List<CategoryChildNavigationResponse> CategoryChildNavigationResponses
);