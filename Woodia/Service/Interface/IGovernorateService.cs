public interface IGovernorateService
{
    Task<List<GovernorateResponse>> GetAllGovernorate(CancellationToken cancellationToken);
}