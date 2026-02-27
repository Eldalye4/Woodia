using Mapster;
using Woodia.Contract.Job;
using Woodia.Contract.Offer;
using Woodia.Entity;
using Woodia.Service.Interface;

namespace Woodia.Service;

public class OfferService:IOfferService
{
    private readonly ApplicationDbContext _context;
    public OfferService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<OfferResponse>> AddAsync(string CarpenterId, int jobId, OfferRequest offerRequest,
        CancellationToken cancellationToken = default)
    {
        var offer = offerRequest.Adapt<Offer>();
        offer.CarpenterId = CarpenterId;
        offer.JobId=jobId;
        await _context.Offers.AddAsync(offer,cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success(offer.Adapt<OfferResponse>());
    }
    
}