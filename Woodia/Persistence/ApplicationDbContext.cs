

using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Woodia.Entity;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :  IdentityDbContext<ApplicationUser,ApplicationRole,string>(options)
{
    public DbSet<CarpenterProfile> CarpenterProfiles{get;set;}
    public DbSet<PortfolioItem> PortfolioItems {get;set;}
    public DbSet<PortfolioItemImage> PortfolioItemImages { get; set; }
    public DbSet<ContactInformation> contactInformation{ get; set; }
    public DbSet<Governorate> governorates{ get; set; }
    public DbSet<Category> categories{ get; set; }
    public DbSet<Product> Products{get;set;}
    public DbSet<ProductImage> ProductImages{get;set;}
    public DbSet<Job> Jobs{get;set;}
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<AuditableEntity>();
        foreach (var entityEntry in entries)
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property(x => x.CreatedAt).CurrentValue = DateTime.UtcNow;
            }
            else if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property(x => x.UpdateAt).CurrentValue = DateTime.UtcNow;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
        
        
        
    }

}