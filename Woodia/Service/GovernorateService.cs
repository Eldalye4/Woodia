using Mapster;
using Microsoft.EntityFrameworkCore;

public class GovernorateService:IGovernorateService
{
    private readonly ApplicationDbContext _context;

    public GovernorateService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<List<GovernorateResponse>> GetAllGovernorate(CancellationToken cancellationToken)
        => await _context.governorates
            .AsNoTracking()
            .ProjectToType<GovernorateResponse>()
            .ToListAsync(cancellationToken);
        
    
}