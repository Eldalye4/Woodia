using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Woodia.Contract.Job;
using Woodia.Entity;
using Woodia.Enums;
using Woodia.Service.Interface;

namespace Woodia.Service;

public class JobService:IJobService
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    public JobService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<Result<JobResponse>> AddAsync(string UserId,JobRequest jobRequest, CancellationToken cancellationToken)
    {
        
    
        if (await _context.categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == jobRequest.CategoryId,cancellationToken) is not { } category)
        {
            return Result.failure<JobResponse>(CategoryError.CategoryNotFound);
        }
    
        if (category.ParentId == null)
        {
            return Result.failure<JobResponse>(JobError.CannotLinkToParent);
        }
    
        var job = jobRequest.Adapt<Job>();
        job.ClientId = UserId;
        await _context.Jobs.AddAsync(job, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success(job.Adapt<JobResponse>());
        
        
        
    }

    public async Task<Result<PaginatedList<JoblistingResponse>>> GetAllForClientAsync(RequestFilters requestFilters,
        string userId, CancellationToken cancellationToken)
    {
        JobStatus? parsedStatus=null;
        if (!string.IsNullOrEmpty(requestFilters.SearchValue) &&
            Enum.TryParse<JobStatus>(requestFilters.SearchValue, ignoreCase:true,out var status))
        {
            parsedStatus=status;
        }
            
            
        var jobs = _context
            .Jobs
            .Where(x=>x.ClientId==userId && (!parsedStatus.HasValue||x.JobStatus==parsedStatus.Value ))
            .OrderByDescending(x=>x.CreatedAt)
            .ProjectToType<JoblistingResponse>();

        var response =
            await PaginatedList<JoblistingResponse>
                .CreateAsync(jobs, requestFilters.PageNumber, requestFilters.PageSize, cancellationToken);
        return Result.Success(response);
        
        
    }

    public async Task<Result<JobResponse>> GetForClientAsync(string clientID,int Id, CancellationToken cancellationToken)
    {
        var job = await _context.Jobs.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id &&x.ClientId==clientID ,cancellationToken);
        if (job is null)
        {
            return Result.failure<JobResponse>(JobError.JobNotFound);
        }
        return Result.Success(job.Adapt<JobResponse>());
    }

    public async Task<Result> UpdateAsync(string clientID,int Id, JobRequest jobRequest,
        CancellationToken cancellationToken)
    {
        var job = await _context.Jobs.FirstOrDefaultAsync(x => x.Id == Id  && x.ClientId==clientID,cancellationToken);
        if (job is null)
        {
            return Result.failure(JobError.JobNotFound);
        }
        if (job.JobStatus != JobStatus.Open)
        {
            return Result.failure(JobError.CannotUpdateJob); 
        }

        jobRequest.Adapt(job);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }

    public async Task<Result> CanceleStateAsync(string clientID, int Id,CancellationToken cancellationToken)
    {
        var job = await _context.Jobs.FirstOrDefaultAsync(x => x.Id == Id  && x.ClientId==clientID,cancellationToken);
        if (job is null)
        {
            return Result.failure(JobError.JobNotFound);
        }
        if (job.JobStatus != JobStatus.Open)
        {
            return Result.failure(JobError.CannotCancelJob); 
        }

        job.JobStatus = JobStatus.Canceled;
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
    
    public async Task<Result> CompleteStateAsync(string clientID, int Id,CancellationToken cancellationToken)
    {
        var job = await _context.Jobs.FirstOrDefaultAsync(x => x.Id == Id  && x.ClientId==clientID,cancellationToken);
        if (job is null)
        {
            return Result.failure(JobError.JobNotFound);
        }

        job.JobStatus = JobStatus.Completed;
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
    public async Task<Result> InProgressStateAsync(string clientID, int Id,CancellationToken cancellationToken)
    {
        var job = await _context.Jobs.FirstOrDefaultAsync(x => x.Id == Id  && x.ClientId==clientID,cancellationToken);
        if (job is null)
        {
            return Result.failure(JobError.JobNotFound);
        }
        if (job.JobStatus != JobStatus.Open)
        {
            return Result.failure(JobError.CannotStartJob);
        }

        job.JobStatus = JobStatus.InProgress;
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}