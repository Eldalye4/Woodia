public record CategoryNavigationResponse(
    string Name,
    List<CategoryChildNavigationResponse> CategoryChildNavigationResponses
);