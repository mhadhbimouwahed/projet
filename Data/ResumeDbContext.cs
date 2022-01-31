using ApplicationManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace ApplicationManagement.Data;

public class ResumeDbContext:DbContext
{
    public ResumeDbContext(DbContextOptions options) : base(options)
    {
        
    }
    
    
    public virtual DbSet<Applicant> Applicants { get; set; }
    public virtual DbSet<Experience> Experiences { get; set; }
    

}