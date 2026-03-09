using Mapster;
using Microsoft.EntityFrameworkCore;
using Woodia.Contract.Job;
using Woodia.Contract.Offer;
using Woodia.Entity;
using Woodia.Enums;
using Woodia.Service.Interface;

namespace Woodia.Service;

public class OfferService:IOfferService
{
    private readonly ApplicationDbContext _context;
    public OfferService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<OfferResponse>> AddAsync(string carpenterId, int jobId, OfferRequest offerRequest,
        CancellationToken cancellationToken = default)
    {
        if (!await _context.Jobs.AnyAsync(x => x.Id == jobId &&x.JobStatus==JobStatus.Open,cancellationToken))
        {
            return Result.failure<OfferResponse>(JobError.JobNotFound);
        }

        if (await _context.Offers.AnyAsync(x => x.JobId == jobId && x.CarpenterId == carpenterId && x.Status!=OfferStatus.Withdrawn,cancellationToken))
        {
            return Result.failure<OfferResponse>(OfferError.DuplicateOffer);
        }
        var offer = offerRequest.Adapt<Offer>();
        offer.CarpenterId = carpenterId;
        offer.JobId=jobId;
        await _context.Offers.AddAsync(offer,cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success(offer.Adapt<OfferResponse>());
    }

    public async Task<Result<PaginatedList<OfferSummaryResponse>>> GetAllAsync(RequestFilters requestFilters,string carpenterId, CancellationToken cancellationToken=default)
    {
        var offers = _context.Offers
            .Include(x=>x.Job)
            .OrderByDescending(x=>x.CreatedAt)
            .Where(x => x.CarpenterId == carpenterId)
            .ProjectToType<OfferSummaryResponse>();
        var response =
            await PaginatedList<OfferSummaryResponse>
                .CreateAsync(offers, requestFilters.PageNumber, requestFilters.PageSize, cancellationToken);
        return Result.Success(response);
    }

    public async Task<Result<OfferDetailsResponse>> GetAsync(string carpenterId,int id, CancellationToken cancellationToken = default)
    {
        var offer =await _context.Offers
            .Where(x => x.Id == id && x.CarpenterId == carpenterId)
            .ProjectToType<OfferDetailsResponse>()
            .FirstOrDefaultAsync(cancellationToken);
        if (offer is null)
        {
            return Result.failure<OfferDetailsResponse>(OfferError.OfferNotFound);
        }
        return Result.Success(offer);
    }

    public async Task<Result> withdrawStatusAsync(string carpenterId, int id,
        CancellationToken cancellationToken = default)
    {
        var offer = await _context.Offers.FirstOrDefaultAsync(x => x.Id == id && x.CarpenterId == carpenterId,
            cancellationToken);
        if (offer is null)
        {
            return Result.failure(JobError.JobNotFound);
        }

        if (offer.Status == OfferStatus.Accepted || offer.Status == OfferStatus.Rejected)
        {
            return Result.failure(OfferError.CannotWithdraw);
        }

        if (offer.Status == OfferStatus.Withdrawn)
        {
            return Result.failure(OfferError.AlreadyWithdrawn);
        }

        offer.Status = OfferStatus.Withdrawn;
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }

    public async Task<Result<PaginatedList<JobOfferSummaryResponse>>> GetAllOfferByJobAsync(RequestFilters requestFilters,
        int jobId, string ClientId,CancellationToken cancellationToken = default)
    {
        if (!await _context.Jobs.AnyAsync(x => x.Id == jobId , cancellationToken))
        {
            return Result.failure<PaginatedList<JobOfferSummaryResponse>>(JobError.JobNotFound);
        }
        var offers =  _context.Offers
            .Where(x => x.Job.ClientId == ClientId && x.JobId == jobId && 
                        (x.Status==OfferStatus.Pending || x.Status==OfferStatus.Interviewing))
            .ProjectToType<JobOfferSummaryResponse>();
            
        var response =
            await PaginatedList<JobOfferSummaryResponse>
                .CreateAsync(offers, requestFilters.PageNumber, requestFilters.PageSize, cancellationToken);
        return Result.Success(response);
    }
    
    public async Task<Result<JobOfferDetailsResponse>> GetOfferByJobAsync(
        int jobId, string ClientId,int offerId,CancellationToken cancellationToken = default)
    {
        if (!await _context.Jobs.AnyAsync(x => x.Id == jobId , cancellationToken))
        {
            return Result.failure<JobOfferDetailsResponse>(JobError.JobNotFound);
        }
        var offer = await _context.Offers
            .Where(x => x.Job.ClientId == ClientId && 
                        x.Id == offerId &&
                        x.JobId == jobId &&
                        (x.Status == OfferStatus.Pending || 
                         x.Status == OfferStatus.Interviewing))
            .ProjectToType<JobOfferDetailsResponse>()
            .FirstOrDefaultAsync(cancellationToken);
        if (offer is null)
        {
            return Result.failure<JobOfferDetailsResponse>(OfferError.OfferNotFound);
        }
            
        
        return Result.Success(offer);
    }

    public async Task<Result> RejectOffer(string clientID, int offerId, CancellationToken cancellationToken = default)
    {
        var offer = await _context.Offers.FirstOrDefaultAsync(x => x.Id == offerId &&x.Job.ClientId==clientID, cancellationToken);
        if (offer is null)
        {
            return Result.failure(OfferError.OfferNotFound);
        }

        if (offer.Status != OfferStatus.Interviewing && offer.Status != OfferStatus.Pending)
        {
            return Result.failure(OfferError.InvalidOfferStatus);
        }
        offer.Status=OfferStatus.Rejected;
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}