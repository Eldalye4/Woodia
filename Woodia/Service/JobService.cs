using System.Text.Json.Nodes;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Woodia.Abstractions.Consts.ConvertJson;
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
        if (await _context.Products.FirstOrDefaultAsync(x => x.Id == jobRequest.ProductId,cancellationToken) is not { } product)
        {
            return Result.failure<JobResponse>(ProductError.ProductNotFound);
        }
    
        if (category.ParentId == null)
        {
            return Result.failure<JobResponse>(JobError.CannotLinkToParent);
        }

        product.JobsCount += 1;
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
        if (await _context.categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == jobRequest.CategoryId,cancellationToken) is not { } category)
        {
            return Result.failure<JobResponse>(CategoryError.CategoryNotFound);
        }
    
        if (category.ParentId == null)
        {
            return Result.failure<JobResponse>(JobError.CannotLinkToParent);
        }
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

    public async Task<Result<PaginatedList<CarpenterJobListingResponse>>> GetAllForCarpenterAsync(RequestFilters requestFilters,CancellationToken cancellationToken)
    {
        var jobs = _context.Jobs.Where(x => x.JobStatus == JobStatus.Open).Join(
            _context.categories,
            x => x.CategoryId,
            y => y.Id,
            (Jobs, Categorys) => new
            {
                Job = Jobs,
                CategoryName = Categorys.NameAr
            }
        ).OrderByDescending(x => x.Job.CreatedAt)
        .Select(x=>new CarpenterJobListingResponse(
            x.Job.Id,
            x.Job.Title,
            x.Job.Description,
            x.Job.DeliveryDay,
            x.Job.ExpectedBudget,
            x.Job.CreatedAt,
                x.CategoryName
            ));
        var response =
            await PaginatedList<CarpenterJobListingResponse>
                .CreateAsync(jobs, requestFilters.PageNumber, requestFilters.PageSize, cancellationToken);
        return Result.Success(response);
    }

    public async Task<Result<CarpenterJobResponse>> GetForCarpenterAsync(int Id,CancellationToken cancellationToken)
    {
        
        var jobs = await _context.Jobs.Where(x=>x.Id==Id && x.JobStatus==JobStatus.Open).Join(
            _context.categories,
            x => x.CategoryId,
            y => y.Id,
            (jobs, Categorys) => new {
                jobs.Id,
                jobs.ModelConfig,
                jobs.Description,
                jobs.DeliveryDay,
                jobs.Title,
                jobs.ExpectedBudget,
                Name = Categorys.NameAr,
                jobs.CreatedAt
            }
        ).FirstOrDefaultAsync(cancellationToken);
        if (jobs is null)
        {
            return Result.failure<CarpenterJobResponse>(JobError.JobNotFound);
        }

        var jobResponse = new CarpenterJobResponse(
            jobs.Id,
            ConvertStringToJson.ValidateJson(jobs.ModelConfig),
            jobs.Description,
            jobs.DeliveryDay,
            jobs.Title,
            jobs.ExpectedBudget,
            jobs.Name,
            jobs.CreatedAt);
        return Result.Success(jobResponse);
    }
    
}